using System.Collections;
using BlockSort.GameLogic;
using BlockSort.Sound;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BlockSort.Bottle
{
    public sealed class Bottle : MonoBehaviour, IPointerDownHandler
    {
        private static readonly int SARM = Shader.PropertyToID("_SARM");

        private static readonly int FILL_AMOUNT = Shader.PropertyToID("_FillAmount");

        [SerializeField]
        private SpriteRenderer[] _tempWaterSprites;

        [SerializeField]
        private float bottleUpWhenChoose;

        [SerializeField]
        private float _moveUpDuration = 1;

        [SerializeField]
        private Transform leftRotationPoint;

        [SerializeField]
        private Transform rightRotationPoint;

        public float _bottleMovementSpeed = 2;

        public float bottleRotateSpeed = 1;

        public UnityEvent<int> OnInteract;

        [SerializeField]
        private float _timeToRotate = 1;

        [SerializeField]
        private float[] _rotationValues = { 54, 71, 83, 90 };

        [SerializeField]
        private SpriteRenderer _bottleMaskSR;

        [SerializeField]
        private AnimationCurve _scaleAndRotationMultiplierCurve;

        [SerializeField]
        private AnimationCurve _fillAmountCurve;

        [SerializeField]
        private AnimationCurve _rotationSpeedMultiplierCurve;

        [SerializeField]
        private float[] _fillAmounts = { -0.5f, -0.29f, -0.08f, 0.13f, 0.34f };

        [Range(0, 4)]
        [SerializeField]
        private int _numberOfColorsInBottle = 4;

        [SerializeField]
        private LineRenderer _linePouringWaterRenderer;

        [SerializeField]
        private Color _topColor;

        [SerializeField]
        private SpriteRenderer _fillRenderer;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Color[] _bottleColors;

        [SerializeField]
        private AudioSource PouringSoundPrefab;

        [SerializeField]
        private float timeToPlayPouringSound;

        [SerializeField]
        private ColorTransferStrategy _colorTransferStrategy;

        private int _bottleIndex = -1;

        private Bottle _bottleToFill;

        private Tween _moveBottleUpTween;
        private float _moveUpOffsetFromOriginY;

        private float _originY;

        private Transform choosenRotationPoint;
        private float directionMultiplier = 1.0f;
        private Vector3 endPosition;
        private bool isPouringSoundPlaying;

        private AudioSource pouringSound;

        private Vector3 startposition;

        public Transform LeftRotationPoint => leftRotationPoint;

        public Transform RightRotationPoint => rightRotationPoint;

        public float BottleMovementSpeed => _bottleMovementSpeed;

        public float BottleRotateSpeed => bottleRotateSpeed;

        public float TimeToRotate => _timeToRotate;

        public float[] RotationValues => _rotationValues;

        public SpriteRenderer BottleMaskSR => _bottleMaskSR;

        public AnimationCurve ScaleAndRotationMultiplierCurve => _scaleAndRotationMultiplierCurve;

        public AnimationCurve FillAmountCurve => _fillAmountCurve;

        public AnimationCurve RotationSpeedMultiplierCurve => _rotationSpeedMultiplierCurve;

        public float[] FillAmounts => _fillAmounts;

        public int NumberOfColorsInBottle
        {
            get => _numberOfColorsInBottle;
            set => _numberOfColorsInBottle = value;
        }

        public LineRenderer LinePouringWaterRenderer => _linePouringWaterRenderer;

        public Color TopColor => _topColor;

        public Color[] BottleColors => _bottleColors;

        public int NumberOfColorsToTransfer { get; private set; }

        public int NumberOfTopColorLayers { get; private set; } = 1;

        /// <summary>
        ///     How much the bottle should rotate to transform all bottle liquid
        /// </summary>
        public int RotationIndex { get; private set; }

        public Vector3 OriginalPosition { get; private set; }

        private void Awake()
        {
            OriginalPosition = transform.position;
            _originY = transform.position.y;
            _moveUpOffsetFromOriginY = transform.position.y + bottleUpWhenChoose;
            GetPouringSound();
        }

        private void OnDestroy()
        {
            SoundController.Instance.RemoveAudioSource(pouringSound);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnInteract.Invoke(_bottleIndex);
        }

        public void SetTopColorAlpha(float alpha)
        {
            _topColor.a = alpha;
        }

        public void FillColorsOnTop(int numberOfColorsToTransfer, int numberOfColorsInBottleToFill, Color color)
        {
            for (var i = 0; i < numberOfColorsToTransfer; i++)
            {
                _bottleColors[numberOfColorsInBottleToFill + i] = color;
            }

            UpdateColorsOnShader();
        }

        public void SetLinePouringWaterRenderer(LineRenderer lineRenderer)
        {
            _linePouringWaterRenderer = lineRenderer;
        }

        private void UpdateColorsOnShader()
        {
            if (_bottleMaskSR == null)
            {
                return;
            }

            for (var i = 0; i < _bottleColors.Length; i++)
            {
                _bottleMaskSR.material.SetColor($"_C{i + 1}", _bottleColors[i]);
            }
        }

        public void SetColorsWithTubeLogic(int bottleIndex, Tube tubeLogic)
        {
            _bottleIndex = bottleIndex;
            var numBlock = tubeLogic.GetNumBlock();
            _numberOfColorsInBottle = numBlock;
            var blocksLogic = tubeLogic.GetBlocks();
            for (var i = 0; i < (int)Config.Config.NUM_BLOCK_IN_TUBE; i++)
            {
                var isVisibleTube = i < numBlock;

                _tempWaterSprites[i].color = isVisibleTube ? blocksLogic[i].GetColor() : Color.clear;
                _tempWaterSprites[i].gameObject.SetActive(isVisibleTube);
            }

            // if (numBlock > 0)
            // {
            //     // var blocksLogic = tubeLogic.GetBlocks();
            //     for (var index = 0; index < numBlock; index++)
            //     {
            //         var block = blocksLogic[index];
            //         _bottleColors[index] = block.GetColor();
            //     }
            // }
            //
            // if (_bottleMaskSR != null)
            // {
            //     _bottleMaskSR.material.SetFloat(FILL_AMOUNT, _fillAmounts[_numberOfColorsInBottle]);
            // }
            //
            // UpdateColorsOnShader();
            //
            // UpdateTopColorValues();
        }

        public void Select()
        {
            // UpdateTopColorValues();
            MoveUp();
        }

        private void MoveUp()
        {
            _moveBottleUpTween = transform.DOMoveY(_moveUpOffsetFromOriginY, _moveUpDuration);
        }

        public void Deselect()
        {
            // UpdateTopColorValues();
            MoveDown();
        }

        private void MoveDown()
        {
            transform.DOMoveY(_originY, _moveUpDuration);
        }

        public IEnumerator StartColorTransfer(Bottle secondBottle)
        {
            yield break;

            _bottleToFill = secondBottle;
            ChooseRotationAndDirection();
            NumberOfColorsToTransfer = Mathf.Min(NumberOfTopColorLayers, 4 - _bottleToFill._numberOfColorsInBottle);

            for (var i = 0; i < NumberOfColorsToTransfer; i++)
            {
                _bottleToFill._bottleColors[_bottleToFill._numberOfColorsInBottle + i] = _topColor;
            }

            _bottleToFill.UpdateColorsOnShader();
            CalculateRotationIndex(_bottleColors.Length - _bottleToFill._numberOfColorsInBottle);
            MoveForwardToAnimate();
            yield return StartCoroutine(MoveToBottleToFill());
        }

        public void MoveForwardToAnimate()
        {
            _spriteRenderer.sortingOrder += 2;
            _bottleMaskSR.sortingOrder += 2;
        }

        public void MoveBackwardAfterAnimate()
        {
            _spriteRenderer.sortingOrder -= 2;
            _bottleMaskSR.sortingOrder -= 2;
        }

        public void MoveBackwardAfterFillColor()
        {
            _fillRenderer.sortingOrder = 0;
            _spriteRenderer.sortingOrder = 1;
        }

        public void MoveForwardToFillColor()
        {
            _fillRenderer.sortingOrder = 2;
            _spriteRenderer.sortingOrder = 3;
        }

        private void CalculateRotationIndex(int numberOfEmptySpaceInSecondBottle)
        {
            RotationIndex = _bottleColors.Length - 1 - (_numberOfColorsInBottle - Mathf.Min(
                numberOfEmptySpaceInSecondBottle,
                NumberOfTopColorLayers));
        }

        private void ChooseRotationAndDirection()
        {
            var isLeftSideOfBottleToFill = transform.position.x > _bottleToFill.transform.position.x;
            choosenRotationPoint = isLeftSideOfBottleToFill
                ? leftRotationPoint
                : rightRotationPoint;
            directionMultiplier = isLeftSideOfBottleToFill ? -1 : 1;
        }

        private IEnumerator MoveToBottleToFill()
        {
            startposition = transform.position;
            endPosition = choosenRotationPoint == leftRotationPoint
                ? _bottleToFill.rightRotationPoint.position
                : _bottleToFill.leftRotationPoint.position;

            PauseMoveUp();

            float t = 0;
            while (t <= 1)
            {
                transform.position = Vector3.Lerp(startposition, endPosition, t);
                t += Time.deltaTime * _bottleMovementSpeed;

                yield return new WaitForEndOfFrame();
            }

            transform.position = endPosition;
            yield return StartCoroutine(Rotate());
        }

        public void PauseMoveUp()
        {
            _moveBottleUpTween.Pause();
        }

        private IEnumerator Rotate()
        {
            float t = 0;
            float lerpValue;
            float angleValue;
            float lastAngleValue = 0;

            while (t < _timeToRotate)
            {
                lerpValue = t / _timeToRotate;
                angleValue = Mathf.Lerp(0.0f, directionMultiplier * _rotationValues[RotationIndex], lerpValue);

                transform.RotateAround(choosenRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);

                _bottleMaskSR.material.SetFloat(SARM, _scaleAndRotationMultiplierCurve.Evaluate(angleValue));

                if (_fillAmounts[_numberOfColorsInBottle] > _fillAmountCurve.Evaluate(angleValue) + 0.005f)
                {
                    if (_linePouringWaterRenderer != null && _linePouringWaterRenderer.enabled == false)
                    {
                        _topColor.a = 200;
                        _linePouringWaterRenderer.startColor = _topColor;
                        _linePouringWaterRenderer.endColor = _topColor;
                        _linePouringWaterRenderer.SetPosition(0, choosenRotationPoint.position);
                        _linePouringWaterRenderer.SetPosition(1, choosenRotationPoint.position - Vector3.up * 1.45f);
                        _bottleToFill._fillRenderer.sortingOrder = 2;
                        _bottleToFill._spriteRenderer.sortingOrder = 3;
                        _linePouringWaterRenderer.enabled = true;
                    }

                    PlayPouringSound();
                    _bottleMaskSR.material.SetFloat(FILL_AMOUNT, _fillAmountCurve.Evaluate(angleValue));

                    _bottleToFill.FillUp(_fillAmountCurve.Evaluate(lastAngleValue) -
                                         _fillAmountCurve.Evaluate(angleValue));
                }

                t += Time.deltaTime * _rotationSpeedMultiplierCurve.Evaluate(angleValue) *
                     bottleRotateSpeed; // the old one nothing
                lastAngleValue = angleValue;
                yield return new WaitForEndOfFrame();
            }

            angleValue = directionMultiplier * _rotationValues[RotationIndex];
            _bottleMaskSR.material.SetFloat(SARM, _scaleAndRotationMultiplierCurve.Evaluate(angleValue));
            _bottleMaskSR.material.SetFloat(FILL_AMOUNT, _fillAmountCurve.Evaluate(angleValue));

            _numberOfColorsInBottle -= NumberOfColorsToTransfer;
            _bottleToFill._numberOfColorsInBottle += NumberOfColorsToTransfer;

            _bottleToFill.Fulfill();
            StopPouringSound();
            if (_linePouringWaterRenderer != null)
            {
                _linePouringWaterRenderer.enabled = false;
            }

            _bottleToFill._fillRenderer.sortingOrder = 0;
            _bottleToFill._spriteRenderer.sortingOrder = 1;

            yield return StartCoroutine(RotateBack());
        }

        private IEnumerator RotateBack()
        {
            float t = 0;
            float lerpValue;
            float angleValue;

            var lastAngleValue = directionMultiplier * _rotationValues[RotationIndex];

            while (t < _timeToRotate)
            {
                lerpValue = t / _timeToRotate;
                angleValue = Mathf.Lerp(directionMultiplier * _rotationValues[RotationIndex], 0.0f, lerpValue);

                transform.RotateAround(choosenRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);

                _bottleMaskSR.material.SetFloat(SARM, _scaleAndRotationMultiplierCurve.Evaluate(angleValue));

                lastAngleValue = angleValue;

                t += Time.deltaTime * bottleRotateSpeed;

                yield return new WaitForEndOfFrame();
            }

            UpdateTopColorValues();
            angleValue = 0f;
            transform.eulerAngles = new Vector3(0, 0, angleValue);
            _bottleMaskSR.material.SetFloat(SARM, _scaleAndRotationMultiplierCurve.Evaluate(angleValue));

            yield return StartCoroutine(MoveBackToOriginPosition());
        }

        public void UpdateTopColorValues()
        {
            if (_numberOfColorsInBottle == 0)
            {
                return;
            }

            NumberOfTopColorLayers = 1;
            _topColor = _bottleColors[_numberOfColorsInBottle - 1];

            for (var i = _numberOfColorsInBottle - 1; i >= 1; i--)
            {
                if (_bottleColors[i].Equals(_bottleColors[i - 1]))
                {
                    NumberOfTopColorLayers++;
                }
                else
                {
                    break;
                }
            }

            if (_numberOfColorsInBottle == 4 && NumberOfTopColorLayers == _numberOfColorsInBottle)
            {
                Debug.Log("it's full from the update top colors function");
            }

            RotationIndex = _bottleColors.Length - 1 - (_numberOfColorsInBottle - NumberOfTopColorLayers);
        }

        public void SetRotationIndex(int index)
        {
            RotationIndex = index;
        }

        private IEnumerator MoveBackToOriginPosition()
        {
            startposition = transform.position;
            endPosition = OriginalPosition;
            float t = 0;
            while (t <= 1)
            {
                transform.position = Vector3.Lerp(startposition, endPosition, t);
                t += Time.deltaTime * _bottleMovementSpeed;

                yield return new WaitForEndOfFrame();
            }

            transform.position = endPosition;

            _spriteRenderer.sortingOrder -= 2;
            _bottleMaskSR.sortingOrder -= 2;
        }

        public void StopPouringSound()
        {
            // throw new NotImplementedException();
            pouringSound.Stop();
            isPouringSoundPlaying = false;
        }

        public void Fulfill()
        {
            // throw new NotImplementedException();
        }

        public void FillUp(float fillAmountToAdd)
        {
            Refill(_bottleMaskSR.material.GetFloat(FILL_AMOUNT) + fillAmountToAdd);
        }

        public void Refill(float amount)
        {
            _bottleMaskSR.material.SetFloat(FILL_AMOUNT, amount);
        }

        private void PlayPouringSound()
        {
            // throw new NotImplementedException();
            if (isPouringSoundPlaying)
            {
                return;
            }

            isPouringSoundPlaying = true;
            SoundController.Instance.PlayOneAudioSource(pouringSound);
        }

        public IEnumerator PlayIncompletePouringSound()
        {
            PlayPouringSound();
            float t = 0;
            while (t <= timeToPlayPouringSound)
            {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            StopPouringSound();
        }

        private void GetPouringSound()
        {
            pouringSound = Instantiate(PouringSoundPrefab, transform);
            SoundController.Instance.AddAudioSource(pouringSound);
            pouringSound.Stop();
        }
    }
}
