using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using EasyWiFi.Core;

namespace EasyWiFi.ServerBackchannels
{
    public class se_SendDeadPlayer : MonoBehaviour, IServerBackchannel
    {
        public string control = "DeadPlayerController";
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;

        StringBackchannelType[] stringBackchannel = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0;

        string sendValue;

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;
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
        }

        public void setValue(string newValue)
        {
            sendValue = newValue;
        }

        public void mapPropertyToDataStream(int index)
        {
            stringBackchannel[index].STRING_VALUE = sendValue;
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref stringBackchannel, ref currentNumberControllers);
        }

        public void PressedSend()
        {
            string deadCharacters = "";
            for (int i = 0; i < gl_variables.deadCharacters.Length; i++)
            {
                if (gl_variables.deadCharacters[i])
                {
                    deadCharacters += "1";
                }
                else
                {
                    deadCharacters += "0";
                }
            }

            setValue(deadCharacters);
        }
    }
}
