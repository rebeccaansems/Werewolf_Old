using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.ServerBackchannels;
using EasyWiFi.Core;
using System;

namespace EasyWiFi.ServerControls
{
    public class CharacterSelectionServer : MonoBehaviour, IServerController
    {

        public string control = "CharacterCreateController";
        public EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.Player1;
        public string notifyMethod = "UpdateImage";
        [Tooltip("Determines when your Notify Method gets called")]
        public EasyWiFiConstants.CALL_TYPE callType = EasyWiFiConstants.CALL_TYPE.Only_When_Changed;

        IntBackchannelType[] intController = new IntBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        int currentNumberControllers = 0;
        int lastValue = 0;
        
        public Sprite[] sprites;
        public Image[] image;


        void UpdateImage(object[] obj)
        {
            image[(int)player].sprite = sprites[((IntBackchannelType)obj[0]).INT_VALUE];
        }

        void OnEnable()
        {
            EasyWiFiController.On_ConnectionsChanged += checkForNewConnections;

            //do one check at the beginning just in case we're being spawned after startup and after the callbacks
            //have already been called
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
            //iterate over the current number of connected controllers
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
            if (callType == EasyWiFiConstants.CALL_TYPE.Every_Frame)
                SendMessage(notifyMethod, intController[index], SendMessageOptions.DontRequireReceiver);
            else
            {
                if (lastValue != intController[index].INT_VALUE)
                {
                    SendMessage(notifyMethod, new object[] { intController[index], index });
                }
                lastValue = intController[index].INT_VALUE;
            }
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref intController, ref currentNumberControllers);
        }
    }
}
