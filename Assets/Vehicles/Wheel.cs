using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool grounded;
    public Rigidbody rb;
    public RectangleCar rectangleCar;
    public Thruster thruster;
    private Vector3 localVelocity;
    private Vector3 myPos;

    private void Update()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        myPos = transform.position;
    }

    public void Forward()
    {
        if (grounded)
        {
            //rb.AddRelativeForce(0, 0, rectangleCar.forwardSpeed/4);
            rb.AddForceAtPosition(new Vector3(0,0,1),myPos);
        }
    }

    public void Backward()
    {
        if (grounded)
        {
            //rb.AddRelativeForce(0,0,-rectangleCar.forwardSpeed/4);
            rb.AddForceAtPosition(new Vector3(0,0,-1),myPos);
        }
    }

    //why am I dividing this ???
    public void Left()
    {
        if (grounded)
        {
            //rb.AddRelativeTorque(0, -rectangleCar.turnSpeed/4,0);
            
            //rb.AddRelativeForce(-rectangleCar.localVelocity/5);
            if (transform.localPosition.z >= 0)
            {
                //GetComponent<Renderer>().material.color = Color.red;
                transform.localRotation = Quaternion.Euler(90,90,45);
            }
        }
    }
    
    public void Right()
    {
        if (grounded)
        {
            //rb.AddRelativeTorque(0, rectangleCar.turnSpeed/4,0);
            
            //rb.AddRelativeForce(-rectangleCar.localVelocity/5);
            if (transform.localPosition.z >= 0)
            {
                //GetComponent<Renderer>().material.color = Color.red;
                transform.localRotation = Quaternion.Euler(90,90,-45);
            }
        }
    }

    public void Straight()
    {
        transform.localRotation = Quaternion.Euler(90,90,0);
    }
}
