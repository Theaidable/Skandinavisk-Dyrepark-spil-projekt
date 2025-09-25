#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundBank))]
public class SoundBankEditor : Editor
{
    private const string BaseFolder = "Assets/Content/Audio";
    private enum AudioFolder { BGM, SFX }
    private AudioFolder _target = AudioFolder.SFX; // default

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Importer lyd", EditorStyles.boldLabel);

        _target = (AudioFolder)EditorGUILayout.EnumPopup("Importér til mappe", _target);

        if (GUILayout.Button("Tilføj lyd til valgte mappe"))
        {
            ImportAudioToFolder(GetTargetFolder());
        }
    }
    private string GetTargetFolder()
    {
        var sub = _target.ToString(); // "BGM" eller "SFX"
        return $"{BaseFolder}/{sub}";
    }

    private void ImportAudioToFolder(string targetFolder)
    {
        EnsureFolderHierarchy(BaseFolder, targetFolder);

        string path = EditorUtility.OpenFilePanelWithFilters("Vælg lyd", "", new[] { "Audio", "wav,mp3,ogg,aiff,aif" });

        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        string fileName = Path.GetFileName(path);
        string rawTarget = Path.Combine(targetFolder, fileName).Replace('\\', '/');
        string targetPath = AssetDatabase.GenerateUniqueAssetPath(rawTarget);

        if (!path.StartsWith(Application.dataPath))
        {
            FileUtil.CopyFileOrDirectory(path, targetPath);
            AssetDatabase.Refresh();
        }
        else
        {
            string projRel = "Assets" + path.Substring(Application.dataPath.Length);
            if (projRel != targetPath)
            {
                if (!AssetDatabase.CopyAsset(projRel, targetPath))
                {
                    Debug.LogError("Kunne ikke kopiere asset.");
                    return;
                }
            }
        }

        var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(targetPath);
        if (clip == null)
        {
            Debug.LogError("Import lykkedes, men asset blev ikke genkendt som AudioClip.");
            return;
        }

        Debug.Log($"Importerede '{Path.GetFileName(targetPath)}' til {targetFolder}.");
    }

    static void EnsureFolderHierarchy(string baseFolder, string fullFolder)
    {
        if (!AssetDatabase.IsValidFolder(baseFolder))
        {
            string parent = Path.GetDirectoryName(baseFolder)?.Replace('\\', '/') ?? "Assets";
            string leaf = Path.GetFileName(baseFolder);
            AssetDatabase.CreateFolder(parent, leaf);
        }

        if (!AssetDatabase.IsValidFolder(fullFolder))
        {
            string parent = Path.GetDirectoryName(fullFolder)!.Replace('\\', '/');
            string leaf = Path.GetFileName(fullFolder);
            AssetDatabase.CreateFolder(parent, leaf);
        }
    }
}

#endif