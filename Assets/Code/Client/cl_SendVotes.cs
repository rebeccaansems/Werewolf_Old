using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientControls
{
    public class cl_SendVotes : MonoBehaviour, IClientController
    {
        public string controlName = "SendPlayerController";
        public Button[] nameButtons;

        private IntBackchannelType intData;
        private int controllerValue = -1, currentPlayerValue;

        void Awake()
        {
            string key = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_INT, controlName);
            intData = (IntBackchannelType)EasyWiFiController.controllerDataDictionary[key];
        }

        void Update()
        {
            mapInputToDataStream();
        }

        public void PressedNameButton(int index)
        {
            for (int i = 0; i < nameButtons.Length; i++)
            {
                if (i != index)
                {
                    nameButtons[i].GetComponent<Image>().color = Color.white;
                }
            }
            nameButtons[index].GetComponent<Image>().color = Color.grey;

            currentPlayerValue = index;
        }

        public void PressedSend()
        {
            nameButtons[currentPlayerValue].GetComponent<Image>().color = Color.white;
            controllerValue = currentPlayerValue;
        }

        public void mapInputToDataStream()
        {
            intData.INT_VALUE = controllerValue + 1;
        }
    }
}
