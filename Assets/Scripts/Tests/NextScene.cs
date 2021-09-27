using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Network;

public class NextScene : MonoBehaviour
{
    public NetGameManager netGameManager;
    public void NextScener()
    {
        netGameManager.inGame = true;
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}