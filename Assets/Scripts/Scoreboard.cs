using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject scoreboardEntryPrefab;
    [SerializeField] private Transform scoreboardEntryParent;

    private Dictionary<string, GameObject> scoreboardEntries = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }
    private void AddScoreboardItem(Player player)
    {
        GameObject entry = Instantiate(scoreboardEntryPrefab, scoreboardEntryParent);
        
        entry.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>().text = player.NickName;
        if (player.CustomProperties.ContainsKey("kills"))
            entry.transform.Find("KillText").GetComponent<TextMeshProUGUI>().text = player.CustomProperties["kills"].ToString();
        if (player.CustomProperties.ContainsKey("deaths"))
            entry.transform.Find("DeathText").GetComponent<TextMeshProUGUI>().text = player.CustomProperties["kills"].ToString();

        scoreboardEntries.Add(player.NickName, entry);
    }

    private void RemoveScoreboardItem(Player player)
    {
        Destroy(scoreboardEntries[player.NickName].gameObject);
        scoreboardEntries.Remove(player.NickName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("kills") || changedProps.ContainsKey("deaths"))
        {
            if (scoreboardEntries.ContainsKey(targetPlayer.NickName))
            {
                GameObject playerObject = scoreboardEntries[targetPlayer.NickName];
                playerObject.transform.Find("KillText").GetComponent<TextMeshProUGUI>().text = ((int)changedProps["kills"]).ToString();
                playerObject.transform.Find("DeathText").GetComponent<TextMeshProUGUI>().text = ((int)changedProps["deaths"]).ToString();
            }
        }
    }

    //public void RefreshScoreboard()
    //{
    //    // Oyuncularý skora göre sýrala
    //    Player[] players = PhotonNetwork.PlayerList;
    //    Array.Sort(players, (a, b) =>
    //    {
    //        int aKills = (int)a.CustomProperties["kills"];
    //        int bKills = (int)b.CustomProperties["kills"];
    //        return bKills.CompareTo(aKills);
    //    });
    //}
}
