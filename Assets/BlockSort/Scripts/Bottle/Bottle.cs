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

        private int _bottleIndex = -1;

        private Bottle _bottleToFill;

        private Tween _moveBottleUpTween;
        private float _moveUpOffsetFromOriginY;

        private float _originY;

        private Transform choosenRotationPoint;
        private float directionMultiplier = 1.0f;
        private Vector3 endPosition;

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
            MoveDown();
        }

        private void MoveDown()
        {
            transform.DOMoveY(_originY, _moveUpDuration);
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
        public void PauseMoveUp()
        {
            _moveBottleUpTween.Pause();
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
    }
}
