using UnityEditor;
using UnityEngine;
using System.IO;

public static class ProjectScaffolder
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateFolders()
    {
        string[] relPaths = new string[]
        {
            "Assets/Content",
            "Assets/Content/Audio",
            "Assets/Content/Scriptable Objects",
            "Assets/UI",
            "Assets/UI/Layouts",
            "Assets/UI/Styles",
            "Assets/Core",
            "Assets/Core/Managers",
            "Assets/Core/Scripts",
            "Assets/Core/Systems",
            "Assets/Prefabs",
            "Assets/Scenes",
            "Assets/Scenes/Levels - Minigames",
            "Assets/Settings"
        };

        foreach (var rel in relPaths)
        {
            string full = Path.Combine(Directory.GetCurrentDirectory(), rel);
            if (!Directory.Exists(full))
            {
                Directory.CreateDirectory(full);
                Debug.Log("Created: " + rel);
            }
        }
        AssetDatabase.Refresh();
    }
}
