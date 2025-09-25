using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GraphicsBank))]
public class GraphicsBankEditor : Editor
{
    private const string BaseFolder = "Assets/Content/Sprites";

    private List<string> _folders;
    private List<string> _labels;
    private int _selectedIndex = 0;

    void OnEnable()
    {
        BuildFolderList();
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Importer sprites", EditorStyles.boldLabel);

        if (_folders == null || _folders.Count == 0)
        {
            BuildFolderList();
        }

        EditorGUI.BeginDisabledGroup(_folders.Count == 0);
        _selectedIndex = EditorGUILayout.Popup(new GUIContent("Importér til mappe"), _selectedIndex, _labels.ToArray());
        EditorGUI.EndDisabledGroup();

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Tilføj Sprite"))
            {
                var targetFolder = (_folders.Count > 0) ? _folders[_selectedIndex] : BaseFolder;
                ImportSpriteToFolder(targetFolder);
            }
            if (GUILayout.Button("Genindlæs mapper"))
            {
                BuildFolderList();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Vælg mappe…"))
            {
                string absStart = Path.GetFullPath(BaseFolder);
                string chosenAbs = EditorUtility.OpenFolderPanel("Vælg målmappe i projektet", absStart, "");
                
                if (!string.IsNullOrEmpty(chosenAbs))
                {
                    string assetPath = AbsoluteToAssetPath(chosenAbs);
                    if (IsUnderBase(assetPath))
                    {
                        if (!IsLeafFolder(assetPath))
                        {
                            EditorUtility.DisplayDialog(
                                "Vælg en undermappe",
                                "Den valgte mappe har undermapper. Vælg venligst en konkret undermappe (leaf) under denne.",
                                "OK"
                            );
                            return;
                        }


                        // sikr at den findes i listen (ellers tilføj midlertidigt)
                        int idx = _folders.IndexOf(assetPath);
                        if (idx < 0)
                        {
                            _folders.Add(assetPath);
                            _labels.Add(RelativeLabel(assetPath));
                            _selectedIndex = _folders.Count - 1;
                        }
                        else _selectedIndex = idx;
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Ugyldig mappe",
                            $"Vælg venligst en mappe under\n{BaseFolder}", "OK");
                    }
                }
            }

            if (GUILayout.Button("Åbn mappe"))
            {
                string path = (_folders.Count > 0) ? _folders[_selectedIndex] : BaseFolder;
                EditorUtility.RevealInFinder(Path.GetFullPath(path));
            }

            if (GUILayout.Button("Ping i Project"))
            {
                string path = (_folders.Count > 0) ? _folders[_selectedIndex] : BaseFolder;
                var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (obj) { Selection.activeObject = obj; EditorGUIUtility.PingObject(obj); }
            }
        }
    }

    private void ImportSpriteToFolder(string targetFolder)
    {
        EnsureFolderExists(targetFolder);

        string path = EditorUtility.OpenFilePanelWithFilters("Vælg Sprite", "", new[] { "Billeder", "png,jpg,jpeg,psd,tif,tiff" });

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

            if(projRel != targetPath)
            {
                if (!AssetDatabase.CopyAsset(projRel, targetPath))
                {
                    Debug.LogError("Kunne ikke kopiere asset.");
                    return;
                }
            }
        }

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(targetPath);

        if(sprite == null)
        {
            Debug.LogError("Kunne ikke load'e sprite");
            return;
        }

        Debug.Log($"Importerede '{Path.GetFileName(targetPath)}' til {targetFolder}.");
    }

    private void BuildFolderList()
    {
        EnsureFolderExists(BaseFolder);

        _folders = new List<string>();
        _labels = new List<string>();

        // Kun tilføj Base (root), hvis den er en leaf (ingen undermapper)
        if (IsLeafFolder(BaseFolder))
        {
            _folders.Add(BaseFolder);
            _labels.Add("(root)");
        }

        // Rekursivt alle subfoldere, men kun leafs
        foreach (var f in GetAllSubfoldersRecursive(BaseFolder))
        {
            if (!IsLeafFolder(f))
            {
                continue; // spring mapper med undermapper over
            }

            _folders.Add(f);
            _labels.Add(RelativeLabel(f));
        }

        if (_selectedIndex < 0 || _selectedIndex >= _folders.Count)
        {
            _selectedIndex = 0;
        }
    }

    private static IEnumerable<string> GetAllSubfoldersRecursive(string root)
    {
        // Brug Unitys API for at få Asset-sti’er
        var queue = new Queue<string>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var subs = AssetDatabase.GetSubFolders(current);
            
            foreach (var s in subs)
            {
                yield return s;
                queue.Enqueue(s);
            }
        }
    }

    private static void EnsureFolderExists(string folder)
    {
        if (AssetDatabase.IsValidFolder(folder))
        {
            return;
        }

        string parent = Path.GetDirectoryName(folder)?.Replace('\\', '/') ?? "Assets";
        string leaf = Path.GetFileName(folder);
        
        if (!AssetDatabase.IsValidFolder(parent))
        {
            EnsureFolderExists(parent);
        }

        AssetDatabase.CreateFolder(parent, leaf);
    }

    private static string RelativeLabel(string assetPath)
    {
        // Vis sti relativt til BaseFolder: "UI/Icons" osv.
        if (assetPath.StartsWith(BaseFolder))
        {
            string rel = assetPath.Substring(BaseFolder.Length).TrimStart('/');
            return string.IsNullOrEmpty(rel) ? "(root)" : rel;
        }

        return assetPath;
    }

    private static string AbsoluteToAssetPath(string absolute)
    {
        absolute = absolute.Replace('\\', '/');
        string data = Application.dataPath.Replace('\\', '/');

        if (!absolute.StartsWith(data))
        {
            return string.Empty;
        }

        return "Assets" + absolute.Substring(data.Length);
    }

    private static bool IsUnderBase(string assetPath)
    {
        return !string.IsNullOrEmpty(assetPath) 
            && (assetPath == BaseFolder || assetPath.StartsWith(BaseFolder + "/"));
    }

    static bool IsLeafFolder(string assetFolder)
    {
        // En leaf er en mappe uden undermapper
        return AssetDatabase.GetSubFolders(assetFolder).Length == 0;
    }
}
