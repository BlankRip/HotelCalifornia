// using UnityEngine;
// using VivoxUnity;

// public class VoIPManager : MonoBehaviour
// {
//     static public VoIPManager instance;
//     static public string channelName = "default";

//     [SerializeField]
//     private string server = "https://unity.vivox.com/appconfig/18966-hotel-63052-test";
//     [SerializeField]
//     private string domain = "mtu1xp.vivox.com";
//     [SerializeField]
//     private string tokenIssuer = "18966-hotel-63052-test";
//     [SerializeField]
//     private string tokenKey = "PcHv9TaUUmwRdVyzZMGriK1GKscMnsMF";

//     [SerializeField]ChannelType channelType;


//     public bool audioDeviceMuted;

//     void Start()
//     {
//         if (instance == null) instance = this;
//         else
//         {
//             Destroy(gameObject);
//             return;
//         }

//         Init();
//         DontDestroyOnLoad(this.gameObject);
//     }

//     void OnDestroy()
//     {
//         VivoxVoiceManager.Instance.OnUserLoggedInEvent -= JoinCancel;
//     }

//     void Init()
//     {
//         Debug.Log("Initializing Vivox");
//         VivoxVoiceManager.Instance.SetCredentials(server, domain, tokenIssuer, tokenKey);
//         VivoxVoiceManager.Instance.OnUserLoggedInEvent += JoinCancel;


//         Invoke("CallLogin", 2);
//     }

//     void CallLogin()
//     {
//         Login("displayName");
//     }

//     public void Mute()
//     {
//         audioDeviceMuted = !audioDeviceMuted;
//         VivoxVoiceManager.Instance.AudioInputDevices.Muted = audioDeviceMuted;
//     }

//     public void JoinCancel()
//     {
//         VivoxVoiceManager.Instance.JoinChannel(channelName, channelType, VivoxVoiceManager.ChatCapability.AudioOnly, true, null);
//     }

//     void OnParticipantAdded(string userName, ChannelId channel, IParticipant participant)
//     {
//         Debug.Log("OnPartAdded: " + userName);
//         //UpdateParticipantRoster(participant, channel, true);
//     }

//     void OnParticipantRemoved(string userName, ChannelId channel, IParticipant participant)
//     {
//         Debug.Log("OnPartRemoved: " + participant.Account.Name);
//         //UpdateParticipantRoster(participant, channel, false);
//     }

//     void Login(string displayName)
//     {
//         VivoxVoiceManager.Instance.Login(displayName);
//     }

//     public void Logout()
//     {
//         VivoxVoiceManager.Instance.Logout();
//     }

// }