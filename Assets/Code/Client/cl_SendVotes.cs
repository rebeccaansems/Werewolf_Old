﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;

namespace EasyWiFi.ClientControls
{
    public class cl_SendVotes : MonoBehaviour
    {
        private StringBackchannelType stringData;
        private int currentPlayerValue = -1;
        private string controlName = "SendVotes";

        void Awake()
        {
            string key = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_STRING, controlName);
            stringData = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[key];

            stringData.STRING_VALUE = null;
        }

        public void PressedNameButton(int index)
        {
            for (int i = 0; i < gl_cl_GameObjects.playerPods.Count; i++)
            {
                if (i != index)
                {
                    gl_cl_GameObjects.playerPods[i].GetComponent<Image>().color = Color.white;
                }
            }
            gl_cl_GameObjects.playerPods[gl_cl_GameObjects.playerPods.Count - index].GetComponent<Image>().color = Color.grey;

            currentPlayerValue = gl_cl_GameObjects.playerPods.Count - index;
        }

        public void PressedSend()
        {
            gl_cl_GameObjects.playerPods[currentPlayerValue].GetComponent<Image>().color = Color.white;

            stringData.STRING_VALUE = currentPlayerValue.ToString();

            UnityEngine.SceneManagement.SceneManager.LoadScene("Controller03WaitingVotes", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
