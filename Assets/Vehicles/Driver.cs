using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public RectangleCar rectangleCar;
    public KeyCode forward, backward, left, right;
    public Wheel wheel1, wheel2, wheel3, wheel4;
    private List<Wheel> wheels;

    public GameObject driver;
    // Start is called before the first frame update
    void Start()
    {
        wheels = new List<Wheel>();
        wheels.Add(wheel1);
        wheels.Add(wheel2);
        wheels.Add(wheel3);
        wheels.Add(wheel4);
    }

    void Update()
    {
        if (Input.GetKey(forward))
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.Forward();
            }
        }
        
        if (Input.GetKey(backward))
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.Backward();
            }
        }
        
        if ((Input.GetKey(left))&&((Input.GetKey(forward)||(Input.GetKey(backward)))))
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.Left();
            }
        }
        
        if ((Input.GetKey(right))&&((Input.GetKey(forward)||(Input.GetKey(backward)))))
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.Right();
            }
        }
        
        if (Input.GetKey(left)==false&&(Input.GetKey(right)==false))
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.Straight();
            }
        }
    }
}
