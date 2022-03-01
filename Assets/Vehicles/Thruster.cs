using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private RaycastHit hit;
    public float wheelDiameter;

    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit,wheelDiameter,255,QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(transform.position,hit.point,Color.cyan);
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
