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

    //uses Start instead of OnEnable so gameManager has time to generate player list
    void Start()
    {
        
    }
    
    
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

    //player parameter not actually used at the moment - the If statement was irrelevant
    public void UpdateScoreboardUI()
    {
        playerList = gameManager.playerList;
        textMeshProUGUI.text = "";

        for (int i = 0; i < playerList.Count; i++)
        {
            textMeshProUGUI.text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
        }

        
        
        if (IsClient && !IsServer)
        {
            RequestUpdateScoreboardUIServerRpc();
            print("client request scoreboard update");
        }

        if (IsServer)
        {
            UpdateClientScoreboardUIClientRpc();
        }
    }

    [ClientRpc]
    void UpdateClientScoreboardUIClientRpc()
    {
        //this isn't working as of Tues week 2
        //check how the button state rpc's to client
        //should be able to do something similar?

        if (!IsServer)
        {
            textMeshProUGUI.text = "";
            for (int i = 0; i < playerList.Count; i++)
            {
                textMeshProUGUI.text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestUpdateScoreboardUIServerRpc()
    {
        UpdateScoreboardUI();
    }
}
