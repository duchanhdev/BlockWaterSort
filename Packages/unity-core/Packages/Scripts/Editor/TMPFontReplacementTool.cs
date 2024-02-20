using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using UnityEngine.Video;

namespace Egitech.Core.Editor
{
    /// <summary>
    ///     Data structure containing a list of target and replacement fileID and GUID requiring remapping from previous
    ///     versions of TextMesh Pro to the new TextMesh Pro UPM package.
    ///     This data structure is populated with the data contained in the PackageConversionData.json file included in the
    ///     package.
    /// </summary>
    /*
    [System.Serializable]
    class AssetConversionData
    {
        public List<AssetConversionRecord> assetRecords;
    }
    */
    public sealed class TMPFontReplacementTool : EditorWindow
    {
        private const string K_PROJECT_SCAN_REPORT_DEFAULT_TEXT =
            "<color=#FFFF80><b>Project Scan Results</b></color>\n";

        private static readonly HashSet<Type> M_IGNORE_ASSET_TYPES = new()
        {
            typeof(AnimatorOverrideController),
            typeof(AudioClip),
            typeof(AvatarMask),
            typeof(ComputeShader),
            typeof(Cubemap),
            typeof(DefaultAsset),
            typeof(Flare),
            typeof(Font),
            typeof(GUISkin),
            typeof(HumanTemplate),
            typeof(LightingDataAsset),
            typeof(Mesh),
            typeof(MonoScript),
            typeof(PhysicMaterial),
            typeof(PhysicsMaterial2D),
            typeof(RenderTexture),
            typeof(Shader),
            typeof(TerrainData),
            typeof(TextAsset),
            typeof(Texture2D),
            typeof(Texture2DArray),
            typeof(Texture3D),
            typeof(AssemblyDefinitionAsset),
            typeof(NavMeshData),
            typeof(Tile),
            typeof(SpriteAtlas),
            typeof(VideoClip)
        };

        private static string _mProjectPath;
        private static string _mProjectFolderToScan;
        private static TMP_FontAsset _mFontAssetToScanFor;
        private static TMP_FontAsset _mFontAssetToReplaceWith;

        private static AssetConversionRecord _mFontAssetRecord;

        // variables to keep track of and display the material presets for font assets - future add.
        //private static Material[] m_MaterialPresetsToScanFor;
        //private static int m_ScanForIndex;
        //private static Material[] m_MaterialPresetsToReplaceWith;
        //private static int m_ReplaceWithIndex;
        private static bool _mIsAlreadyScanningProject;
        private static bool _mCancelScanProcess;
        private static string _kProjectScanLabelPrefix = "Scanning: ";
        private static string _mProjectScanResults = string.Empty;
        private static Vector2 _mProjectScanResultScrollPosition;
        private static float _mProgressPercentage;

        private static int _mScanningTotalFiles;
        private static int _mRemainingFilesToScan;
        private static int _mScanningCurrentFileIndex;
        private static string _mScanningCurrentFileName;

        //private static AssetConversionData m_ConversionData;

        private static readonly List<AssetModificationRecord> M_MODIFIED_ASSET_LIST = new();

        private void OnEnable()
        {
            // Set Editor Window Size
            SetEditorWindowSize();

            _mProjectScanResults = K_PROJECT_SCAN_REPORT_DEFAULT_TEXT;
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                // Scan project files and resources
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.Label("Select Font Assets", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Select Font Asset To Scan For:");
                    _mFontAssetToScanFor =
                        EditorGUILayout.ObjectField(_mFontAssetToScanFor, typeof(TMP_FontAsset),
                            false) as TMP_FontAsset;
                    GUILayout.EndHorizontal();
                    // Beginnings of being able to also select a material preset to scan for, needs more investigation
                    /*
                    if (m_FontAssetToScanFor != null)
                    {
                        m_MaterialPresetsToScanFor = TMP_EditorUtility.FindMaterialReferences(m_FontAssetToScanFor);
                        String[] presets = new string[m_MaterialPresetsToScanFor.Length];
                        // probably shouldn't run for loop in here...added it to see if material references would be displayed correctly.
                        for (int i = 0; i < m_MaterialPresetsToScanFor.Length; i++)
                        {
                            presets[i] = m_MaterialPresetsToScanFor[i].name;
                        }
                        m_ScanForIndex = EditorGUILayout.Popup("Material Presets", m_ScanForIndex, presets);
                    }*/
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Select Font Asset to Replace With:");
                    _mFontAssetToReplaceWith =
                        EditorGUILayout.ObjectField(_mFontAssetToReplaceWith, typeof(TMP_FontAsset), false) as
                            TMP_FontAsset;
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                    GUILayout.Label("Scan Project Files", EditorStyles.boldLabel);
                    GUILayout.Label(
                        "Press the <i>Scan Project Files</i> button to begin scanning your project for files & resources that were created with the selected Font Asset.",
                        TMP_UIStyleManager.label);
                    GUILayout.Space(10f);
                    GUILayout.Label(
                        "Project folder to be scanned. Example \"Assets/TextMesh Pro\" If empty, will scan \"Assets/\"");
                    _mProjectFolderToScan = EditorGUILayout.TextField("Folder Path:      ", _mProjectFolderToScan);
                    GUILayout.Space(5f);

                    if (_mFontAssetToScanFor != null && _mFontAssetToReplaceWith != null &&
                        _mIsAlreadyScanningProject == false)
                    {
                        GUI.enabled = true;
                    }
                    else
                    {
                        GUI.enabled = false;
                    }
                    // GUI.enabled = true;
                    if (GUILayout.Button("Scan Project Files"))
                    {
                        var scanPath = AssetDatabase.GetAssetPath(_mFontAssetToScanFor);
                        _mFontAssetRecord.target = AssetDatabase.GUIDFromAssetPath(scanPath).ToString();
                        var replacePath = AssetDatabase.GetAssetPath(_mFontAssetToReplaceWith);
                        _mFontAssetRecord.replacement = AssetDatabase.GUIDFromAssetPath(replacePath).ToString();
                        _mCancelScanProcess = false;

                        // Make sure Asset Serialization mode is set to ForceText and Version Control mode to Visible Meta Files.
                        if (CheckProjectSerializationAndSourceControlModes())
                        {
                            _mProjectPath = Path.GetFullPath("Assets/..");
                            TMP_EditorCoroutine.StartCoroutine(ScanProjectFiles());
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Project Settings Change Required",
                                "In menu options \"Edit - Project Settings - Editor\", please change Asset Serialization Mode to ForceText and Source Control Mode to Visible Meta Files.",
                                "OK", string.Empty);
                        }
                    }

                    GUI.enabled = true;

                    // Display progress bar
                    var rect = GUILayoutUtility.GetRect(0f, 20f, GUILayout.ExpandWidth(true));
                    EditorGUI.ProgressBar(rect, _mProgressPercentage,
                        "Scan Progress (" + _mScanningCurrentFileIndex + "/" + _mScanningTotalFiles + ")");

                    // Display cancel button and name of file currently being scanned.
                    if (_mIsAlreadyScanningProject)
                    {
                        var cancelRect = new Rect(rect.width - 20, rect.y + 2, 20, 16);
                        if (GUI.Button(cancelRect, "X"))
                        {
                            _mCancelScanProcess = true;
                        }
                        GUILayout.Label(_kProjectScanLabelPrefix + _mScanningCurrentFileName, TMP_UIStyleManager.label);
                    }
                    else
                    {
                        GUILayout.Label(string.Empty);
                    }

                    GUILayout.Space(5);

                    // Creation Feedback
                    GUILayout.BeginVertical(TMP_UIStyleManager.textAreaBoxWindow, GUILayout.ExpandHeight(true));
                    {
                        _mProjectScanResultScrollPosition =
                            EditorGUILayout.BeginScrollView(_mProjectScanResultScrollPosition,
                                GUILayout.ExpandHeight(true));
                        GUILayout.Label(_mProjectScanResults, TMP_UIStyleManager.label);
                        EditorGUILayout.EndScrollView();
                    }
                    GUILayout.EndVertical();
                    GUILayout.Space(5f);
                }
                GUILayout.EndVertical();

                // Scan project files and resources
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.Label("Save Modified Project Files", EditorStyles.boldLabel);
                    GUILayout.Label(
                        "Pressing the <i>Save Modified Project Files</i> button will update the files in the <i>Project Scan Results</i> listed above. <color=#FFFF80>Please make sure that you have created a backup of your project first</color> as these file modifications are permanent and cannot be undone.",
                        TMP_UIStyleManager.label);
                    GUILayout.Space(5f);
                    GUILayout.Label("Unless using source control, then you can just revert/discard changes.",
                        TMP_UIStyleManager.label);
                    GUILayout.Space(5f);

                    GUI.enabled = _mIsAlreadyScanningProject == false && M_MODIFIED_ASSET_LIST.Count > 0 ? true : false;
                    if (GUILayout.Button("Save Modified Project Files"))
                    {
                        UpdateProjectFiles();
                    }
                    GUILayout.Space(10f);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            GUILayout.Space(5f);
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        // Create Project Files GUID Remapping Tool window
        [MenuItem("Window/TextMeshPro/Project Font Replacement Tool", false, 2120)]
        private static void ShowConverterWindow()
        {
            var window = GetWindow<TMPFontReplacementTool>();
            window.titleContent = new GUIContent("Font Replacement Tool");
            window.Focus();
        }

        /// <summary>
        ///     Limits the minimum size of the editor window.
        /// </summary>
        private void SetEditorWindowSize()
        {
            EditorWindow editorWindow = this;

            var currentWindowSize = editorWindow.minSize;

            editorWindow.minSize =
                new Vector2(Mathf.Max(640, currentWindowSize.x), Mathf.Max(420, currentWindowSize.y));
        }

        /// <summary>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool ShouldIgnoreFile(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var fileType = AssetDatabase.GetMainAssetTypeAtPath(filePath);

            if (M_IGNORE_ASSET_TYPES.Contains(fileType))
            {
                return true;
            }

            // Exclude FBX
            return fileType == typeof(GameObject) && fileExtension.ToLower() is ".fbx" or ".blend";
        }

        private IEnumerator ScanProjectFiles()
        {
            _mIsAlreadyScanningProject = true;
            var packageFullPath = TMP_EditorUtility.packageFullPath;

            // List containing assets that have been modified.
            _mProjectScanResults = K_PROJECT_SCAN_REPORT_DEFAULT_TEXT;
            M_MODIFIED_ASSET_LIST.Clear();
            _mProgressPercentage = 0;

            // Read Conversion Data from Json file.
            /*
            if (m_ConversionData == null)
                m_ConversionData = JsonUtility.FromJson<AssetConversionData>(File.ReadAllText(packageFullPath + "/PackageConversionData.json"));
            */

            // Get list of GUIDs for assets that might contain references to previous GUIDs that require updating.
            var searchFolder = string.IsNullOrEmpty(_mProjectFolderToScan) ? "Assets" : _mProjectFolderToScan;
            var guids = AssetDatabase.FindAssets("t:Object", new[] { searchFolder }).Distinct().ToArray();

            _kProjectScanLabelPrefix = "<b>Phase 1 - Filtering:</b> ";
            _mScanningTotalFiles = guids.Length;
            _mScanningCurrentFileIndex = 0;

            var projectFilesToScan = new List<AssetFileRecord>();

            foreach (var guid in guids)
            {
                if (_mCancelScanProcess)
                {
                    break;
                }

                var assetFilePath = AssetDatabase.GUIDToAssetPath(guid);

                _mScanningCurrentFileIndex += 1;
                _mScanningCurrentFileName = assetFilePath;
                _mProgressPercentage = (float)_mScanningCurrentFileIndex / _mScanningTotalFiles;

                // Filter out file types we have no interest in searching
                if (ShouldIgnoreFile(assetFilePath))
                {
                    continue;
                }

                var assetMetaFilePath = AssetDatabase.GetTextMetaFilePathFromAssetPath(assetFilePath);

                projectFilesToScan.Add(new AssetFileRecord(assetFilePath, assetMetaFilePath));

                yield return null;
            }

            _mRemainingFilesToScan = _mScanningTotalFiles = projectFilesToScan.Count;

            _kProjectScanLabelPrefix = "<b>Phase 2 - Scanning:</b> ";

            for (var i = 0; i < _mScanningTotalFiles; i++)
            {
                if (_mCancelScanProcess)
                {
                    break;
                }

                var fileRecord = projectFilesToScan[i];

                ThreadPool.QueueUserWorkItem(task =>
                {
                    ScanProjectFileAsync(fileRecord);

                    _mScanningCurrentFileName = fileRecord.AssetFilePath;

                    var completedScans = _mScanningTotalFiles - Interlocked.Decrement(ref _mRemainingFilesToScan);

                    _mScanningCurrentFileIndex = completedScans;
                    _mProgressPercentage = (float)completedScans / _mScanningTotalFiles;
                });

                if (i % 64 == 0)
                {
                    yield return new WaitForSeconds(2.0f);
                }
            }

            while (_mRemainingFilesToScan > 0 && !_mCancelScanProcess)
            {
                yield return null;
            }

            _mIsAlreadyScanningProject = false;
            _mScanningCurrentFileName = string.Empty;
        }

        private static void ScanProjectFileAsync(AssetFileRecord fileRecord)
        {
            if (_mCancelScanProcess)
            {
                return;
            }

            // Read the asset data file
            var assetDataFile = string.Empty;
            var hasFileChanged = false;

            try
            {
                assetDataFile = File.ReadAllText(_mProjectPath + "/" + fileRecord.AssetFilePath);
            }
            catch
            {
                // Continue to the next asset if we can't read the current one.
                return;
            }

            // Read the asset meta data file
            var assetMetaFile = File.ReadAllText(_mProjectPath + "/" + fileRecord.AssetMetaFilePath);
            var hasMetaFileChanges = false;

            /*
            foreach (AssetConversionRecord record in m_ConversionData.assetRecords)
            {
                if (assetDataFile.Contains(record.target))
                {
                    hasFileChanged = true;

                    assetDataFile = assetDataFile.Replace(record.target, record.replacement);
                }

                //// Check meta file
                if (assetMetaFile.Contains(record.target))
                {
                    hasMetaFileChanges = true;

                    assetMetaFile = assetMetaFile.Replace(record.target, record.replacement);
                }
            }
            */
            // We could add more logic here if we start swapping font materials as well
            // We could either default to the new font's default material (same guid) or
            // start adding object fields for swapping to a new material as well in the Editor Window.
            // These would likely be controlled with some toggles to determine what we want to swap.
            // Currently we are only looking for guid (target) and replacing it with the new guid (replacement)
            if (assetDataFile.Contains(_mFontAssetRecord.target))
            {
                hasFileChanged = true;
                assetDataFile = assetDataFile.Replace(_mFontAssetRecord.target, _mFontAssetRecord.replacement);
            }

            if (assetMetaFile.Contains(_mFontAssetRecord.target))
            {
                hasMetaFileChanges = true;
                assetMetaFile =
                    assetMetaFile.Replace(_mFontAssetRecord.target, _mFontAssetRecord.replacement);
            }

            if (hasFileChanged)
            {
                AssetModificationRecord modifiedAsset;
                modifiedAsset.AssetFilePath = fileRecord.AssetFilePath;
                modifiedAsset.AssetDataFile = assetDataFile;

                M_MODIFIED_ASSET_LIST.Add(modifiedAsset);

                _mProjectScanResults += fileRecord.AssetFilePath + "\n";
            }

            if (hasMetaFileChanges)
            {
                AssetModificationRecord modifiedAsset;
                modifiedAsset.AssetFilePath = fileRecord.AssetMetaFilePath;
                modifiedAsset.AssetDataFile = assetMetaFile;

                M_MODIFIED_ASSET_LIST.Add(modifiedAsset);

                _mProjectScanResults += fileRecord.AssetMetaFilePath + "\n";
            }
        }

        /// <summary>
        /// </summary>
        private static void ResetScanProcess()
        {
            _mIsAlreadyScanningProject = false;
            _mScanningCurrentFileName = string.Empty;
            _mProgressPercentage = 0;
            _mScanningCurrentFileIndex = 0;
            _mScanningTotalFiles = 0;
        }

        /// <summary>
        /// </summary>
        private static void UpdateProjectFiles()
        {
            // Make sure Asset Serialization mode is set to ForceText with Visible Meta Files.
            CheckProjectSerializationAndSourceControlModes();

            var projectPath = Path.GetFullPath("Assets/..");

            // Display dialogue to show user a list of project files that will be modified upon their consent.
            if (EditorUtility.DisplayDialog("Save Modified Asset(s)?",
                    "Are you sure you want to save all modified assets?", "YES", "NO"))
            {
                for (var i = 0; i < M_MODIFIED_ASSET_LIST.Count; i++)
                    // Make sure all file streams that might have been opened by Unity are closed.
                    //AssetDatabase.ReleaseCachedFileHandles();
                    //Debug.Log("Writing asset file [" + m_ModifiedAssetList[i].assetFilePath + "].");
                {
                    File.WriteAllText(projectPath + "/" + M_MODIFIED_ASSET_LIST[i].AssetFilePath,
                        M_MODIFIED_ASSET_LIST[i].AssetDataFile);
                }
            }

            AssetDatabase.Refresh();

            _mProgressPercentage = 0;
            _mProjectScanResults = K_PROJECT_SCAN_REPORT_DEFAULT_TEXT;
        }

        /// <summary>
        ///     Check project Asset Serialization and Source Control modes
        /// </summary>
        private static bool CheckProjectSerializationAndSourceControlModes()
        {
            // Check Project Asset Serialization and Visible Meta Files mode.
            return EditorSettings.serializationMode == SerializationMode.ForceText &&
                   VersionControlSettings.mode == "Visible Meta Files";
        }

        /// <summary>
        /// </summary>
        private struct AssetModificationRecord
        {
            public string AssetFilePath;
            public string AssetDataFile;
        }

        private struct AssetFileRecord
        {
            public readonly string AssetFilePath;
            public readonly string AssetMetaFilePath;

            public AssetFileRecord(string filePath, string metaFilePath)
            {
                AssetFilePath = filePath;
                AssetMetaFilePath = metaFilePath;
            }
        }
    }
}