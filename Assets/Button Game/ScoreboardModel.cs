using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardModel : MonoBehaviour
{
    public GameManager gameManager;

    public delegate void Scoreboard(Player player);

    public event Scoreboard ScoredPointEvent;
    private List<Player> playerList;

    private void OnEnable()
    {
        playerList = gameManager.playerList;
        foreach (Player player in playerList)
        {
            player.IScoredEvent += AddScore;
        }
    }
    
    private void OnDisable()
    {
        foreach (Player player in playerList)
        {
            player.IScoredEvent -= AddScore;
        }
    }

    public void AddScore(Player player)
    {
        ScoredPointEvent?.Invoke(player);
    }
}
