using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonState : NetworkBehaviour
{
    public Color[] colours;
    public string[] names;

    public GameManager gameManager;
    public GameObject buttonLeft;
    public Image image;
    public TextMeshProUGUI textMeshProUGUI;
    
    private bool colourOff = true;

    private int newColourIndex;
    private int newTextIndex;

    private int currentColour;
    private int currentText;

    public delegate void ButtonMatch();

    public event ButtonMatch ButtonMatchEvent;
    public event ButtonMatch ButtonFailEvent;

    private void OnEnable()
    {
        gameManager.SetUpGameEvent += SetupArrays;
        buttonLeft = this.gameObject;
    }

    private void Start()
    {
        image = buttonLeft.GetComponent<Image>();
        textMeshProUGUI = buttonLeft.GetComponentInChildren<TextMeshProUGUI>();
        //ChangeButtonState();
    }
    
    public void ChangeButtonState()
    {
        if (colourOff)
        {
            colourOff = false;
            StartCoroutine(ChangeCoroutine());
        }
    }
    IEnumerator ChangeCoroutine()
    {
        var random = Random.Range(0.8f, 2.5f);
        yield return new WaitForSeconds(random);
        ChangeColour();
        ChangeText();
        colourOff = true;
        CheckButtonState();
        ChangeButtonState();
    }

    void CheckButtonState()
    {
        if (currentColour == currentText)
        {
            ButtonMatchEvent?.Invoke();
            print("match");
            CallClientMatchEventClientRpc();
        }

        if (currentColour != currentText)
        {
            ButtonFailEvent?.Invoke();
            print("fail");
            CallClientFailEventClientRpc();
        }
    }
    
    [ClientRpc]
    void CallClientMatchEventClientRpc() //this is not working as intended
    {
        if (IsClient && !IsServer)
        {
            ButtonMatchEvent?.Invoke();
        }
    }

    [ClientRpc]
    void CallClientFailEventClientRpc() //this is not working as intended
    {
        if (IsClient && !IsServer)
        {
            ButtonFailEvent?.Invoke();
            print("button fail event");
        }
    }
    
    void SetupArrays()
    {
       SetColourArray();
       SetTextArray();
    }

    void SetColourArray()
    {
        colours = new Color[6];
        colours[0] = new Color(1,0,0,1);
        colours[1] = new Color(0,1,0,1);
        colours[2] = new Color(0,0,1,1);
        colours[3] = new Color(1,0.92f,0.016f,1);
        colours[4] = new Color(0,1,1,1);
        colours[5] = new Color(1,0,1,1);
        foreach (Color colour in colours)
        {
            //print("colour " + colour);
        }
    }

    void SetTextArray()
    {
        names = new string[6];
        names[0] = "red";
        names[1] = "green";
        names[2] = "blue";
        names[3] = "yellow";
        names[4] = "cyan";
        names[5] = "magenta";
        foreach (string name in names)
        {
            //print("name " + name);
        }
    }
    
    
    void ChangeColour()
    {
        newColourIndex = Random.Range(0, colours.Length - 1);
        if(IsServer) SetClientColourClientRpc(newColourIndex);
        
        //image.color = colours[newColourIndex];
        //currentColour = newColourIndex;
    }

    void ChangeText()
    {
        newTextIndex = Random.Range(0, names.Length - 1);
        if(IsServer) SetClientTextClientRpc(newTextIndex);
        
        //textMeshProUGUI.text = names[newTextIndex];
        //currentText = newTextIndex;
    }

    [ClientRpc]
    void SetClientColourClientRpc(int index)
    {
        image.color = colours[index];
        currentColour = index;
    }

    [ClientRpc]
    void SetClientTextClientRpc(int index)
    {
        textMeshProUGUI.text = names[index];
        currentText = index;
    }
}
