using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;

namespace EasyWiFi.ServerControls
{
    public class se_ReceiveVotes : MonoBehaviour, IServerController
    {
        private StringBackchannelType[] stringController = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        private EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;
        private string control = "SendVotes";
        private int currentNumberControllers = 0;
        private int lastIndex = -1;
        private int[] currentController = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

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
            if (stringController[index].STRING_VALUE != null)
            {
                int ignore;
                if (lastIndex != index && int.TryParse(stringController[index].STRING_VALUE, out ignore))
                {
                    UpdateCharacters(int.Parse(stringController[index].STRING_VALUE) - 1, index);
                    lastIndex = index;
                }
            }
        }

        void UpdateCharacters(int index, int playerVoting)
        {
            currentController[playerVoting] = index;
            gl_se_GameObjects.votesText[playerVoting].text = (int.Parse(gl_se_GameObjects.votesText[playerVoting].text) + 1).ToString();
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref stringController, ref currentNumberControllers);
        }
    }
}
