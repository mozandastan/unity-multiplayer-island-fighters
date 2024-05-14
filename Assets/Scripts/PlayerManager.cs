using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    [Header("NameText")]
    [SerializeField] private GameObject nameObj;
    private Camera cam;

    [Header("Components")]
    [SerializeField] private GameObject myCam;
    [SerializeField] private ThirdPersonController myMovement;

    void Start()
    {
        TextMeshPro nameText = nameObj.transform.GetChild(0).GetComponent<TextMeshPro>();
        nameText.text = photonView.Owner.NickName;

        if (photonView.IsMine)
        {
            nameText.color = Color.green;
        }
        else
        {
            nameText.color = Color.white;
            myMovement.enabled = false;
            myCam.SetActive(false);
        }
    }

    void Update()
    {
        if (cam == null)
            cam = FindObjectOfType<Camera>();

        if (cam == null)
            return;
        nameObj.transform.LookAt(cam.transform);
    }
}
