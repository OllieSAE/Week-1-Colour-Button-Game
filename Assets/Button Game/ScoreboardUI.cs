using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public ScoreboardModel scoreboardModel;
    public GameManager gameManager;

    private string myName;
    private int myScore;

    private List<Player> playerList;

    void OnEnable()
    {
        scoreboardModel.ScoredPointEvent += UpdateScoreboardUI;
        playerList = gameManager.playerList;
    }

    private void OnDisable()
    {
        scoreboardModel.ScoredPointEvent -= UpdateScoreboardUI;
    }

    public void UpdateScoreboardUI(Player player)
    {
        textMeshProUGUI.text = "";
        
        myName = player.myName;
        
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].GetName() == myName)
            {
                playerList[i].SetScore();
            }
        }
        
        for (int i = 0; i < playerList.Count; i++)
        {
            textMeshProUGUI.text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
        }
    }
}
