using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointsManager : MonoBehaviour
{
    public List<Transform> controlPoints;
    void Start()
    {
        controlPoints = new List<Transform>(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            controlPoints.Add(transform.GetChild(i));
        }
        print("Control Points Transforms count = " +controlPoints.Count);
    }
}
