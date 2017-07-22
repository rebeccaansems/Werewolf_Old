using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_CreateGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public Text roomCodeText, numberPlayersText;

    public string roomCode { get; private set; }

    private bool playerJoinedRoom = true;

    void Start()
    {
        roomCode = getRandomWord();
        roomCodeText.text = roomCode;

        PhotonNetwork.autoJoinLobby = false;

        if (!(PhotonNetwork.connected))
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    private string getRandomWord()
    {
        string possibleLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        string word = "";
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];

        return word;
    }

    private void Update()
    {
        if (PhotonNetwork.connectionStateDetailed.ToString().Equals("ConnectedToMaster") && playerJoinedRoom)
        {
            Debug.Log("[PHOTON] Room Created: " + roomCode);
            PhotonNetwork.JoinOrCreateRoom(roomCode, new RoomOptions() { MaxPlayers = 12, PlayerTtl = 600000 }, TypedLobby.Default);

            playerJoinedRoom = false;
        }

        if (PhotonNetwork.inRoom)
        {
            numberPlayersText.text = PhotonNetwork.room.PlayerCount.ToString();
        }

    }
}
