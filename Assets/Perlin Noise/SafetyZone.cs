using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafetyZone : MonoBehaviour
{
    public delegate void EnteredSafety();
    public event EnteredSafety enteredSafetyEvent;
    public event EnteredSafety exitSafetyEvent;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Fish")
        {
            enteredSafetyEvent?.Invoke();
            print("enter");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Fish")
        {
            exitSafetyEvent?.Invoke();
            print("exit");
        }
    }
}
