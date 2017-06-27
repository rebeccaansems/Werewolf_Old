using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.ServerBackchannels;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ServerControls
{
    public class se_ReceivePlayerName : MonoBehaviour
    {
        public GameObject playerPod, podPanel;

        private StringBackchannelType[] stringController = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        private EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;

        private string control = "SendPlayerName";
        private int currentNumberControllers = 0;

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            if (stringController[0] == null && EasyWiFiController.lastConnectedPlayerNumber >= 0)
            {
                EasyWiFiUtilities.checkForClient(control, (int)player, ref stringController, ref currentNumberControllers);
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
                if (stringController[i] != null && stringController[i].serverKey != null && stringController[i].logicalPlayerNumber != EasyWiFiConstants.PLAYERNUMBER_DISCONNECTED)
                {
                    mapDataStructureToAction(i);
                }
            }
        }

        public void mapDataStructureToAction(int index)
        {
            if (stringController != null)
            {
                if (currentNumberControllers > gl_se_GameObjects.playerPods.Count)
                {
                    GameObject newPod = Instantiate(playerPod, podPanel.transform);
                    gl_se_GameObjects.playerPods.Add(newPod);
                    gl_se_GameObjects.playerNamesText.Add(newPod.GetComponentsInChildren<Text>()[0]);
                    gl_se_GameObjects.votesText.Add(newPod.GetComponentsInChildren<Text>()[1]);
                }

                gl_se_GameObjects.playerNamesText[index].text = stringController[index].STRING_VALUE;
                GetComponent<se_SendPlayerInfo>().SendNames();
            }
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref stringController, ref currentNumberControllers);
        }
    }
}