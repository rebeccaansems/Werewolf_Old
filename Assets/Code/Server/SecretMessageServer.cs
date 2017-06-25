using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.ServerBackchannels;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ServerControls
{
    public class SecretMessageServer : MonoBehaviour
    {

        public StringServerBackchannel stringBackchannel;

        void Update()
        {
            stringBackchannel.setValue("hello");
        }
    }
}
