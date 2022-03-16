using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : VehicleBase
{
    void Start()
    {
        forwardSpeed = 10;
        turnSpeed = 2;
        maxForce = 5;
        print("I'm the other car");
    }

}
