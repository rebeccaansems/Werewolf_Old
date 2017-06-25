using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientBackchannels
{
    public class SecretMessageClient : MonoBehaviour, IClientBackchannel
    {
        public Text message;

        public string controlName = "SecretMessageController";
        public string notifyMethod = "UpdateText";
        [Tooltip("Determines when your Notify Method gets called")]
        public EasyWiFiConstants.CALL_TYPE callType = EasyWiFiConstants.CALL_TYPE.Only_When_Changed;

        //runtime variables
        StringBackchannelType stringBackchannel = new StringBackchannelType();
        string backchannelKey;
        string lastValue = "";

        void Awake()
        {
            backchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_STRING, controlName);
            stringBackchannel = (StringBackchannelType)EasyWiFiController.controllerDataDictionary[backchannelKey];
        }

        public void UpdateText(object[] para)   
        {
            message.text = para[0].ToString();
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
            if (callType == EasyWiFiConstants.CALL_TYPE.Every_Frame)
            {
                SendMessage(notifyMethod, new object[] { stringBackchannel.STRING_VALUE }, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                if (stringBackchannel.STRING_VALUE != null && !lastValue.Equals(stringBackchannel.STRING_VALUE))
                {
                    SendMessage(notifyMethod, new object[] { stringBackchannel.STRING_VALUE }, SendMessageOptions.DontRequireReceiver);
                }
                lastValue = stringBackchannel.STRING_VALUE;
            }
        }
    }
}
