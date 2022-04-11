using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    [Header("Login Panel")]
    public InputField PlayerName;
    public GameObject LoginUIPanel;
    public Text ConnectionStatus;

    [Header("Game Options Login Panel")]
    public GameObject GameOptionsUIPanel;

    [Header("Create Room UI Panel")]
    public GameObject CreateRoomUIPanel;
    public InputField RoomName;
    public InputField maxPlayers;

    [Header("Inside UI Panel")]
    public GameObject InsideRoomUIPanel;
    public Text RoomInfoText;
    public GameObject PlayerListPrefab;
    public GameObject PlayerListContent;
    public GameObject StartGameButton;

    [Header("RoomList  UI Panel")]
    public GameObject RoomListUIPanel;
    public GameObject RoomListEntryPrefab;
    public GameObject RoomListParentGameObject;

    [Header("Join Random Room UI Panel")]
    public GameObject JoinRandomRoomUIPanel;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObjects;
    private Dictionary<int, GameObject> PlayerListGameObjects;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        ActivatePanels(LoginUIPanel.name);
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObjects = new Dictionary<string, GameObject>();
        PhotonNetwork.AutomaticallySyncScene = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        ConnectionStatus.text = " Connection Status: " + PhotonNetwork.NetworkClientState;
        
    }

    #region methods
    public void OnLoginButtonClicked()
    {
        string playerName = PlayerName.text;
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
    public void OnCreateRoombuttonClicked()
    {
        string roomName = RoomName.text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room Name was " + Random.Range(1000, 10000);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)int.Parse(maxPlayers.text) ;
        
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    public void OnCancelButtonClicked()
    {
        ActivatePanels(GameOptionsUIPanel.name);
    }
    public void OnShowRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivatePanels(RoomListUIPanel.name);
    }
    public void OnBackButtonClicked()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivatePanels(GameOptionsUIPanel.name);
    }
    public void OnJoinRandomRoomButtonClicked()
    {
        ActivatePanels(JoinRandomRoomUIPanel.name);
        PhotonNetwork.JoinRandomRoom();
    }
    public void ActivatePanels(string PanelName)
    {
        LoginUIPanel.SetActive(PanelName.Equals(LoginUIPanel.name));
        GameOptionsUIPanel.SetActive(PanelName.Equals(GameOptionsUIPanel.name));
        CreateRoomUIPanel.SetActive(PanelName.Equals(CreateRoomUIPanel.name));
        InsideRoomUIPanel.SetActive(PanelName.Equals(InsideRoomUIPanel.name));
        RoomListUIPanel.SetActive(PanelName.Equals(RoomListUIPanel.name));
        JoinRandomRoomUIPanel.SetActive(PanelName.Equals(JoinRandomRoomUIPanel.name));
    }
    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void OnStartGameButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("level");
        }
        
    }


    #endregion

    #region PhotonCallBacks

    public override void OnConnected()
    {
        Debug.Log("you have connected to net");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName+"  has connected to server ");
        ActivatePanels(GameOptionsUIPanel.name);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully by " + PhotonNetwork.LocalPlayer.NickName+ "Roomname was "+ PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Successfully by " + PhotonNetwork.LocalPlayer.NickName+ "Roomname was " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanels(InsideRoomUIPanel.name);
        RoomInfoText.text = " Room Name " + PhotonNetwork.CurrentRoom.Name + " Players/MaxPlayers: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        if (PlayerListGameObjects == null)
        {
            PlayerListGameObjects = new Dictionary<int, GameObject>();
        }
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            StartGameButton.SetActive(true);
        }
        else if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            StartGameButton.SetActive(false);
        }
        

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerListGameObject = Instantiate(PlayerListPrefab);
            playerListGameObject.transform.SetParent(PlayerListContent.transform);
            playerListGameObject.transform.localScale = Vector3.one;
            playerListGameObject.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;
            if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
            }
            PlayerListGameObjects.Add(player.ActorNumber, playerListGameObject);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearListView();
       foreach(RoomInfo room in roomList)
        {
            
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }

            }
            else
            {
                if (cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList[room.Name]= room;
                }
                else
                {
                    cachedRoomList.Add(room.Name, room);
                }
            }
       }
       foreach(RoomInfo room in cachedRoomList.Values)
       {
            
            GameObject roomListEntry = Instantiate(RoomListEntryPrefab);
            roomListEntry.transform.SetParent(RoomListParentGameObject.transform);
            roomListEntry.transform.localScale = Vector3.one;
            roomListEntry.transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            roomListEntry.transform.Find("RoomPlayersText").GetComponent<Text>().text = room.PlayerCount+" / "+ room.MaxPlayers;
            roomListEntry.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButonClicked(room.Name));
            roomListGameObjects.Add(room.Name, roomListEntry);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        RoomInfoText.text = " Room Name " + PhotonNetwork.CurrentRoom.Name + " Players/MaxPlayers: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;



        GameObject playerListGameObject = Instantiate(PlayerListPrefab);
        playerListGameObject.transform.SetParent(PlayerListContent.transform);
        playerListGameObject.transform.localScale = Vector3.one;
        playerListGameObject.transform.Find("PlayerNameText").GetComponent<Text>().text = newPlayer.NickName;
        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
        }
        else
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
        }
        PlayerListGameObjects.Add(newPlayer.ActorNumber, playerListGameObject);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomInfoText.text = " Room Name " + PhotonNetwork.CurrentRoom.Name + " Players/MaxPlayers: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        Destroy(PlayerListGameObjects[otherPlayer.ActorNumber].gameObject);
        PlayerListGameObjects.Remove(otherPlayer.ActorNumber);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            StartGameButton.SetActive(true);
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("thr joining random failed because " + message);
        string RoomName = " Room " + Random.Range(1000, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(RoomName, roomOptions);
    }
    public void ClearListView()
    {
        Debug.Log("It came in to ClearListView");
        foreach (var roomListGameObjects in roomListGameObjects.Values)
        {
            Destroy(roomListGameObjects);
        }
        roomListGameObjects.Clear();
    }
    public override void OnLeftLobby()
    {
        Debug.Log("Left Lobby");
        ClearListView();
        cachedRoomList.Clear();
    }
    public override void OnLeftRoom()
    {
        ActivatePanels(GameOptionsUIPanel.name);
        foreach(GameObject playerListGameobject in PlayerListGameObjects.Values)
        {
            Destroy(playerListGameobject);
        }
        PlayerListGameObjects.Clear();
        PlayerListGameObjects = null;
    }
    #endregion

    private void OnJoinRoomButonClicked(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
    }
}
