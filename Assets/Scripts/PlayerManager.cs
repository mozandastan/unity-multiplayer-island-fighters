using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviourPun
{
    [Header("NameText")]
    [SerializeField] private GameObject nameObj;
    private Camera cam;

    [Header("Components")]
    [SerializeField] private GameObject myCam;
    [SerializeField] private ThirdPersonController myMovement;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerHealth playerHealth;

    private int kills;
    private int deaths;

    void Start()
    {
        TextMeshPro nameText = nameObj.transform.GetChild(0).GetComponent<TextMeshPro>();
        nameText.text = photonView.Owner.NickName;

        if (photonView.IsMine)
        {
            nameText.color = Color.green;

            kills = 0;
            deaths = 0;
            UpdatePlayerProperties();
        }
        else
        {
            nameText.color = Color.white;
            myMovement.enabled = false;
            playerCombat.enabled = false;
            //playerHealth.enabled = false;
            myCam.SetActive(false);
        }
    }

    void Update()
    {
        if (cam == null || !cam.gameObject.activeSelf)
            cam = FindObjectOfType<Camera>();

        if (cam == null)
            return;
        nameObj.transform.LookAt(cam.transform);
    }
    [PunRPC]
    public void AddKill()
    {
        if (photonView.IsMine)
        {
            kills++;
            UpdatePlayerProperties();
        }
    }

    public void AddDeath()
    {
        if (photonView.IsMine)
        {
            deaths++;
            UpdatePlayerProperties();
        }
    }

    private void UpdatePlayerProperties()
    {
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["kills"] = kills;
        hash["deaths"] = deaths;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

}
