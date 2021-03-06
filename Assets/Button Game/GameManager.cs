using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public ButtonState buttonState;
    
    private bool pointsAwarded = true;
    private bool currentMatch = false;
    
    public delegate void SetUpGame();
    public event SetUpGame SetUpGameEvent;
    
    private string myName;
    private int myScore;
    private KeyCode myKeyCode;

    public List<Player> playerList;

    void Awake()
    {
        playerList = new List<Player>();
        playerList.AddRange(GameObject.FindObjectsOfType<Player>());
        SetUpGameEvent?.Invoke();
        buttonState.ButtonMatchEvent += NewMatch;
        buttonState.ButtonFailEvent += NewFail;
    }
    void Start()
    {
        SetPlayers();
    }
    void OnDisable()
    {
        buttonState.ButtonMatchEvent -= NewMatch;
        buttonState.ButtonFailEvent -= NewFail;
    }
    

    void Update()
    {
        PlayerInputs();
    }

    void SetPlayers()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            print("" + playerList[i].GetName() + "'s keycode is " + playerList[i].GetKeyCodeName().ToString());
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
    void PlayerInputs()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (Input.GetKeyDown(playerList[i].GetKeyCode()) && currentMatch && (!pointsAwarded))
            {
                pointsAwarded = true;
                currentMatch = false;
                playerList[i].GainScore();
            }
            else if (Input.GetKeyDown(playerList[i].GetKeyCode()) && (pointsAwarded))
            {
                print(""+playerList[i].myName + ", you're too slow, points have already been awarded for this round!");
            }
            else if (Input.GetKeyDown(playerList[i].GetKeyCode()) && (!currentMatch) && (!pointsAwarded))
            {
                playerList[i].LoseScore();
                print(""+playerList[i].myName + ", you dun goofed, you lose a point!");
            }
        }
    }
}
