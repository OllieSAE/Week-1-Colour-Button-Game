using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    public PerlinTerrain perlinTerrain;
    public List<GameObject> cubes;
    public List<Vector3> controlPoints;
    private bool available;
    private Vector3 a, b, c, d, e, point;
    private float speed;

    private void Start()
    {
        available = true;
    }

    private void Update()
    {
        if (cubes == null && perlinTerrain.cubes != null)
        {
            //dunno why this doesn't work?
            //how the fuck do you make a list equal another list
            //it's CLEARLY not null? zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz
            cubes = new List<GameObject>(perlinTerrain.cubes);
            print("cube list should be up");
        }
        
        if (available)
        {
            StartCoroutine(BirdzierPoints());
        }
        //BirdzierCurve();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,point, 0.5f);
    }

    IEnumerator BirdzierPoints()
    {
        available = false;
        yield return new WaitForSeconds(3f);
        controlPoints.Clear();
        controlPoints.Add(cubes[Random.Range(0, cubes.Count)].transform.position);
        controlPoints.Add(cubes[Random.Range(0, cubes.Count)].transform.position);
        controlPoints.Add(cubes[Random.Range(0, cubes.Count)].transform.position);
        controlPoints.Add(cubes[Random.Range(0, cubes.Count)].transform.position);
        // for (int i = 0; i < 4; i++)
        // {
        //     print("hello");
        //     controlPoints[i] = cubes[Random.Range(0, cubes.Count)].transform.position;
        //     print(""+ controlPoints[i]);
        // }
        available = true;
    }
    void BirdzierCurve()
    {
        //this bit needs to be updated to be modular!
        a = Vector3.Lerp(controlPoints[0], controlPoints[1], speed);
        b = Vector3.Lerp(controlPoints[1], controlPoints[2], speed);
        c = Vector3.Lerp(controlPoints[2], controlPoints[3], speed);
        d = Vector3.Lerp(a, b, speed);
        e = Vector3.Lerp(b, c, speed);
        point = Vector3.Lerp(d, e, speed);
        print(""+ controlPoints[3]);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(point,0.1f);
    }
}
