using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System;
using TMPro;

public class MenuController : MonoBehaviourPunCallbacks
{

    string playerName = "Player";
    string gameVersion = "0.1";

    [Header("PlayerName")]
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private TMP_InputField inputPlayername;

    [Header("CreateRoom")]
    [SerializeField] private GameObject CreateRoomMenu;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_InputField inputMaxPlayer;

    [Header("RoomList")]
    [SerializeField] private GameObject RoomListMenu;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    List<RoomInfo> createdRooms = new List<RoomInfo>();

    [Header("RoomPlayers")]
    [SerializeField] private GameObject RoomPlayersMenu;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerItemPrefab;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private TMP_Text roomNameText;

    private void Start()
    {

        if (!PhotonNetwork.IsConnected)
        {
            //Set the App version before connecting
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon'a baðlandý!"); // Photon sunucusuna baþarýyla baðlandý

        //This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye baðlandý");
        //PlayerNamePanel.SetActive(true);
    }

    public void CreatePlayername()
    {
        if (string.IsNullOrEmpty(inputPlayername.text))
        {
            return;
        }
        playerName = inputPlayername.text;
        PhotonNetwork.NickName = inputPlayername.text;

    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(inputRoomName.text) || string.IsNullOrEmpty(inputMaxPlayer.text))
        {
            return;
        }

        RoomOptions options = new RoomOptions { MaxPlayers = Convert.ToInt32(inputMaxPlayer.text) }; // Oda seçeneklerini ayarla
        PhotonNetwork.CreateRoom(inputRoomName.text, options); // Yeni bir oda oluþtur
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("odaya girildi");

        CreateRoomMenu.SetActive(false);
        RoomListMenu.SetActive(false);
        RoomPlayersMenu.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Transform item in playerListContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Player _player in PhotonNetwork.PlayerList)
        {
            GameObject player = Instantiate(playerItemPrefab, playerListContent);
            player.transform.GetChild(0).GetComponent<TMP_Text>().text = _player.NickName;
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");


        foreach (Transform room in roomListContent)
        {
            Destroy(room.gameObject);
        }
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                continue;
            }
            GameObject roomTemp = Instantiate(roomListItemPrefab, roomListContent);
            roomTemp.transform.GetChild(0).GetComponent<TMP_Text>().text = room.Name;
            roomTemp.GetComponent<Button>().onClick.AddListener(() => JoinRoom(room.Name));
        }

    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject player = Instantiate(playerItemPrefab, playerListContent);
        player.transform.GetChild(0).GetComponent<TMP_Text>().text = newPlayer.NickName;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (Transform child in playerListContent)
        {
            if (child.transform.GetChild(0).GetComponent<TMP_Text>().text == otherPlayer.NickName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
    public void StartGameBtnFnc()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Oda sahibi deðiþti");

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnLeftLobby()
    {
        Debug.Log("On Left Lobby");

        foreach (Transform room in roomListContent)
        {
            Destroy(room.gameObject);
        }
    }
}
