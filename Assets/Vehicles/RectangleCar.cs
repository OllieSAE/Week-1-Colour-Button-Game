using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class RectangleCar : VehicleBase
{
    public void Start()
    {
        forwardSpeed = 20;
        turnSpeed = 2;
        maxForce = 5;
        print("I'm the rectangle car");
    }
}
