using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public static class EasyWiFiLandscapeController
{

    static EasyWiFiLandscapeController()
    {
        EditorApplication.hierarchyWindowChanged += OnHierarchyChange;
    }
    static void OnHierarchyChange()
    {

#pragma warning disable CS0618 // Type or member is obsolete
        if (EditorApplication.currentScene.Contains("MultiplayerDynamicClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("ControlsKitchenSinkClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("DrawingClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("UnityUINavigationClientScene") ||
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("PanTiltZoomClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("DualStickZoomClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("PrecomputedSteeringClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("MultiplayerControllerSelectClientScene") ||
#pragma warning disable CS0618 // Type or member is obsolete
            EditorApplication.currentScene.Contains("SteeringWheelClientScene"))
        {
            //we only need to execute once on our scenes
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            EditorApplication.hierarchyWindowChanged -= OnHierarchyChange;
        }

    }
}