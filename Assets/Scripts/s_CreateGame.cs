using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_CreateGame : MonoBehaviour
{
    public const string VERSION = "0.1";

    public Text roomCodeText;
    public string roomCode { get; private set; }

    void Start()
    {
        roomCode = getRandomWord();
        roomCodeText.text = roomCode;

        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

        if (!(PhotonNetwork.connected))
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }

        //PhotonNetwork.autoJoinLobby = false;
        //PhotonNetwork.CreateRoom(roomCode, new RoomOptions() { MaxPlayers = 12, PlayerTtl = 600000 }, null);
    }

    private string getRandomWord()
    {
        string possibleLetters = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
        string word = "";
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];
        word += possibleLetters[Random.Range(0, possibleLetters.Length)];

        return word;
    }

    bool test = true;
    private void Update()
    {
        if (PhotonNetwork.connectionStateDetailed.ToString().Equals("ConnectedToMaster") && test)
        {
            Debug.Log("[PHOTON] Room Created: " + roomCode);
            test = false;
            PhotonNetwork.JoinOrCreateRoom(roomCode, new RoomOptions() { MaxPlayers = 12, PlayerTtl = 600000 }, TypedLobby.Default);
        }
    }
}
