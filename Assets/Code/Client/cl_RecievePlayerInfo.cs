﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyWiFi.ClientBackchannels
{
    public class cl_RecievePlayerInfo : MonoBehaviour, IClientBackchannel
    {
        public string pnControlName = "PlayerNameController";
        public GameObject playerVotePod, playerVotePanel;
        public Canvas votesCanvas;

        //runtime variables
        StringBackchannelType playerNameStringBackchannel = new StringBackchannelType();
        string pnBackchannelKey;
        string pnLastValue = "";

        void Awake()
        {
            pnBackchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_STRING, pnControlName);
            playerNameStringBackchannel = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[pnBackchannelKey];
        }

        void DisableDeadCharacters(string deadCharacters)
        {
            for (int i = 0; i < deadCharacters.Length; i++)
            {
                if (deadCharacters[i] == '1')
                {
                    gl_cl_GameObjects.playerPods[i].GetComponent<Button>().interactable = false;
                }
            }
        }

        void NameCharacters(string names)
        {
            List<string> namesList = names.Split(',').ToList<string>();
            namesList.RemoveAt(namesList.Count - 1);
            
            for (int i = 0; i < namesList.Count; i++)
            {
                if (i > gl_cl_GameObjects.playerPods.Count - 1)
                {
                    GameObject newPod = Instantiate(playerVotePod, playerVotePanel.transform);
                    newPod.transform.SetAsFirstSibling();
                    newPod.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<ClientControls.cl_SendVotes>().PressedNameButton(i); });
                    gl_cl_GameObjects.playerPods.Add(newPod);
                    gl_cl_GameObjects.playerNames.Add(newPod.GetComponentsInChildren<Text>()[0]);
                }

                gl_cl_GameObjects.playerNames[i].text = namesList[i];
            }
        }

        void Update()
        {
            if (playerNameStringBackchannel.serverKey != null)
            {
                mapDataStructureToMethod();
            }
        }

        public void mapDataStructureToMethod()
        {
            if (votesCanvas.gameObject.activeSelf)
            {
                if (playerNameStringBackchannel.STRING_VALUE != null)
                {
                    if (!playerNameStringBackchannel.STRING_VALUE.Equals(pnLastValue))
                    {
                        NameCharacters(playerNameStringBackchannel.STRING_VALUE);
                    }
                    pnLastValue = playerNameStringBackchannel.STRING_VALUE;
                }
            }
        }
    }
}
