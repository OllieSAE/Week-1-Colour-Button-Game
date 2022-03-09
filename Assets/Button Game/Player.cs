using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string myName;
    private int myScore = 0;
    public KeyCode myKeyCode;
    //public event Action SetPlayerEvent;

    public delegate void SetPlayer(string myName, int myScore, KeyCode myKeyCode);
    public event SetPlayer SetPlayerEvent;

    public delegate void IScored(Player player);

    public event IScored IScoredEvent;
    
    void Start()
    {
        SetPlayerEvent?.Invoke(myName,myScore,myKeyCode);
        
        //this.GetComponentInParent<GameManager>().SetPlayer(myName, myScore, myKeyCode);
    }

    public void GetDetails()
    {
        myName = GetName();
        myKeyCode = GetKeyCode();
        myScore = GetScore();
    }
    
    public string GetName()
    {
        return myName;
    }
    //right click helper icon on left
    //encapsulate field
    //turn it to property!

    public string GetKeyCodeName()
    {
        return myKeyCode.ToString();
    }

    public KeyCode GetKeyCode()
    {
        return myKeyCode;
    }
    
    public void AddScore()
    {
        IScoredEvent?.Invoke(this);
    }

    public void SetScore()
    {
        myScore += 1;
    }

    public int GetScore()
    {
        return myScore;
    }
    
    //Event is currently not implemented
    //Intention was to use this to add score, but did not know how to add parameters to the event to send in player details
    // void OnEnable()
    // {
    //     GameManager.ScoreboardEvent += AddScore;
    // }
    // void OnDisable()
    // {
    //     GameManager.ScoreboardEvent -= AddScore;
    // }
}
