using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Network;

public class NextScene : MonoBehaviour
{
    public void NextScener()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        NetGameManager.instance.LeaveRoom();
        SceneManager.LoadScene(0);
    }
}