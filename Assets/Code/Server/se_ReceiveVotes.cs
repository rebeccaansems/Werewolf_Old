using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using System.Collections;

namespace EasyWiFi.ServerControls
{
    public class se_ReceiveVotes : MonoBehaviour, IServerController
    {
        private StringBackchannelType[] stringController = new StringBackchannelType[EasyWiFiConstants.MAX_CONTROLLERS];
        private EasyWiFiConstants.PLAYER_NUMBER player = EasyWiFiConstants.PLAYER_NUMBER.AnyPlayer;
        private string control = "SendVotes";
        private int currentNumberControllers = 0;
        private int lastIndex = -1;
        private int numberVotes = 0;

        void OnEnable()
        {
            numberVotes = 0;
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

            if (numberVotes == currentNumberControllers && currentNumberControllers > 0 && !GetComponent<ServerBackchannels.se_SendWhoDied>().enabled)
            {
                this.GetComponent<ServerBackchannels.se_SendWhoDied>().enabled = true;
                this.GetComponent<ServerBackchannels.se_SendWhoDied>().sendWhoDied(countVotes());

                resetcount++;
                lastIndex = -1;
                numberVotes = 0;

                updateAllCharacters();
                StartCoroutine("DisableSendWhoDied");
            }
        }
        int resetcount = 0;

        public void mapDataStructureToAction(int index)
        {
            if (stringController[index].STRING_VALUE != null)
            {
                int ignore;
                if (lastIndex != index && int.TryParse(stringController[index].STRING_VALUE, out ignore))
                {
                    numberVotes++;
                    updateCharacters(int.Parse(stringController[index].STRING_VALUE) - 1, index);
                    lastIndex = index;
                }
            }
        }

        int countVotes()
        {
            int deadPlayer = -1;
            int deadPlayerVotes = -1;
            for (int i = 0; i < gl_se_GameObjects.numberVotes.Count; i++)
            {
                if (deadPlayer < gl_se_GameObjects.numberVotes[i])
                {
                    deadPlayerVotes = gl_se_GameObjects.numberVotes[i];
                    deadPlayer = i;
                }
            }
            return deadPlayer;
        }

        void updateAllCharacters()
        {
            for (int i = 0; i < gl_se_GameObjects.numberVotes.Count; i++)
            {
                gl_se_GameObjects.numberVotes[i] = -1;
                updateCharacters(i, 0);
            }
        }

        void updateCharacters(int index, int playerVoting)
        {
            gl_se_GameObjects.numberVotes[playerVoting]++;
            gl_se_GameObjects.votesText[playerVoting].text = (gl_se_GameObjects.numberVotes[playerVoting] - 1).ToString() + " R: " + resetcount;
        }

        IEnumerator DisableSendWhoDied()
        {
            yield return new WaitForSeconds(2);
            this.GetComponent<ServerBackchannels.se_SendWhoDied>().enabled = false;
            StopCoroutine("DisableSendWhoDied");
        }

        public void checkForNewConnections(bool isConnect, int playerNumber)
        {
            EasyWiFiUtilities.checkForClient(control, (int)player, ref stringController, ref currentNumberControllers);
        }
    }
}
