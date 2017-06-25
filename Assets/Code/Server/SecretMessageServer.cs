using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using EasyWiFi.Core;

namespace EasyWiFi.ServerBackchannels
{
    public class SecretMessageServer : MonoBehaviour, IServerBackchannel
    {
        public string control = "SecretMessageController";
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;

        //runtime variables
        StringBackchannelType[] stringBackchannel = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0;

        //variable other script will modify via setValue to be sent across the backchannel
        string value;

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            //do one check at the beginning just in case we're being spawned after startup and after the callbacks
            //have already been called
            if (stringBackchannel[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(control, (int)player, ref stringBackchannel, ref currentNumberControllers);
            }
        }

        void OnDestroy()
        {
            EasyWiFiController.On_ConnectionsChanged -= checkForNewConnections;
        }

        void Update()
        {
            for (int i = 0; i < currentNumberControllers; i++)
            {
                if (stringBackchannel[i] != null && stringBackchannel[i].serverKey != null && stringBackchannel[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapPropertyToDataStream(i);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                setValue("helllo");
                Debug.Log("helllo");
            }
        }

        public void setValue(string newValue)
        {
            value = newValue;
        }

        public void mapPropertyToDataStream(int index)
        {
            stringBackchannel[index].STRING_VALUE = value;
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref stringBackchannel, ref currentNumberControllers);
        }
    }
}
