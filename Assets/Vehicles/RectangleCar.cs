using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class RectangleCar : MonoBehaviour
{
    private Rigidbody rb;
    public Driver driver;
    public float forwardSpeed;
    public float turnSpeed;
    public Vector3 localVelocity;
    private Vector3 clamp;
    public Thruster thruster;
    public float force;
    public float maxForce;

    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);
    }


}
