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
    //public GameObject buttonRight;
    public GameObject P1Score;
    public GameObject P2Score;

    private bool colourOff = true;
    private bool nameOff;

    private int newColourIndex;
    private int newTextIndex;

    private int currentColour;
    private int currentText;

    private bool pointsAwarded = false;
    private int playerOnePoints;
    private int playerTwoPoints;

    public delegate void ScoreDelegate();
    public static event ScoreDelegate ScoreboardEvent;

    void Start()
    {
        SetupArrays();
        UpdateScoreButtons();
        SetColour();
        SetText();
    }

    void Update()
    {
        ChangeButtons();
        CheckButtons();
        PlayerInputs();
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
            if((Input.GetKeyDown(KeyCode.M)) && (!pointsAwarded))
            {
                pointsAwarded = true;
                if (ScoreboardEvent != null)
                {
                    ScoreboardEvent();
                }
                //playerTwoPoints += 1;
                print("Player Two Points = " + playerTwoPoints);
                //UpdateScoreButtons();
            }

            if ((Input.GetKeyDown(KeyCode.A)) && (!pointsAwarded))
            {
                pointsAwarded = true;
                //playerOnePoints += 1;
                print("Player One Points = " + playerOnePoints);
                //UpdateScoreButtons();
            }
        }
    }
    
    IEnumerator ChangeCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SetColour();
        SetText();
        colourOff = true;
        pointsAwarded = false;
    }
    void SetColour()
    {
         newColourIndex = Random.Range(0, colours.Length - 1);
         buttonLeft.GetComponent<Image>().color = colours[newColourIndex];
         //buttonRight.GetComponent<Image>().color = colours[newColourIndex];
    }

    void SetText()
    {
        newTextIndex = Random.Range(0, names.Length - 1);
        //newText = names[2];
        buttonLeft.GetComponentInChildren<TextMeshProUGUI>().text = names[newTextIndex];
        //buttonRight.GetComponentInChildren<TextMeshProUGUI>().text = names[newTextIndex];
    }

    void GetColour()
    {
        //currentColour = buttonLeft.GetComponent<Image>().color;
        currentColour = newColourIndex;
    }

    void GetText()
    {
        //currentText = buttonLeft.GetComponentInChildren<TextMeshProUGUI>().text;
        currentText = newTextIndex;
    }

    void UpdateScoreButtons()
    {
        P1Score.GetComponentInChildren<TextMeshProUGUI>().text = playerOnePoints.ToString();
        P2Score.GetComponentInChildren<TextMeshProUGUI>().text = playerTwoPoints.ToString();
    }
}
