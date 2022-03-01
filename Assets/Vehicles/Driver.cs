using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public RectangleCar rectangleCar;
    public KeyCode forward, backward, left, right;

    public GameObject driver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward))
        {
            rectangleCar.Forward();
        }
        
        if (Input.GetKey(backward))
        {
            rectangleCar.Backward();
        }
        
        if (Input.GetKey(left))
        {
            rectangleCar.Left();
        }
        
        if (Input.GetKey(right))
        {
            rectangleCar.Right();
        }
    }
}
