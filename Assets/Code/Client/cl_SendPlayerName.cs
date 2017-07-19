using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientControls
{
    public class cl_SendPlayerName : MonoBehaviour, IClientController
    {
        public InputField nameInput;
        public Canvas voteCanvas, characterCreateCanvas;

        private string controlName = "SendPlayerName";
        private StringBackchannelType stringData;
        private string stringKey;

        //variable other script will modify via setValue to be sent across the network
        string value;

        // Use this for initialization
        void Awake()
        {
            stringKey = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_STRING, controlName);
            stringData = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[stringKey];
        }

        //here we grab the input and map it to the data list
        void Update()
        {
            mapInputToDataStream();
        }

        public void setValue(string newValue)
        {
            value = newValue;
        }

        public void mapInputToDataStream()
        {
            //for properties DO NOT reset to default values becasue there isn't a default
            stringData.STRING_VALUE = value;
        }

        public void submitName()
        {
            setValue(nameInput.text);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Controller02Vote", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
