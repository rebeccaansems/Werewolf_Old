using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_JoinGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public InputField roomCodeInput;

    private bool playerJoinedRoom = true;

    void Start()
    {
        PhotonNetwork.autoJoinLobby = false;

        if (!(PhotonNetwork.connected))
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    public void JoinGame()
    {
        PhotonNetwork.JoinRoom(roomCodeInput.text);
        Debug.Log("[PHOTON] Trying to join room: " + roomCodeInput.text);
    }
    private void Update()
    {
        if (PhotonNetwork.connected && PhotonNetwork.room != null && playerJoinedRoom)
        {
            Debug.Log("[PHOTON] Room Joined: " + roomCodeInput.text);
            playerJoinedRoom = false;
        }
    }
}
