using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Network;

namespace Knotgames.Extensions
{
    public class NextScene : MonoBehaviour
    {
        public void NextScener()
        {
            SceneManager.LoadScene(1);
        }

        public void Restart()
        {
            NetGameManager.instance.connectedPlayers.Remove(NetConnector.instance.playerID.value);
            NetGameManager.instance.LeaveRoom();
            SceneManager.LoadScene(0);
        }

        public void RestartNoServer()
        {
            SceneManager.LoadScene(0);
        }
    }
}