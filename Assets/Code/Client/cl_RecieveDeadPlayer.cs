using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientBackchannels
{
    public class cl_RecieveDeadPlayer : MonoBehaviour, IClientBackchannel
    {
        public string controlName = "DeadPlayerController";
        public EasyWiFiConstants.CALL_TYPE callType = EasyWiFiConstants.CALL_TYPE.Only_When_Changed;
        public Button[] characters;

        //runtime variables
        StringBackchannelType stringBackchannel = new StringBackchannelType();
        string backchannelKey;
        string lastValue = "";

        void Awake()
        {
            backchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_STRING, controlName);
            stringBackchannel = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[backchannelKey];
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

        void Update()
        {
            if (stringBackchannel.serverKey != null)
            {
                mapDataStructureToMethod();
            }
        }

        public void mapDataStructureToMethod()
        {
            if (stringBackchannel.STRING_VALUE != null)
            {
                if (!stringBackchannel.STRING_VALUE.Equals(lastValue))
                {
                    DisableDeadCharacters(stringBackchannel.STRING_VALUE);
                }
                lastValue = stringBackchannel.STRING_VALUE;
            }
        }
    }
}
