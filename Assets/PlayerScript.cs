using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //public GameObject player;
    //public List<GameObject> playerList;

    public int playerScore;
    public GameObject scoreboardUI;


    //public ButtonScript buttonScript;
    // Start is called before the first frame update
    void Start()
    {
        //if (buttonScript == null)
        //{
            //buttonScript = GameObject.FindObjectOfType<ButtonScript>();
        //}

        
        //scoreboardUI = this.gameObject;
    }


    void OnEnable()
    {
        ButtonScript.ScoreboardEvent += AddScore;
    }
    void OnDisable()
    {
        ButtonScript.ScoreboardEvent -= AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddScore()
    {
        playerScore += 1;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        scoreboardUI.GetComponentInChildren<TextMeshProUGUI>().text = playerScore.ToString();
    }
}
