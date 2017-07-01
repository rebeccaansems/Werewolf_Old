using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

public class BuildSettings : EditorWindow
{

    public string[] options = new string[] { "Controller", "Server" };
    public int index = 0;

    private static string buildPath = "C:/Users/User/Desktop/Werewolf Builds";
    private static string scenePath = "C:/Users/User/Documents/GitHub/Werewolf/Assets/Scenes";

    [MenuItem("Custom/Windows Build")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(BuildSettings));
        window.Show();
    }

    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);
        if (GUILayout.Button("Build"))
            ContollerServerSwitch();
    }

    void ContollerServerSwitch()
    {
        switch (index)
        {
            case 0:
                Build("Controller");
                break;
            case 1:
                Build("Server");
                break;
        }
    }

    private static void Build(string type)
    {
        List<string> levels = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(scenePath);
        FileInfo[] info = dir.GetFiles("*.unity");
        foreach (FileInfo f in info)
        {
            if (f.FullName.Contains(type))
            {
                levels.Add(f.FullName.Replace("C:/Users/User/Documents/GitHub/Werewolf/", ""));
            }
        }
        //int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        //for (int i = 0; i < sceneCount; i++)
        //{
        //    string scene = (UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        //    if (scene.Contains(type))
        //    {
        //        levels.Add(scene);
        //    }
        //}

        // Build player.
        BuildPipeline.BuildPlayer(levels.ToArray(), buildPath + "/" + type + ".exe", BuildTarget.StandaloneWindows, BuildOptions.Development);
        PostBuild();
    }

    public static void PostBuild()
    {
        Process.Start(buildPath);
    }

    [MenuItem("Custom/Reset Playerprefs")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
}