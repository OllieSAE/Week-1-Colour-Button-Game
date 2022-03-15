using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleBase : MonoBehaviour
{
    private Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeed;
    public Vector3 localVelocity;
    private Vector3 clamp;
    public float force;
    public float maxForce;
    public Wheel wheel1, wheel2, wheel3, wheel4;
    public List<Wheel> wheels;

    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        wheels = new List<Wheel>();
        wheels.Add(wheel1);
        wheels.Add(wheel2);
        wheels.Add(wheel3);
        wheels.Add(wheel4);
    }

    void Update()
    {
        //localVelocity = transform.InverseTransformDirection(rb.velocity);
    }

    public void Activate()
    {
        foreach (Wheel wheel in wheels)
        {
            enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
