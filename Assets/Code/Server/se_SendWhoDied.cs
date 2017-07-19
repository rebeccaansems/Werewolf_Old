using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyWiFi.Core;

namespace EasyWiFi.ServerBackchannels
{
    public class se_SendWhoDied : MonoBehaviour, IServerBackchannel
    {
        public string pnControlName = "DeadPlayerController";

        private EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;
        private IntBackchannelType[] deadPlayerBackchannel = new IntBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        private int currentNumberControllers = 0;

        private int pnSendValue;

        public void Awake()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            if (deadPlayerBackchannel[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(pnControlName, (int)player, ref deadPlayerBackchannel, ref currentNumberControllers);
            }

            pnSendValue = -1;
        }

        void OnDestroy()
        {
            EasyWiFiController.On_ConnectionsChanged -= checkForNewConnections;
        }

        void Update()
        {
            for (int i = 0; i < currentNumberControllers; i++)
            {
                if (deadPlayerBackchannel[i] != null && deadPlayerBackchannel[i].serverKey != null && deadPlayerBackchannel[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapPropertyToDataStream(i);
                }
            }
        }

        public void mapPropertyToDataStream(int index)
        {
            deadPlayerBackchannel[index].INT_VALUE = pnSendValue + 1;
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(pnControlName, (int)player, ref deadPlayerBackchannel, ref currentNumberControllers);
        }

        public void sendWhoDied(int deadPlayer)
        {
            pnSendValue = deadPlayer;
        }
    }
}
