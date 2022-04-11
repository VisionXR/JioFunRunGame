using JMRSDK.Toolkit.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JioNetworkmanager : MonoBehaviourPunCallbacks
{
    [Header("Login Panel")]
    public TMP_Text PlayerNameInput;
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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ActivatePanels(LoginUIPanel.name);
    }

    
    void Update()
    {
        ConnectionStatus.text = " Connection Status: " + PhotonNetwork.NetworkClientState;
    }
    #region Methods
    public void OnEnterButtonClicked()
    {
        string playerName = PlayerNameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("PlayerName was invalid");
        }
    }
    public void OnCreateRoomButtonClicked()
    {
        string roomName = RoomName.Text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room Name was " + Random.Range(1000, 10000);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void ActivatePanels(string PanelName)
    {
        LoginUIPanel.SetActive(PanelName.Equals(LoginUIPanel.name));
        GameOptionsUIPanel.SetActive(PanelName.Equals(GameOptionsUIPanel.name));
        CreateRoomUIPanel.SetActive(PanelName.Equals(CreateRoomUIPanel.name));
        InsideRoomUIPanel.SetActive(PanelName.Equals(InsideRoomUIPanel.name));
        RoomListUIPanel.SetActive(PanelName.Equals(RoomListUIPanel.name));
        JoinRandomRoomUIPanel.SetActive(PanelName.Equals(JoinRandomRoomUIPanel.name));
        FriendRoomUIPanel.SetActive(PanelName.Equals(FriendRoomUIPanel.name));
    }
    public void OnInsideRoomBackButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void OnStartGameButtonClicked()
    {

        PhotonNetwork.LoadLevel(NextSceneNumber);
    }
    public void OnJoinRandomRoomButtonClicked()
    {
        PhotonNetwork.JoinRoom(FriendRoomName.Text);
    }
    public void OnJoinRoomButonClicked()
    {
        string roomName = FriendRoomName.Text;
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
    }
    public void OnFriendRoomBackButtonClicked()
    {
        ActivatePanels(GameOptionsUIPanel.name);
    }

    #endregion

    #region Photon CallBacks


    public override void OnConnected()
    {
        Debug.Log("you have connected to internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "  has connected to server ");
        ActivatePanels(GameOptionsUIPanel.name);
    }
   
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully by " + PhotonNetwork.LocalPlayer.NickName + "Roomname was " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Successfully by " + PhotonNetwork.LocalPlayer.NickName + "Roomname was " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanels(InsideRoomUIPanel.name);
        RoomInfoText.text = " Room Name " + PhotonNetwork.CurrentRoom.Name + " Players/MaxPlayers: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        Player1Name.text = PlayerNameInput.name;
        StartGameButton.SetActive(false);
       

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Joined Room Successfully by " + newPlayer.NickName + "Roomname was " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanels(InsideRoomUIPanel.name);
        RoomInfoText.text = " Room Name " + PhotonNetwork.CurrentRoom.Name + " Players/MaxPlayers: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        Player2Name.text = newPlayer.NickName;
        Debug.Log(" new player enterd the room");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartGameButton.SetActive(true);
        }
    }
    
    
    public override void OnLeftLobby()
    {
        ActivatePanels(GameOptionsUIPanel.name);
    }
    public override void OnLeftRoom()
    {
        ActivatePanels(CreateRoomUIPanel.name);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Room Joined Failed because " + message+ "return code was "+ returnCode);

    }
    
    

    #endregion
}
