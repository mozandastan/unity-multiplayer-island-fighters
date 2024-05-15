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

    void Start()
    {
        instance = this;

        if(PhotonNetwork.CurrentRoom != null) 
        {
            // *************
            Invoke("SpawnPlayer", 0.5f);
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
        int randomPoint = Random.Range(0, spawnPointObj.transform.childCount);
        Transform spawnPoint = spawnPointObj.transform.GetChild(randomPoint);

        PhotonNetwork.Instantiate(playerObj.name, spawnPoint.position, Quaternion.identity,0);

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
}
