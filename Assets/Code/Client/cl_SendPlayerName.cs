using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientControls
{
    public class cl_SendPlayerName : MonoBehaviour
    {
        public InputField nameInput;
        public Canvas voteCanvas, characterCreateCanvas;

        private string controlName = "SendPlayerName";
        private StringBackchannelType stringData;

        void Awake()
        {
            string key = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_STRING, controlName);
            stringData = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[key];
        }

        public void submitName()
        {
            stringData.STRING_VALUE = nameInput.text;

            voteCanvas.gameObject.SetActive(true);

            characterCreateCanvas.gameObject.SetActive(false);
        }
    }
}
