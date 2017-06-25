using UnityEditor;
using UnityEngine;
using System.Collections;


public class BuildSettings : EditorWindow
{

    public string[] options = new string[] { "Server", "Controller" };
    public int index = 0;

    private static string path = "C:/Users/User/Desktop/Werewolf Builds";

    [MenuItem("Examples/Windows Build")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(BuildSettings));
        window.Show();
    }

    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);
        if (GUILayout.Button("Build"))
            InstantiatePrimitive();
    }

    void InstantiatePrimitive()
    {
        switch (index)
        {
            case 0:
                BuildController();
                break;
            case 1:
                BuildServer();
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }

    public static void BuildController()
    {
        string[] levels = new string[] { "Assets/Scenes/Controller.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/Controller.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

    }

    public static void BuildServer()
    {
        string[] levels = new string[] { "Assets/Scenes/Server.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/Server.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

    }
}