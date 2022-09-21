using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public string myName;
    private int myScore = 0;
    public KeyCode myKeyCode;
    
    public delegate void IScored(Player player);
    public event IScored IScoredEvent;
    public event IScored ILostScoreEvent;

    public void GainScore()
    {
        myScore += 1;
        IScoredEvent?.Invoke(this);
    }

    public void LoseScore()
    {
        myScore -= 1;
        ILostScoreEvent?.Invoke(this);
    }

    #region Getters

    public int GetScore()
    {
        if (myScore >= 0)
        {
            return myScore;
        }
        else
        {
            myScore = 0;
            return myScore;
        }
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

    #endregion
}
