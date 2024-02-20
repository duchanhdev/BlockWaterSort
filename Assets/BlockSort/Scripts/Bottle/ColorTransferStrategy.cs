using System.Collections;
using UnityEngine;

namespace BlockSort.Bottle
{
    [CreateAssetMenu(fileName = "ColorTransferStrategy", menuName = "ColorTransferStrategy/Shader An Animation")]
    public sealed class ColorTransferStrategy : ScriptableObject
    {
        private static readonly int SARM = Shader.PropertyToID("_SARM");

        public IEnumerator StartColorTransfer(Bottle originBottle, Bottle bottleToTransfer, Color colorToTransfer)
        {
            var state = ChooseRotationAndDirection(originBottle, bottleToTransfer);
            var numberOfColorsInBottleToTransfer = bottleToTransfer.NumberOfColorsInBottle;
            var numberOfColorsToTransfer =
                Mathf.Min(originBottle.NumberOfTopColorLayers, 4 - numberOfColorsInBottleToTransfer);

            bottleToTransfer.FillColorsOnTop(numberOfColorsToTransfer, numberOfColorsInBottleToTransfer,
                colorToTransfer);

            CalculateRotationIndex(originBottle, originBottle.BottleColors.Length - numberOfColorsInBottleToTransfer);
            originBottle.MoveForwardToAnimate();
            yield return originBottle.StartCoroutine(MoveToBottleToFill(originBottle, bottleToTransfer, state));
        }

        private static State ChooseRotationAndDirection(Bottle originBottle, Bottle bottleToTransfer)
        {
            var isLeftSideOfBottleToFill = originBottle.transform.position.x > bottleToTransfer.transform.position.x;
            var chosenRotationPoint = isLeftSideOfBottleToFill
                ? originBottle.LeftRotationPoint
                : originBottle.RightRotationPoint;
            var directionMultiplier = isLeftSideOfBottleToFill ? -1 : 1;

            return new State(chosenRotationPoint, directionMultiplier);
        }

        private static void CalculateRotationIndex(Bottle originBottle, int numberOfEmptySpaceInSecondBottle)
        {
            var rotationIndex = originBottle.BottleColors.Length - 1 - (originBottle.NumberOfColorsInBottle - Mathf.Min(
                numberOfEmptySpaceInSecondBottle,
                originBottle.NumberOfTopColorLayers));

            originBottle.SetRotationIndex(rotationIndex);
        }

        private IEnumerator MoveToBottleToFill(Bottle originBottle, Bottle toTransferBottle, State state)
        {
            state.StartPosition = originBottle.transform.position;
            state.EndPosition = state.ChosenRotationPoint == originBottle.LeftRotationPoint
                ? toTransferBottle.RightRotationPoint.position
                : toTransferBottle.LeftRotationPoint.position;

            originBottle.PauseMoveUp();

            float t = 0;
            while (t <= 1)
            {
                originBottle.transform.position = Vector3.Lerp(state.StartPosition, state.EndPosition, t);
                t += Time.deltaTime * originBottle.BottleMovementSpeed;

                yield return new WaitForEndOfFrame();
            }

            originBottle.transform.position = state.EndPosition;
            yield return originBottle.StartCoroutine(Rotate(originBottle, toTransferBottle, state));
        }

        private IEnumerator Rotate(Bottle originBottle, Bottle toTransferBottle, State state)
        {
            float t = 0;
            float lerpValue;
            float angleValue;
            float lastAngleValue = 0;

            var timeToRotate = originBottle.TimeToRotate;
            var linePouringWaterRenderer = originBottle.LinePouringWaterRenderer;
            while (t < timeToRotate)
            {
                lerpValue = t / timeToRotate;
                angleValue = Mathf.Lerp(0.0f,
                    state.DirectionMultiplier * originBottle.RotationValues[originBottle.RotationIndex], lerpValue);

                originBottle.transform.RotateAround(state.ChosenRotationPoint.position, Vector3.forward,
                    lastAngleValue - angleValue);

                originBottle.BottleMaskSR.material.SetFloat(SARM,
                    originBottle.ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

                if (originBottle.FillAmounts[originBottle.NumberOfColorsInBottle] >
                    originBottle.FillAmountCurve.Evaluate(angleValue) + 0.005f)
                {
                    if (linePouringWaterRenderer != null && linePouringWaterRenderer.enabled == false)
                    {
                        originBottle.SetTopColorAlpha(200);
                        linePouringWaterRenderer.startColor = originBottle.TopColor;
                        linePouringWaterRenderer.endColor = originBottle.TopColor;
                        var position = state.ChosenRotationPoint.position;
                        linePouringWaterRenderer.SetPosition(0, position);
                        linePouringWaterRenderer.SetPosition(1, position - Vector3.up * 1.45f);
                        toTransferBottle.MoveForwardToFillColor();
                        linePouringWaterRenderer.enabled = true;
                    }

                    originBottle.Refill(originBottle.FillAmountCurve.Evaluate(angleValue));

                    toTransferBottle.FillUp(originBottle.FillAmountCurve.Evaluate(lastAngleValue) -
                                            originBottle.FillAmountCurve.Evaluate(angleValue));
                }

                t += Time.deltaTime * originBottle.RotationSpeedMultiplierCurve.Evaluate(angleValue) *
                     originBottle.BottleRotateSpeed;
                lastAngleValue = angleValue;
                yield return new WaitForEndOfFrame();
            }

            angleValue = state.DirectionMultiplier * originBottle.RotationValues[originBottle.RotationIndex];
            originBottle.BottleMaskSR.material.SetFloat(SARM,
                originBottle.ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
            originBottle.Refill(originBottle.FillAmountCurve.Evaluate(angleValue));

            originBottle.NumberOfColorsInBottle -= originBottle.NumberOfColorsToTransfer;
            toTransferBottle.NumberOfColorsInBottle += originBottle.NumberOfColorsToTransfer;

            toTransferBottle.Fulfill();
            originBottle.StopPouringSound();
            if (originBottle.LinePouringWaterRenderer != null)
            {
                linePouringWaterRenderer.enabled = false;
            }

            toTransferBottle.MoveBackwardAfterFillColor();

            yield return originBottle.StartCoroutine(RotateBack(originBottle, state));
        }

        private IEnumerator RotateBack(Bottle originBottle, State state)
        {
            float t = 0;
            float lerpValue;
            float angleValue;

            var lastAngleValue = state.DirectionMultiplier * originBottle.RotationValues[originBottle.RotationIndex];

            var timeToRotate = originBottle.TimeToRotate;
            while (t < timeToRotate)
            {
                lerpValue = t / timeToRotate;
                angleValue =
                    Mathf.Lerp(state.DirectionMultiplier * originBottle.RotationValues[originBottle.RotationIndex],
                        0.0f, lerpValue);

                originBottle.transform.RotateAround(state.ChosenRotationPoint.position, Vector3.forward,
                    lastAngleValue - angleValue);
                originBottle.BottleMaskSR.material.SetFloat(SARM,
                    originBottle.ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

                lastAngleValue = angleValue;

                t += Time.deltaTime * originBottle.BottleRotateSpeed;

                yield return new WaitForEndOfFrame();
            }

            originBottle.UpdateTopColorValues();
            angleValue = 0f;
            originBottle.transform.eulerAngles = new Vector3(0, 0, angleValue);
            originBottle.BottleMaskSR.material.SetFloat(SARM,
                originBottle.ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            yield return originBottle.StartCoroutine(MoveBackToOriginPosition(originBottle, state));
        }

        private static IEnumerator MoveBackToOriginPosition(Bottle originBottle, State state)
        {
            state.StartPosition = originBottle.transform.position;
            state.EndPosition = originBottle.OriginalPosition;
            float t = 0;
            while (t <= 1)
            {
                originBottle.transform.position = Vector3.Lerp(state.StartPosition, state.EndPosition, t);
                t += Time.deltaTime * originBottle.BottleMovementSpeed;

                yield return new WaitForEndOfFrame();
            }

            originBottle.transform.position = state.EndPosition;

            originBottle.MoveBackwardAfterAnimate();
        }

        private struct State
        {
            public Vector3 StartPosition { get; set; }

            public Vector3 EndPosition { get; set; }

            public Transform ChosenRotationPoint { get; }

            public float DirectionMultiplier { get; }

            public State(Transform chosenRotationPoint, float directionMultiplier = 1.0f)
            {
                ChosenRotationPoint = chosenRotationPoint;
                DirectionMultiplier = directionMultiplier;
                StartPosition = Vector3.zero;
                EndPosition = Vector3.zero;
            }
        }
    }
}
