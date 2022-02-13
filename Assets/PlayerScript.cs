using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    public string myName;
    private int myScore = 0;
    public KeyCode myKeyCode;
    
    void Awake()
    {
        this.GetComponent<ButtonScript>().SetPlayer(myName, myScore, myKeyCode);
    }
    
    public string GetName()
    {
        return myName;
    }

    public string GetKeyCodeName()
    {
        return myKeyCode.ToString();
    }

    public KeyCode GetKeyCode()
    {
        return myKeyCode;
    }
    
    void OnEnable()
    {
        ButtonScript.ScoreboardEvent += AddScore;
    }
    void OnDisable()
    {
        ButtonScript.ScoreboardEvent -= AddScore;
    }

    public void AddScore()
    {
        GetComponent<ScoreboardScript>().UpdateScoreboardUI(myName);
    }

    public void SetScore()
    {
        myScore += 1;
    }

    public int GetScore()
    {
        return myScore;
    }
}
