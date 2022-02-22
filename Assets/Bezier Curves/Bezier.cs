using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Bezier : MonoBehaviour
{
    public Manager manager;
    public ControlPointsManager controlPointsManager;

    private Vector3 a, b, c, d, e, point;
    private Vector3 cp1, cp2, cp3, cp4;
    private float speed;
    private bool controlPointsGenerated;

    private Vector3[] bezierControlPoints;
    
    //CHANGE EVERYTHING TO LISTS
    //NOT ARRAYS
    //EASIER TO ADD
    
    void Start()
    {
        bezierControlPoints = new[] {cp1, cp2, cp3, cp4};
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPointsManager.controlPoints.Length > 0 && !controlPointsGenerated)
        {
            GenerateControlPoints();
        }

        if (controlPointsGenerated)
        {
            BezierCurve();
        }

        speed = manager.timer;
    }

    void GenerateControlPoints()
    {
        for (int i = 0; i < controlPointsManager.controlPoints.Length; i++)
        {
            bezierControlPoints[i] = controlPointsManager.controlPoints[i].transform.position;
        }
        controlPointsGenerated = true;
    }

    void BezierCurve()
    {
        a = Vector3.Lerp(bezierControlPoints[0], bezierControlPoints[1], speed);
        b = Vector3.Lerp(bezierControlPoints[1], bezierControlPoints[2], speed);
        c = Vector3.Lerp(bezierControlPoints[2], bezierControlPoints[3], speed);
        d = Vector3.Lerp(a, b, speed);
        e = Vector3.Lerp(b, c, speed);
        point = Vector3.Lerp(d, e, speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(point,0.1f);
    }
}
