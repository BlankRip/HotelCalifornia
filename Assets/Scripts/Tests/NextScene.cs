using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Network;

public class NextScene : MonoBehaviour
{
    public NetGameManager netGameManager;
    private void Start()
    {
        if (netGameManager != null)
            netGameManager = FindObjectOfType<NetGameManager>();
    }
    public void NextScener()
    {
        netGameManager.inGame = true;
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Backer()
    {
        Destroy(NetConnector.instance.gameObject);
        SceneManager.LoadScene(0);
    }
}