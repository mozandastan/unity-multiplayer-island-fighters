using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [SerializeField] private GameObject spawnPointObj;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject PauseCanvas;

    private bool pauseState = false;

    [SerializeField] private GameObject generalCamera;
    private GameObject playerCamera;

    void Start()
    {
        instance = this;

        if(PhotonNetwork.CurrentRoom != null) 
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
            SetPauseFnc();
        }
    }
    private void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerObj.name, GetSpawnPoint().position, Quaternion.identity,0);
        playerCamera = player.GetComponentInChildren<Camera>().gameObject;
    }
    public Transform GetSpawnPoint()
    {
        int randomPoint = Random.Range(0, spawnPointObj.transform.childCount);
        Transform spawnPoint = spawnPointObj.transform.GetChild(randomPoint);
        return spawnPoint;
    }
    private void SetPauseFnc()
    {
        PauseCanvas.SetActive(pauseState);
        Cursor.lockState = pauseState ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseState;
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
        generalCamera.SetActive(true);
        playerCamera.SetActive(false);
    }
    public void SwitchToPlayerCamera()
    {
        generalCamera.SetActive(false);
        playerCamera.SetActive(true);
    }
}
