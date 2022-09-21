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
        playerList = gameManager.playerList;
    }
    void OnEnable()
    {
        scoreboardModel.ScoredPointEvent += UpdateScoreboardUI;
        scoreboardModel.LostPointEvent += UpdateScoreboardUI;
    }
    private void OnDisable()
    {
        scoreboardModel.ScoredPointEvent -= UpdateScoreboardUI;
        scoreboardModel.LostPointEvent -= UpdateScoreboardUI;
    }

    //player parameter not actually used at the moment - the If statement was irrelevant
    public void UpdateScoreboardUI(Player player)
    {
        if (IsClient)
        {
            
        }
        textMeshProUGUI.text = "";

        for (int i = 0; i < playerList.Count; i++)
        {
            textMeshProUGUI.text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
        }
    }
}
