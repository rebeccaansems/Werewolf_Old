using EasyWiFi.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClient : MonoBehaviour {

    public Text message;
	public void updateText(IntBackchannelType text)
    {
        Debug.Log(text.INT_VALUE);
        message.text = text.INT_VALUE.ToString();
    }
}
