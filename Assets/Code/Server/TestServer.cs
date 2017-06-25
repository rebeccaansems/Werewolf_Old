using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestServer : MonoBehaviour
{

    public IntServerBackchannel stringBackchannel;

    private void Update()
    {
        stringBackchannel.setValue(12);
    }

}
