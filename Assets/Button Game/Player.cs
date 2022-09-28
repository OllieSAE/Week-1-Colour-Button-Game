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
    private KeyCode myKeyCode = KeyCode.Space;
    
    public delegate void IScored();
    public event IScored IScoredEvent;
    public event IScored ILostScoreEvent;

    public delegate void IPressedMykey(Player player);

    public event IPressedMykey IPressedMyKeyEvent;

    private void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (IsClient)
        {
            if (IsLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CheckPlayerInputsServerRpc();
                }
            }
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    void CheckPlayerInputsServerRpc()
    {
        IPressedMyKeyEvent?.Invoke(this);
    }

    public void ChangeScore(int amount)
    {
        //this is not synchronised - if a player joined late, they'd be X points behind!
        //score needs to be STORED and updated on server side only
        //somehow "broadcast" it to all clients each update
        
        //client should just invoke an rpc saying "i pressed a trigger"
        //server does everything else
        if (IsServer)
        {
            myScore += amount;
            IScoredEvent?.Invoke();
            ChangeScoreClientRpc(myScore);
        }
        
        if (IsClient && !IsServer)
        {
            //RequestGainScoreServerRpc();
        }
    }

    [ClientRpc]
    void ChangeScoreClientRpc(int newScore)
    {
        if (IsClient)
        {
            myScore = newScore;
            //IScoredEvent?.Invoke();
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
