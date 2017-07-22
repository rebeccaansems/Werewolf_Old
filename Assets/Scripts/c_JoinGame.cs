using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_JoinGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public Text errorText;
    public InputField roomCodeInput;

    private string joinRoomCode;
    private bool roomExists = false;

    void Start()
    {
        PhotonNetwork.autoJoinLobby = true;

        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    public void JoinGame()
    {
        joinRoomCode = roomCodeInput.text.ToUpper();

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].Name.Equals(joinRoomCode))
            {
                Debug.Log("[PHOTON] Joined room: " + joinRoomCode);
                PhotonNetwork.JoinRoom(joinRoomCode);
                roomExists = true;
                break;
            }
        }

        if (!roomExists)
        {
            Debug.Log("[PHOTON] Failed to join: " + joinRoomCode);
            errorText.text = "Room code " + joinRoomCode + " does not exist.";
        }
    }
}
