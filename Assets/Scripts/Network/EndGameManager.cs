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
                    GameObject go = GameObject.Instantiate(NetGameManager.instance.humanModels[i].model);
                    Animator anim = go.AddComponent<Animator>();
                    anim.runtimeAnimatorController = NetGameManager.instance.humanModels[i].animatorController;
                    anim.avatar = NetGameManager.instance.humanModels[i].animationAvatar;
                    go.transform.position = playerSlots[i].position;
                    go.transform.rotation = playerSlots[i].rotation;
                }
                humanWinPanel.SetActive(true);
            }
            else
            {
                for (int i = 0; i < NetGameManager.instance.ghostModels.Count; i++)
                {
                    GameObject go = GameObject.Instantiate(NetGameManager.instance.ghostModels[i].model);
                    Animator anim = null;
                    try
                    {
                        anim = go.GetComponent<Animator>();
                    }
                    catch
                    {
                        anim = go.AddComponent<Animator>();
                    }
                    anim.runtimeAnimatorController = NetGameManager.instance.ghostModels[i].animatorController;
                    anim.avatar = NetGameManager.instance.ghostModels[i].animationAvatar;
                    go.transform.position = playerSlots[i].position;
                    go.transform.rotation = playerSlots[i].rotation;
                }
                ghostWinPanel.SetActive(true);
            }
            NetGameManager.instance.humanModels.Clear();
            NetGameManager.instance.ghostModels.Clear();
        }
    }
}