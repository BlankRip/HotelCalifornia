using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knotgames.Network
{
    public class LeftPanel : MonoBehaviour
    {
        public GameObject thePanel;

        private void Start()
        {
            NetConnector.instance.OnMsgRecieve.AddListener(Hear);
        }

        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<ReadyData>(dataString).eventName;
            switch (eventName)
            {
                case "playerLeftRoom":
                    thePanel.SetActive(true);
                    Cursor.visible = true;
                    break;
            }
        }
        public void LeaveRoom()
        {
            NetConnector.instance.OnMsgRecieve.RemoveListener(Hear);
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("leaveRoom", DistributionOption.serveMe)));
            Destroy(NetConnector.instance.gameObject);
            SceneManager.LoadScene(0);
        }

        [System.Serializable]
        public class ReadyData
        {
            public string eventName;
            public string distributionOption;
            public ReadyData(string name, string distributionOption)
            {
                eventName = name;
                this.distributionOption = distributionOption;
            }
        }
    }
}