using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using EasyWiFi.Core;

namespace EasyWiFi.ServerBackchannels
{
    public class se_SendPlayerInfo : MonoBehaviour, IServerBackchannel
    {
        public string pnControlName = "PlayerNameController";
        public Text[] playerNames;
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;

        StringBackchannelType[] playerNameStringBackchannel = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0;

        string dcSendValue, pnSendValue;

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            if (playerNameStringBackchannel[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(pnControlName, (int)player, ref playerNameStringBackchannel, ref currentNumberControllers);
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
                if (playerNameStringBackchannel[i] != null && playerNameStringBackchannel[i].serverKey != null && playerNameStringBackchannel[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapPropertyToDataStream(i);
                }
            }
        }

        public void mapPropertyToDataStream(int index)
        {
            playerNameStringBackchannel[index].STRING_VALUE = pnSendValue;
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(pnControlName, (int)player, ref playerNameStringBackchannel, ref currentNumberControllers);
        }

        public void PressedSend()
        {
        }

        public void SendNames()
        {
            string playerNameString = "";

            for (int i = 0; i < gl_se_GameObjects.playerNamesText.Count - 1; i++)
            {
                playerNameString += gl_se_GameObjects.playerNamesText[i].text + ",";
            }
            playerNameString += gl_se_GameObjects.playerNamesText[gl_se_GameObjects.playerNamesText.Count - 1].text + ",";

            pnSendValue = playerNameString;
        }
    }
}
