using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyWiFi.Core;

namespace EasyWiFi.ClientBackchannels
{
    public class cl_RecieveWhoDied : MonoBehaviour, IClientBackchannel
    {
        public string pnControlName = "DeadPlayerController";

        //runtime variables
        IntBackchannelType deadPlayerBackchannel = new IntBackchannelType();
        string pnBackchannelKey;

        void Awake()
        {
            pnBackchannelKey = EasyWiFiController.registerControl(EasyWiFiConstants.BACKCHANNELTYPE_INT, pnControlName);
            deadPlayerBackchannel = (IntBackchannelType)EasyWiFiController.controllerDataDictionary[pnBackchannelKey];
        }

        void Update()
        {
            if (deadPlayerBackchannel.serverKey != null)
            {
                mapDataStructureToMethod();
            }
        }

        public void mapDataStructureToMethod()
        {
            if (deadPlayerBackchannel.INT_VALUE != 0)
            {
                Debug.Log("Dead Player: " + (deadPlayerBackchannel.INT_VALUE-1));
                gl_cl_GameObjects.deadPlayers.Add(deadPlayerBackchannel.INT_VALUE-1);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Controller02Vote", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }
    }
}
