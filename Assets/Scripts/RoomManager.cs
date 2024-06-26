using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [SerializeField] private GameObject spawnPointObj;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject ScoreboardPanel;

    private bool pauseState = false;
    private bool scoreboardState = false;

    [SerializeField] private GameObject generalCamera;
    private GameObject playerCamera;

    private PhotonView PV;

    void Start()
    {
        instance = this;

        if (PhotonNetwork.CurrentRoom != null)
        {
            // *************
            Invoke("SpawnPlayer", 1f);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseState = !pauseState;
            SetPanelStateFnc(PausePanel, pauseState);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboardState = !scoreboardState;
            SetPanelStateFnc(ScoreboardPanel, scoreboardState);
        }
    }
    private void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerObj.name, GetSpawnPoint().position, Quaternion.identity, 0);

        PV = player.GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            playerCamera = player.GetComponentInChildren<Camera>().gameObject;
            SwitchToPlayerCamera();
        }
    }
    public Transform GetSpawnPoint()
    {
        int randomPoint = Random.Range(0, spawnPointObj.transform.childCount);
        Transform spawnPoint = spawnPointObj.transform.GetChild(randomPoint);
        return spawnPoint;
    }
    private void SetPanelStateFnc(GameObject panel, bool state)
    {
        panel.SetActive(state);
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void SwitchToGeneralCamera()
    {
        if (PV.IsMine)
        {
            generalCamera.SetActive(true);
            playerCamera.SetActive(false);
        }

    }
    public void SwitchToPlayerCamera()
    {
        if (PV.IsMine)
        {
            generalCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
    }
}
