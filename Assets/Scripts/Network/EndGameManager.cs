using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] GameObject humanWinPanel;
        [SerializeField] GameObject ghostWinPanel;
        [SerializeField] Transform[] playerSlots;

        private void Start()
        {
            InitializeWinScreen(NetGameManager.instance.humanWin);
        }

        void InitializeWinScreen(bool humanWin)
        {
            if (humanWin)
            {
                for (int i = 0; i < NetGameManager.instance.humanModels.Count; i++)
                {
                    GameObject go = GameObject.Instantiate(NetGameManager.instance.humanModels[i]);
                    go.transform.position = playerSlots[i].position;
                }
                humanWinPanel.SetActive(true);
            }
            else
            {
                for (int i = 0; i < NetGameManager.instance.ghostModels.Count; i++)
                {
                    GameObject go = GameObject.Instantiate(NetGameManager.instance.ghostModels[i]);
                    go.transform.position = playerSlots[i].position;
                }
                ghostWinPanel.SetActive(true);
            }
        }
    }
}