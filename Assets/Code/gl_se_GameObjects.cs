using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gl_se_GameObjects : MonoBehaviour
{
    public static List<GameObject> playerPods;
    public static List<Text> votesText;
    public static List<Text> playerNamesText;

    private void Awake()
    {
        playerPods = new List<GameObject>();
        votesText = new List<Text>();
        playerNamesText = new List<Text>();
    }
}
