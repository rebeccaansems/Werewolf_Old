using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.ServerBackchannels;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ServerControls
{
    public class se_ReceivePlayer : MonoBehaviour
    {
        public string control = "SendPlayerController";
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.Player1;
        public Image[] playerPanels;

        IntBackchannelType[] intController = new IntBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0, lastValue = -1;

        void UpdateCharacters(int index)
        {
            gl_variables.deadCharacters[index] = true;
            UpdatePanelColors();
        }

        void UpdatePanelColors()
        {
            for (int i = 0; i < playerPanels.Length; i++)
            {
                if (gl_variables.deadCharacters[i])
                {
                    playerPanels[i].color = Color.red;
                }
            }
        }

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            if (intController[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(control, (int)player, ref intController, ref currentNumberControllers);
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
                if (intController[i] != null && intController[i].serverKey != null && intController[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapDataStructureToAction(i);
                }
            }
        }

        public void mapDataStructureToAction(int index) 
        {
            if (lastValue != intController[index].INT_VALUE && intController[index].INT_VALUE!=0)
            {
                UpdateCharacters(intController[index].INT_VALUE - 1);
                lastValue = intController[index].INT_VALUE - 1;
            }
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref intController, ref currentNumberControllers);
        }
    }
}
