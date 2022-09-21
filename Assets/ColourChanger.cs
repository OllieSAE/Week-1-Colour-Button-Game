using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColourChanger : NetworkBehaviour
{
    private Renderer parentRenderer;
    private Color[] colours;
    private int newColourIndex;

    private void Start()
    {
        parentRenderer = GetComponentInParent<Renderer>();
        colours = new Color[6];
        colours[0] = new Color(1,0,0,1);
        colours[1] = new Color(0,1,0,1);
        colours[2] = new Color(0,0,1,1);
        colours[3] = new Color(1,0.92f,0.016f,1);
        colours[4] = new Color(0,1,1,1);
        colours[5] = new Color(1,0,1,1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickRandomColour();
        }
    }

    private void PickRandomColour()
    {
        if (IsServer)
        {
            newColourIndex = Random.Range(0, colours.Length - 1);
            ChangeColourClientRpc(newColourIndex);
        }
    }

    [ClientRpc]
    private void ChangeColourClientRpc(int index)
    {
        parentRenderer.material.color = colours[index];
    }
}
