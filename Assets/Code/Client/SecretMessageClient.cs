using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ClientControls
{
    public class SecretMessageClient : MonoBehaviour
    {
        public Text message;

        public void UpdateText(string text)
        {
            message.text = text;
        }
    }
}
