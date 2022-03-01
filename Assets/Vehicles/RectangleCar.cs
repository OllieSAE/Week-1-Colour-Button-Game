using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RectangleCar : MonoBehaviour
{
    private Rigidbody rb;
    public Driver driver;
    public float forwardSpeed;
    public float turnSpeed;
    private Vector3 localVelocity;
    private Vector3 clamp;
    public Thruster thruster;
    public float force;
    public float maxForce;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        clamp = new Vector3(1, 1, 1);
    }

    void Update()
    {
        force = thruster.wheelDiameter - transform.position.y;
        force *= maxForce;
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        if (thruster.grounded)
        {
            rb.AddRelativeForce(0,force,0);
            print("yeet");
        }
    }

    // Update is called once per frame
    public void Forward()
    {
        rb.AddRelativeForce(0, 0, forwardSpeed);
    }

    public void Backward()
    {
        rb.AddRelativeForce(0,0,-forwardSpeed);
    }

    
    //why am I dividing this ???
    public void Left()
    {
        rb.AddRelativeTorque(0, -turnSpeed,0);
        rb.AddRelativeForce(-localVelocity/5);
    }

    public void Right()
    {
        rb.AddRelativeTorque(0, turnSpeed,0);
        rb.AddRelativeForce(-localVelocity/5);
    }

}
