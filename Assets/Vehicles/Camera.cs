using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    public Transform target;
    
    public float cameraSpeed;
    public Vector3 offset;
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position - offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);
        transform.position = smoothedPosition;
    }
}
