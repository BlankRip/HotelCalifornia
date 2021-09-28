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
        SceneManager.LoadScene(0);
    }

    public void Backer()
    {
        SceneManager.LoadScene(0);
    }
}