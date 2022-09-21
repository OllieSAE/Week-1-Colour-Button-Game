using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScoreboardModel : NetworkBehaviour
{
    public GameManager gameManager;

    public delegate void Scoreboard();

    public event Scoreboard ScoredPointEvent;
    public event Scoreboard LostPointEvent;
    private List<Player> playerList;

    //uses Start instead of OnEnable so gameManager has time to generate player list
    void Start()
    {
        if (IsServer)
        {
            
        }
        playerList = gameManager.playerList;
        foreach (Player player in playerList)
        {
            player.IScoredEvent += AddScore;
            player.ILostScoreEvent += MinusScore;
        }
    }
    
    private void OnDisable()
    {
        foreach (Player player in playerList)
        {
            player.IScoredEvent -= AddScore;
            player.ILostScoreEvent -= MinusScore;
        }
    }

    public void AddScore()
    {
        if (IsServer)
        {
            
        }
        ScoredPointEvent?.Invoke();
    }

    public void MinusScore()
    {
        if (IsServer)
        {
            
        }
        LostPointEvent?.Invoke();
    }
}
