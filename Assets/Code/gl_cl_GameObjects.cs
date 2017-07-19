using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gl_cl_GameObjects : MonoBehaviour
{
    public static List<GameObject> playerPods;
    public static List<Text> playerNames;
    public static List<int> deadPlayers;

    void Start()
    {
        playerPods = new List<GameObject>();
        playerNames = new List<Text>();
        deadPlayers = new List<int>();
    }
}
