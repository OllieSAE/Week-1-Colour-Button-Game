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
    
    public delegate void IScored();
    public event IScored IScoredEvent;
    public event IScored ILostScoreEvent;

    public void GainScore()
    {
        myScore += 1;
        IScoredEvent?.Invoke();

        if (IsClient && !IsServer)
        {
            RequestGainScoreServerRpc();
        }

        if (IsServer)
        {
            GainScoreClientRpc();
        }
    }

    [ClientRpc]
    void GainScoreClientRpc()
    {
        if (!IsServer)
        {
            myScore += 1;
            IScoredEvent?.Invoke();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestGainScoreServerRpc()
    {
        myScore += 1;
        IScoredEvent?.Invoke();
    }

    public void LoseScore()
    {
        myScore -= 1;
        ILostScoreEvent?.Invoke();
        
        if (IsClient && !IsServer)
        {
            RequestLoseScoreServerRpc();
        }

        if (IsServer)
        {
            LoseScoreClientRpc();
        }
    }

    [ClientRpc]
    void LoseScoreClientRpc()
    {
        if (!IsServer)
        {
            myScore -= 1;
            ILostScoreEvent?.Invoke();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestLoseScoreServerRpc()
    {
        myScore -= 1;
        ILostScoreEvent?.Invoke();
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
