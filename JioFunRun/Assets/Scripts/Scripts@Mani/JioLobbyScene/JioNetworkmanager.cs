using ExitGames.Client.Photon;
using JMRSDK.Toolkit.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JioNetworkmanager : MonoBehaviourPunCallbacks, IOnEventCallback
{
<<<<<<< Updated upstream:JioFunRun/Assets/Scripts/Scripts@Mani/JioLobbyScene/JioNetworkmanager.cs
    public static JioNetworkmanager Instance;

    [Header("Login Panel")]
    public JMRUIPrimaryInputField PlayerNameIF;
    public TMP_Text ConnectionStatus;
    public GameObject LoginUIPanel;

    [Header("Create Room UI Panel")]
    public GameObject CreateRoomUIPanel;
    public JMRUIPrimaryInputField RoomName;

    [Header("Game Options Login Panel")]
    public GameObject GameOptionsUIPanel;

    [Header("Inside UI Panel")]
    public GameObject InsideRoomUIPanel;
    public TMP_Text RoomInfoText;
    public TMP_Text Player1Name, Player2Name;

    public GameObject StartGameButton;

    [Header("RoomList  UI Panel")]
    public GameObject RoomListUIPanel;

    [Header("Join Random Room UI Panel")]
    public GameObject JoinRandomRoomUIPanel;


    [Header("Join friend Room UI Panel")]
    public GameObject FriendRoomUIPanel;
    public JMRUIPrimaryInputField FriendRoomName;

    [Header("Before Game Start")]
    public string P1, P2;


    public int NextSceneNumber;
    private const byte SendPlayerPos=1,PlayerHasCollided=2;
   
    public event Action<Vector3> ReceiveOtherPlayerPosition;
=======
    public static JioNetworkmanager instance;
    RoomOptions roomOptions;
    private const byte SendPlayerPos=1,PlayerHasCollided=2,SendIdlePos = 3;
    public event Action<Vector3> ReceiveOtherPlayerPosition;
    public event Action ReceiveIdlePos;
    public event Action<string> RoomInfo;
    public event Action<string> NewPlayerJoined;

>>>>>>> Stashed changes:JioFunRun/Assets/Scripts/JioLobbyScene/JioNetworkmanager.cs


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        MenuManager.instance.CreateRoom += OnCreateRoom;
        MenuManager.instance.JoinRoom += OnJoinRoom;
        PhotonNetwork.AutomaticallySyncScene = true;
        roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        PhotonNetwork.ConnectUsingSettings();
        
        
    }

    private void OnJoinRoom(string PlayerName, string RoomName)
    {
        PhotonNetwork.NickName = PlayerName;
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions,TypedLobby.Default);
    }

    private void OnCreateRoom(string PlayerName, string RoomName)
    {
        PhotonNetwork.NickName = PlayerName;
        PhotonNetwork.CreateRoom(RoomName, roomOptions);

    }

    public bool isMaster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #region Photon CallBacks


    public override void OnConnected()
    {
       
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        RoomInfo("Success");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInfo("Fail");
    }
    public override void OnJoinedRoom()
    {
        RoomInfo("Success");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        NewPlayerJoined(newPlayer.NickName);
       
    }


    public override void OnLeftLobby()
    {
       
    }
    public override void OnLeftRoom()
    {
        
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
       

    }



    #endregion

    #region SyncPlayer
     public void SendPlayerData(Vector3 Position)
    {
        
        object[] data = new object[] { RoundVector(Position) };
        RaiseEventOptions raiseEventOptions;
        if (PhotonNetwork.IsMasterClient)
        {
            raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        }
        else
        {
            raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        }
        PhotonNetwork.RaiseEvent(SendPlayerPos, data, raiseEventOptions, SendOptions.SendReliable);
    }

    private Vector3 RoundVector(Vector3 input)
    {
        Vector3 output;
        float x = input.x;
        float y = input.y;
        float z = input.z;
        x = (float)Math.Round(x, 5);
        y = (float)Math.Round(y, 5);
        z = (float)Math.Round(z, 5);
        output = new Vector3(x, y, z);
        return (output);
    }
    public void OnEvent(EventData photonEvent)
    {
       
        byte eventCode = photonEvent.Code;
        if (eventCode == SendPlayerPos)
        {
            
            object[] data = (object[])photonEvent.CustomData;
            if (ReceiveOtherPlayerPosition != null)
            {
                ReceiveOtherPlayerPosition((Vector3)data[0]);
            }

        }
        if (eventCode == PlayerHasCollided)
        {
            object[] data = (object[])photonEvent.CustomData;
            bool isCollided = (bool)data[0];
            Debug.Log("Other Player has Collided With GameObject");
            // instantiate at previous CheckPoint
        }
       
    }
    #endregion

    #region PlayerHasCollided
    public void OnPlayerCollidedWithObject()
    {
        object[] data = new object[] { JioPlayer.instance.isCollided };
        RaiseEventOptions raiseEventOptions;
        if (PhotonNetwork.IsMasterClient)
        {
            raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        }
        else
        {
            raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        }
        PhotonNetwork.RaiseEvent(PlayerHasCollided, data, raiseEventOptions, SendOptions.SendReliable);
    }
    #endregion

}
