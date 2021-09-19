using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scener : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("transsyncscene", LoadSceneMode.Single);
    }
}