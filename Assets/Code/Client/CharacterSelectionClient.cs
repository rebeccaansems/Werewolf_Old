using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientControls
{
    public class CharacterSelectionClient : MonoBehaviour, IClientController
    {
        public string controlName = "CharacterCreateController";

        public Image image;
        public Sprite[] images;

        private int currentImage;
        private IntBackchannelType intData;

        private int controllerValue;

        void Awake()
        {
            string key = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_INT, controlName);
            intData = (IntBackchannelType)EasyWiFiController.controllerDataDictionary[key];
        }

        public void mapInputToDataStream()
        {
            intData.INT_VALUE = controllerValue;
        }

        void Update()
        {
            mapInputToDataStream();
        }

        public void PressedLeft()
        {
            if (currentImage > 0)
            {
                currentImage--;
            }
            else
            {
                currentImage = images.Length-1;
            }
            image.sprite = images[currentImage];
        }

        public void PressedRight()
        {
            if (currentImage < images.Length - 1)
            {
                currentImage++;
            }
            else
            {
                currentImage = 0;
            }
            image.sprite = images[currentImage];
        }

        public void PressedSend()
        {
            controllerValue = currentImage;
        }
    }
}
