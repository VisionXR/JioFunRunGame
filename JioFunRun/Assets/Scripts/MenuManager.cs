using UnityEngine;
using UnityEngine.SceneManagement;
using JMRSDK.Toolkit.UI;
using System;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject LevelPanel, GameModePanel, RoomPanel, WaitingPanel;
    public JMRUIPrimaryInputField RoomNameIF, PlayerNameIF;
    public event Action<string,string> CreateRoom, JoinRoom;
    public Text WaitingText,DebugText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ResetPanels();
        EnablePanel(GameModePanel);
        JioNetworkmanager.instance.RoomInfo += OnRoomInfo;
        JioNetworkmanager.instance.NewPlayerJoined += OnNewPlayerJoined;
    }

    private void OnNewPlayerJoined(string NewPlayerName)
    {
        ResetPanels();
        EnablePanel(LevelPanel);
    }

    private void OnRoomInfo(string RoomCreation)
    {
        if (RoomCreation == "Success")
        {
            EnablePanel(WaitingPanel);
            WaitingText.text = "Please Wait For Other Player";

        }
        else if(RoomCreation == "Fail")
        {
            ResetPanels();
            EnablePanel(RoomPanel);
            StartCoroutine(ShowText("Fail to created/Joined Room", DebugText));
        }
    }
    private IEnumerator ShowText(string Msg,Text Debug)
    {
        Debug.text = Msg;
        yield return new WaitForSeconds(5);
        Debug.text = "";
    }

    public void OnLevelSelected(int i)
    {
        if (PlayerPrefs.GetString("MP") == "false")
        {
            SceneManager.LoadSceneAsync(i);
        }
        else
        {
            PhotonNetwork.LoadLevel(i);
        }

    }

    public void OnSPSelected()
    {
        PlayerPrefs.SetString("MP","false");
        ResetPanels();
        EnablePanel(LevelPanel);

    }
    public void OnMPSelected()
    {
        PlayerPrefs.SetString("MP", "true");
        ResetPanels();
        EnablePanel(RoomPanel);

    }
    private void EnablePanel(GameObject Panel)
    {
        Panel.GetComponent<CanvasGroup>().alpha = 1;
        Panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    private void DisablePanel(GameObject Panel)
    {
        Panel.GetComponent<CanvasGroup>().alpha = 0;
        Panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    private void ResetPanels()
    {
        DisablePanel(LevelPanel);
        DisablePanel(GameModePanel);
        DisablePanel(RoomPanel);
        DisablePanel(WaitingPanel);
    }
    public void OnCreateRoom()
    {
        CreateRoom(PlayerNameIF.Text, RoomNameIF.Text);// to network manager
        ResetPanels();
        EnablePanel(WaitingPanel);

    }
    public void OnJoinRoom()
    {
        JoinRoom(PlayerNameIF.Text, RoomNameIF.Text);// to network manager
        ResetPanels();
        EnablePanel(WaitingPanel);
    }
}
