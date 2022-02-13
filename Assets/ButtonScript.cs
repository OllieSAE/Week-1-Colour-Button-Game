using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour
{
    public Color[] colours;
    public string[] names;

    public GameObject buttonLeft;

    private bool colourOff = true;
    private bool nameOff;

    private int newColourIndex;
    private int newTextIndex;

    private int currentColour;
    private int currentText;

    private bool pointsAwarded = false;
    
    public delegate void ScoreDelegate();
    public static event ScoreDelegate ScoreboardEvent;
    
    private string myName;
    private int myScore;
    private KeyCode myKeyCode;

    private List<PlayerScript> playerList;

    void Start()
    {
        SetupArrays();
        SetColour();
        SetText();
        SetPlayers();
    }

    void Update()
    {
        ChangeButtons();
        CheckButtons();
        PlayerInputs();
    }

    public void SetPlayer(string newMyName, int newMyScore, KeyCode newMyKeyCode)
    {
        myName = newMyName;
        myScore = newMyScore;
        myKeyCode = newMyKeyCode;
    }

    void SetPlayers()
    {
        playerList = new List<PlayerScript>();
        playerList.AddRange(GameObject.FindObjectsOfType<PlayerScript>());

        for (int i = 0; i < playerList.Count; i++)
        {
            print("" + playerList[i].GetName() + "'s keycode is " + playerList[i].GetKeyCodeName().ToString());
        }
    }

    

    void SetupArrays()
    {
        colours = new Color[6];
        names = new string[6];

        colours[0] = new Color(1,0,0,1);
        colours[1] = new Color(0,1,0,1);
        colours[2] = new Color(0,0,1,1);
        colours[3] = new Color(1,0.92f,0.016f,1);
        colours[4] = new Color(0,1,1,1);
        colours[5] = new Color(1,0,1,1);
        
        names[0] = "red";
        names[1] = "green";
        names[2] = "blue";
        names[3] = "yellow";
        names[4] = "cyan";
        names[5] = "magenta";
        
        foreach (Color colour in colours)
        {
            //print("colour " + colour);
        }
        foreach (string name in names)
        {
            //print("name " + name);
        }
    }

    void ChangeButtons()
    {
        if (colourOff)
        {
            colourOff = false;
            StartCoroutine(ChangeCoroutine());
        }
    }

    void CheckButtons()
    {
        GetColour();
        GetText();
    }

    void PlayerInputs()
    {
        if(currentColour == currentText)
        {
            print("match between " + currentColour + " and " + currentText);
            for (int i = 0; i < playerList.Count; i++)
            {
                if (Input.GetKeyDown(playerList[i].GetKeyCode()) && (!pointsAwarded))
                {
                    pointsAwarded = true;
                    playerList[i].AddScore();
                    //if (ScoreboardEvent != null)
                    //{
                        //ScoreboardEvent();
                        //print("player name that just scored: " + playerList[i].GetName());
                    //}
                }
            }
        }
    }
    
    IEnumerator ChangeCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SetColour();
        SetText();
        colourOff = true;
        pointsAwarded = false;
    }
    void SetColour()
    {
         newColourIndex = Random.Range(0, colours.Length - 1);
         buttonLeft.GetComponent<Image>().color = colours[newColourIndex];
    }

    void SetText()
    {
        newTextIndex = Random.Range(0, names.Length - 1);
        buttonLeft.GetComponentInChildren<TextMeshProUGUI>().text = names[newTextIndex];
    }

    void GetColour()
    {
        currentColour = newColourIndex;
    }

    void GetText()
    {
        currentText = newTextIndex;
    }
}
