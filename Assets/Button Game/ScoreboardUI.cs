using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour
{
    public GameObject scoreboardUI;

    private string myName;
    private int myScore;

    private List<Player> playerList;

    void Start()
    {
        playerList = new List<Player>();
        playerList.AddRange(GameObject.FindObjectsOfType<Player>());
    }
    
    public void UpdateScoreboardUI(string newMyName)
    {
        scoreboardUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        
        myName = newMyName;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].GetName() == myName)
            {
                playerList[i].SetScore();
            }
        }
        
        for (int i = 0; i < playerList.Count; i++)
        {
            scoreboardUI.GetComponentInChildren<TextMeshProUGUI>().text += "\n" + playerList[i].GetName() + "'s score is " + playerList[i].GetScore();
        }
    }
}
