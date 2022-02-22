using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointsManager : MonoBehaviour
{
    public Transform[] controlPoints;
    void Start()
    {
        controlPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            controlPoints[i] = transform.GetChild(i);
        }
        print(controlPoints.Length);
    }
}
