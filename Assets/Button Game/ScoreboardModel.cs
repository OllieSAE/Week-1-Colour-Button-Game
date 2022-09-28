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
        GameManager.StartGameEvent += StartSetup;
    }

    void StartSetup()
    {
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

        GameManager.StartGameEvent -= StartSetup;
    }

    public void AddScore()
    {
        ScoredPointEvent?.Invoke();
    }

    public void MinusScore()
    {
        LostPointEvent?.Invoke();
    }
}
