using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] private GameObject spawnPointObj;
    [SerializeField] private GameObject playerObj;
    void Start()
    {
        instance = this;

        SpawnPlayer();
    }

    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        int randomPoint = Random.Range(0, spawnPointObj.transform.childCount);
        Transform spawnPoint = spawnPointObj.transform.GetChild(randomPoint);

        GameObject player = PhotonNetwork.Instantiate(playerObj.name, spawnPoint.position, Quaternion.identity);
    }
}
