using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scener : MonoBehaviour
{
    public string loadScene;

    public void PlayGame()
    {
        SceneManager.LoadScene(loadScene, LoadSceneMode.Single);
    }

    public static void LoadScene(int index)
    {
         SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}