using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;

//maybe chuck a dictionary here - or actually in game manager more likely!!
//[Serializable]
//public class Player
//{
//  public string name;
//  public int score;
//  public etc etc.
//}

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

    private void Start()
    {
        GameManager.StartGameEvent += StartSetup;
    }

    private void StartSetup()
    {
        //NOTE: Hack just to show host/client name during testing
        //will remove when player setup UI exists
        if (IsServer)
        {
            if (IsLocalPlayer)
            {
                myName = "Host";
            }
            else myName = "Client";
        }

        if (IsClient && !IsServer)
        {
            if (IsLocalPlayer)
            {
                myName = "Client";
            }
            else myName = "Host";
        }
    }
    
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
        if (IsServer)
        {
            myScore += amount;
            IScoredEvent?.Invoke();
            ChangeScoreClientRpc(myScore);
        }
    }

    [ClientRpc]
    void ChangeScoreClientRpc(int newScore)
    {
        if (IsClient)
        {
            myScore = newScore;
        }
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
