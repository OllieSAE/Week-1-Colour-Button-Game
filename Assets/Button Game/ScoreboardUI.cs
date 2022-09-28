using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ScoreboardUI : NetworkBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public ScoreboardModel scoreboardModel;
    public GameManager gameManager;

    private string myName;
    private int myScore;

    private List<Player> playerList;

    void OnEnable()
    {
        GameManager.StartGameEvent += StartSetup;
    }

    void StartSetup()
    {
        playerList = gameManager.playerList;
        scoreboardModel.ScoredPointEvent += UpdateScoreboardUI;
        scoreboardModel.LostPointEvent += UpdateScoreboardUI;
    }
    private void OnDisable()
    {
        GameManager.StartGameEvent -= StartSetup;
        
        scoreboardModel.ScoredPointEvent -= UpdateScoreboardUI;
        scoreboardModel.LostPointEvent -= UpdateScoreboardUI;
    }

    public void UpdateScoreboardUI()
    {
        if (IsServer)
        {
            UpdateClientScoreboardUIClientRpc();
        }
    }
    
    [ClientRpc]
    void UpdateClientScoreboardUIClientRpc()
    {
        StartCoroutine(UpdateScoreboardCoroutine());
    }

    //NOTE: Coroutine is used because client's scoreboard updated a few frames before client's player's scores
    //so client was ALWAYS one score behind server. Coroutine with a tiny delay allows for proper, albeit hacky, sync
    private IEnumerator UpdateScoreboardCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        print("updating score");
        
        textMeshProUGUI.text = "";
        for (int i = 0; i < playerList.Count; i++)
        {
            textMeshProUGUI.text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
        }
    }
}
