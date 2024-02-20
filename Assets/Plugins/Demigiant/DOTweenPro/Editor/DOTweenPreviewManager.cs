﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/03/12 16:03

using System.Collections.Generic;
using DG.DemiEditor;
using DG.DemiLib;
using DG.Tweening;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG.DOTweenEditor
{
    public static class DOTweenPreviewManager
    {
        private static bool _previewOnlyIfSetToAutoPlay = true;
        private static readonly Dictionary<DOTweenAnimation, TweenInfo> _AnimationToTween = new();
        private static readonly List<DOTweenAnimation> _TmpKeys = new();

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ INTERNAL CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        private class TweenInfo
        {
            public readonly DOTweenAnimation animation;
            public readonly bool isFrom;
            public readonly Tween tween;

            public TweenInfo(DOTweenAnimation animation, Tween tween, bool isFrom)
            {
                this.animation = animation;
                this.tween = tween;
                this.isFrom = isFrom;
            }
        }

        private static class Styles
        {
            private static bool _initialized;

            public static GUIStyle previewBox, previewLabel, btOption, btPreview, previewStatusLabel;

            public static void Init()
            {
                if (_initialized)
                {
                    return;
                }

                _initialized = true;

                previewBox = new GUIStyle(GUI.skin.box).Clone().Padding(1, 1, 0, 3)
                    .Background(DeStylePalette.squareBorderCurved_darkBorders).Border(7, 7, 7, 7);
                previewLabel = new GUIStyle(GUI.skin.label).Clone(10, FontStyle.Bold).Padding(1, 0, 3, 0).Margin(3, 6, 0, 0)
                    .StretchWidth(false);
                btOption = DeGUI.styles.button.bBlankBorderCompact.MarginBottom(2).MarginRight(4);
                btPreview = EditorStyles.miniButton.Clone(Format.RichText);
                previewStatusLabel = EditorStyles.miniLabel.Clone().Padding(4, 0, 0, 0).Margin(0);
            }
        }

        #region Public Methods & GUI

        /// <summary>
        ///     Returns TRUE if its actually previewing animations
        /// </summary>
        public static bool PreviewGUI(DOTweenAnimation src)
        {
            if (EditorApplication.isPlaying)
            {
                return false;
            }

            Styles.Init();

            var isPreviewing = _AnimationToTween.Count > 0;
            var isPreviewingThis = isPreviewing && _AnimationToTween.ContainsKey(src);

            // Preview in editor
            GUI.backgroundColor = isPreviewing
                ? new DeSkinColor(new Color(0.49f, 0.8f, 0.86f), new Color(0.15f, 0.26f, 0.35f))
                : new DeSkinColor(Color.white, new Color(0.13f, 0.13f, 0.13f));
            GUILayout.BeginVertical(Styles.previewBox);
            DeGUI.ResetGUIColors();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Preview Mode - Experimental", Styles.previewLabel);
            _previewOnlyIfSetToAutoPlay = DeGUILayout.ToggleButton(
                _previewOnlyIfSetToAutoPlay,
                new GUIContent("AutoPlay only", "If toggled only previews animations that have AutoPlay turned ON"),
                Styles.btOption
            );
            GUILayout.EndHorizontal();
            GUILayout.Space(1);
            // Preview - Play
            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(
                isPreviewingThis || src.animationType == DOTweenAnimation.AnimationType.None
                                 || !src.isActive || _previewOnlyIfSetToAutoPlay && !src.autoPlay
            );
            if (GUILayout.Button("► Play", Styles.btPreview))
            {
                if (!isPreviewing)
                {
                    StartupGlobalPreview();
                }
                AddAnimationToGlobalPreview(src);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(isPreviewing);
            if (GUILayout.Button("► Play All <i>on GameObject</i>", Styles.btPreview))
            {
                if (!isPreviewing)
                {
                    StartupGlobalPreview();
                }
                var anims = src.gameObject.GetComponents<DOTweenAnimation>();
                foreach (var anim in anims)
                {
                    AddAnimationToGlobalPreview(anim);
                }
            }
            if (GUILayout.Button("► Play All <i>in Scene</i>", Styles.btPreview))
            {
                if (!isPreviewing)
                {
                    StartupGlobalPreview();
                }
                var anims = Object.FindObjectsOfType<DOTweenAnimation>();
                foreach (var anim in anims)
                {
                    AddAnimationToGlobalPreview(anim);
                }
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            // Preview - Stop
            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!isPreviewingThis);
            if (GUILayout.Button("■ Stop", Styles.btPreview))
            {
                if (_AnimationToTween.ContainsKey(src))
                {
                    StopPreview(_AnimationToTween[src].tween);
                }
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(!isPreviewing);
            if (GUILayout.Button("■ Stop All <i>on GameObject</i>", Styles.btPreview))
            {
                StopPreview(src.gameObject);
            }
            if (GUILayout.Button("■ Stop All <i>in Scene</i>", Styles.btPreview))
            {
                StopAllPreviews();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            if (isPreviewing)
            {
                var playingTweens = 0;
                var completedTweens = 0;
                var pausedTweens = 0;
                foreach (var kvp in _AnimationToTween)
                {
                    var t = kvp.Value.tween;
                    if (t.IsPlaying())
                    {
                        playingTweens++;
                    }
                    else if (t.IsComplete())
                    {
                        completedTweens++;
                    }
                    else
                    {
                        pausedTweens++;
                    }
                }
                GUILayout.Label("Playing Tweens: " + playingTweens, Styles.previewStatusLabel);
                GUILayout.Label("Completed Tweens: " + completedTweens, Styles.previewStatusLabel);
//                GUILayout.Label("Paused Tweens: " + playingTweens);
            }
            GUILayout.EndVertical();

            return isPreviewing;
        }

#if !(UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5)
        public static void StopAllPreviews(PlayModeStateChange state)
        {
            StopAllPreviews();
        }
#endif

        public static void StopAllPreviews()
        {
            _TmpKeys.Clear();
            foreach (var kvp in _AnimationToTween)
            {
                _TmpKeys.Add(kvp.Key);
            }
            StopPreview(_TmpKeys);
            _TmpKeys.Clear();
            _AnimationToTween.Clear();

            DOTweenEditorPreview.Stop();
#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5
            UnityEditor.EditorApplication.playmodeStateChanged -= StopAllPreviews;
#else
            EditorApplication.playModeStateChanged -= StopAllPreviews;
#endif
//            EditorApplication.playmodeStateChanged -= StopAllPreviews;

            InternalEditorUtility.RepaintAllViews();
        }

#endregion

#region Methods

        private static void StartupGlobalPreview()
        {
            DOTweenEditorPreview.Start();
#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5
            UnityEditor.EditorApplication.playmodeStateChanged += StopAllPreviews;
#else
            EditorApplication.playModeStateChanged += StopAllPreviews;
#endif
//            EditorApplication.playmodeStateChanged += StopAllPreviews;
        }

        private static void AddAnimationToGlobalPreview(DOTweenAnimation src)
        {
            if (!src.isActive)
            {
                return; // Ignore sources whose tweens have been set to inactive
            }
            if (_previewOnlyIfSetToAutoPlay && !src.autoPlay)
            {
                return;
            }

            var t = src.CreateEditorPreview();
            if (t == null)
            {
                return;
            }
            _AnimationToTween.Add(src, new TweenInfo(src, t, src.isFrom));
            // Tween setup
            DOTweenEditorPreview.PrepareTweenForPreview(t);
        }

        private static void StopPreview(GameObject go)
        {
            _TmpKeys.Clear();
            foreach (var kvp in _AnimationToTween)
            {
                if (kvp.Key.gameObject != go)
                {
                    continue;
                }
                _TmpKeys.Add(kvp.Key);
            }
            StopPreview(_TmpKeys);
            _TmpKeys.Clear();

            if (_AnimationToTween.Count == 0)
            {
                StopAllPreviews();
            }
            else
            {
                InternalEditorUtility.RepaintAllViews();
            }
        }

        private static void StopPreview(Tween t)
        {
            TweenInfo tInfo = null;
            foreach (var kvp in _AnimationToTween)
            {
                if (kvp.Value.tween != t)
                {
                    continue;
                }
                tInfo = kvp.Value;
                _AnimationToTween.Remove(kvp.Key);
                break;
            }
            if (tInfo == null)
            {
                Debug.LogWarning("DOTween Preview ► Couldn't find tween to stop");
                return;
            }
            if (tInfo.isFrom)
            {
                var totLoops = tInfo.tween.Loops();
                if (totLoops < 0 || totLoops > 1)
                {
                    tInfo.tween.Goto(tInfo.tween.Duration(false));
                }
                else
                {
                    tInfo.tween.Complete();
                }
            }
            else
            {
                tInfo.tween.Rewind();
            }
            tInfo.tween.Kill();
            EditorUtility.SetDirty(tInfo.animation); // Refresh views

            if (_AnimationToTween.Count == 0)
            {
                StopAllPreviews();
            }
            else
            {
                InternalEditorUtility.RepaintAllViews();
            }
        }

        // Stops while iterating inversely, which deals better with tweens that overwrite each other
        private static void StopPreview(List<DOTweenAnimation> keys)
        {
            for (var i = keys.Count - 1; i > -1; --i)
            {
                var anim = keys[i];
                var tInfo = _AnimationToTween[anim];
                if (tInfo.isFrom)
                {
                    var totLoops = tInfo.tween.Loops();
                    if (totLoops < 0 || totLoops > 1)
                    {
                        tInfo.tween.Goto(tInfo.tween.Duration(false));
                    }
                    else
                    {
                        tInfo.tween.Complete();
                    }
                }
                else
                {
                    tInfo.tween.Rewind();
                }
                tInfo.tween.Kill();
                EditorUtility.SetDirty(anim); // Refresh views
                _AnimationToTween.Remove(anim);
            }
        }

#endregion
    }
}