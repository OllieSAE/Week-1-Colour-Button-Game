using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Thruster : MonoBehaviour
{
    public RaycastHit hit;
    public RectangleCar rectangleCar;
    public float wheelDiameter;
    public Rigidbody rb;

    public float force;
    public float maxForce;
    private Vector3 myPos;

    public Wheel wheel;
    
    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
        rb = this.GetComponentInParent<Rigidbody>();
        wheelDiameter = this.GetComponentInParent<CapsuleCollider>().radius+0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        force = wheelDiameter;
        maxForce = rectangleCar.maxForce;
        force *= maxForce;
        myPos = transform.position;
    }
    
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, wheelDiameter,255,QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(transform.position,hit.point,Color.cyan);
            wheel.grounded = true;
            Thrust();
        }
        else
        {
            wheel.grounded = false;
        }
    }

    void Thrust()
    {
        if (wheel.grounded)
        {
            rb.AddForceAtPosition(new Vector3(0,(int)force/(hit.distance/0.5f),0),myPos, ForceMode.Acceleration);
        }
    }
    
    
}
