using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : NetworkBehaviour
{
    public ButtonState buttonState;
    
    private bool pointsAwarded = true;
    private bool currentMatch = false;
    
    public delegate void SetUpGame();
    public event SetUpGame SetUpGameEvent;

    public delegate void StartGame();
    public static event StartGame StartGameEvent;
    
    private string myName;
    private int myScore;
    private KeyCode myKeyCode;

    public List<Player> playerList;

    void Awake()
    {
        playerList = new List<Player>();
        
        SetUpGameEvent?.Invoke();
        buttonState.ButtonMatchEvent += NewMatch;
        buttonState.ButtonFailEvent += NewFail;
    }

    void OnDisable()
    {
        buttonState.ButtonMatchEvent -= NewMatch;
        buttonState.ButtonFailEvent -= NewFail;
        foreach (var player in playerList)
        {
            player.IPressedMyKeyEvent -= PlayerPressedButton;
        }
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            if (IsServer)
            {
                playerList.AddRange(GameObject.FindObjectsOfType<Player>());
                buttonState.ChangeButtonState();
                StartGameClientRpc();
                foreach (var player in playerList)
                {
                    player.IPressedMyKeyEvent += PlayerPressedButton;
                }
                StartGameEvent?.Invoke();
            }
            
            //options instead of static event
            //NetworkManager.Singleton.ConnectedClients;
            //NetworkManager.Singleton.OnServerStarted;
        }
    }
    
    

    [ClientRpc]
    void StartGameClientRpc()
    {
        if (!IsServer)
        {
            playerList.AddRange(GameObject.FindObjectsOfType<Player>());
            StartGameEvent?.Invoke();
        }
    }

    void NewMatch()
    {
        currentMatch = true;
        pointsAwarded = false;
    }

    void NewFail()
    {
        currentMatch = false;
        pointsAwarded = false;
    }

    void PlayerPressedButton(Player player)
    {
        if (IsServer)
        {
            if (currentMatch && !pointsAwarded)
            {
                pointsAwarded = true;
                currentMatch = false;
                player.ChangeScore(1);
            }
            else if (pointsAwarded)
            {
                print("" + myName + ", you're too slow, points have already been awarded for this round!");
            }
            else if (!currentMatch && !pointsAwarded)
            {
                player.ChangeScore(-1);
                print("" + myName + ", you dun goofed, you lose a point!");
            }
        }
        
    }
}
