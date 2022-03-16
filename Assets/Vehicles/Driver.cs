using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public KeyCode forward, backward, left, right;
    public Wheel wheel1, wheel2, wheel3, wheel4;
    private List<Wheel> wheels;
    public VehicleBase currentCar;
    public VehicleBase newCar;
    public VehicleManager vehicleManager;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (VehicleBase car in vehicleManager.vehicles)
        {
            if (car != currentCar)
            {
                newCar = car;
            }
        }

        currentCar = newCar;
        wheels = currentCar.wheels;
        print(wheels);
        print(currentCar);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeCar();
        }
        Drive();
    }

    void Drive()
    {
        if (Input.GetKey(forward))
        {
            foreach (Wheel wheel in currentCar.wheels)
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

    void ChangeCar()
    {
        currentCar.Activate();
        foreach (VehicleBase car in vehicleManager.vehicles)
        {
            if (car != currentCar)
            {
                newCar = car;
            }
        }

        currentCar = newCar;
        wheels = currentCar.wheels;
        print(currentCar);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        currentCar = other.gameObject.GetComponent<VehicleBase>();
    }
}
