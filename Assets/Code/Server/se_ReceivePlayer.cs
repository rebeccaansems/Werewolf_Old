﻿using System.Collections;
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
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;
        public Image[] playerPanels;
        public Text[] votesText;

        IntBackchannelType[] intController = new IntBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0, lastValue = -1, lastIndex = -1;
        int[] currentController = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        void UpdateCharacters(int index, int playerVoting)
        {
            if(currentController[playerVoting] != index)
            {
                currentController[playerVoting] = index;
                gl_variables.deadCharacters[index] = true;
                votesText[index].text = (int.Parse(votesText[index].text) + 1).ToString();
                UpdatePanelColors();
            }
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
            if (lastValue != intController[index].INT_VALUE && intController[index].INT_VALUE != 0)
            {
                UpdateCharacters(intController[index].INT_VALUE - 1, index);
                lastValue = -1;
                intController[index].INT_VALUE = 0;
            }
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref intController, ref currentNumberControllers);
        }
    }
}
