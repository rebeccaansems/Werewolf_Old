using UnityEngine;
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
        public string dcControlName = "DeadPlayerController", pnControlName = "PlayerNameController";
        public Canvas votesCanvas;
        public Button[] characters;
        public Text[] playerNames;

        //runtime variables
        StringBackchannelType deadCharacterStringBackchannel = new StringBackchannelType();
        StringBackchannelType playerNameStringBackchannel = new StringBackchannelType();
        string dcBackchannelKey, pnBackchannelKey;
        string dcLastValue = "", pnLastValue = "";

        void Awake()
        {
            dcBackchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_STRING, dcControlName);
            deadCharacterStringBackchannel = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[dcBackchannelKey];

            pnBackchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_STRING, pnControlName);
            playerNameStringBackchannel = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[pnBackchannelKey];
        }

        void DisableDeadCharacters(string deadCharacters)
        {
            for(int i=0; i<characters.Length; i++)
            {
                if(deadCharacters[i] == '1')
                {
                    characters[i].interactable = false;
                }
            }
        }

        void NameCharacters(string names)
        {
            List<string> namesList = names.Split(',').ToList<string>();

            for(int i=0; i< playerNames.Length; i++)
            {
                playerNames[i].text = namesList[i];
            }
        }

        void Update()
        {
            if (deadCharacterStringBackchannel.serverKey != null)
            {
                mapDataStructureToMethod();
            }
        }

        public void mapDataStructureToMethod()
        {
            if (deadCharacterStringBackchannel.STRING_VALUE != null)
            {
                if (!deadCharacterStringBackchannel.STRING_VALUE.Equals(dcLastValue))
                {
                    DisableDeadCharacters(deadCharacterStringBackchannel.STRING_VALUE);
                }
                dcLastValue = deadCharacterStringBackchannel.STRING_VALUE;
            }

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
