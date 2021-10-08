using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] GameObject humanWinPanel;
        [SerializeField] GameObject ghostWinPanel;

        private void Start()
        {
            if (NetGameManager.instance.humanWin)
                humanWinPanel.SetActive(true);
            else
                ghostWinPanel.SetActive(true);
        }
    }
}