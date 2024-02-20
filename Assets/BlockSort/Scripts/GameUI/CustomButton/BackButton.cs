using BlockSort.GameLogic;
using GoogleMobileAds.Common;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class BackButton : RewardAdButton
    {
        [SerializeField]
        [CanBeNull]
        private TextMeshProUGUI _undoCountText;

        [SerializeField]
        private GameObject _hasUndoCountState;

        [SerializeField]
        private GameObject _hasNoUndoCountState;

        //TODO: Workaround to update count and state when undoCount changed, should use delegate instead
        private float _timer;

        private static Game CurrentGame => GameLogic.GameLogic.GetInstance().GetGame();

        private void Update()
        {
            if (_timer >= 0.5)
            {
                _timer = 0;
                Refresh();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        protected override void Init()
        {
            base.Init();
            Refresh();
        }

        private void Refresh()
        {
            UpdateUndoCountText();
            UpdateState();
        }

        private void UpdateState()
        {
            _hasUndoCountState.gameObject.SetActive(CurrentGame.CanUndo);
            _hasNoUndoCountState.gameObject.SetActive(!CurrentGame.CanUndo);
        }

        private void UpdateUndoCountText()
        {
            if (_undoCountText == null)
            {
                return;
            }

            _undoCountText.text = $"{CurrentGame.UndoCount}";
        }

        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { gameUIManager.BackGameStatus(); });
        }

        protected override bool ShouldShowAds()
        {
            return !CurrentGame.CanUndo;
        }

        protected override void OnAfterAdClosed()
        {
            base.OnAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                GameLogic.GameLogic.GetInstance().GetGame().ResetUndoCount();
                UpdateUndoCountText();
                UpdateState();
            });
        }
    }
}
