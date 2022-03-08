using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PerlinTerrain : MonoBehaviour
{
    public GameObject manager;
    public GameObject cubePrefab;
    public int cubeCount;
    public List<GameObject> cubes;
    
    [SerializeField] public static float noiseScale = 0.15f;
    [SerializeField] public static float threshold = 0.5f;
    public static float sandThreshold = 0.5f;
    public static float waterThreshold = -0.75f;

    void Start()
    {
        //toggle between GenerateCube() and GenerateTerrain() to look at an individual cube's properties or all the cubes
        
        //GenerateCube();
        Generate2DTerrain();
        //Generate3DTerrain();
        foreach (Transform child in transform)
        {
            cubes.Add(child.gameObject);
        }
        print("Cube count = " +cubes.Count);
    }

    public static float Perlin3D(float x, float y, float z)
    {
        //get all 3 permutations of noise for X, Y and Z
        float AB = Mathf.PerlinNoise(x, y);
        float BC = Mathf.PerlinNoise(y,z);
        float AC = Mathf.PerlinNoise(y,z);

        //and in reverse
        float BA = Mathf.PerlinNoise(y, x);
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, y);
        
        //return the average
        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f;
    }


    void Generate2DTerrain()
    {
        for (int i = 0; i <= 2*cubeCount; i++)
        {
            for (int j = 0; j <= 2 * cubeCount; j++)
            {
                float noiseValue = (float)NoiseS3D.Noise(i*noiseScale, j*noiseScale);
                
                if (noiseValue >= sandThreshold)
                {
                    GameObject go = Instantiate(cubePrefab);
                    go.transform.parent = gameObject.transform;
                    go.transform.position = new Vector3(i, 0, j);
                    go.GetComponent<Renderer>().material.color = Color.yellow;
                }
                else if (noiseValue >= waterThreshold)
                {
                    GameObject go = Instantiate(cubePrefab);
                    go.transform.parent = gameObject.transform;
                    go.transform.position = new Vector3(i, 0, j);
                    go.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    GameObject go = Instantiate(cubePrefab);
                    go.transform.parent = gameObject.transform;
                    go.transform.position = new Vector3(i, 0, j);
                    go.GetComponent<Renderer>().material.color = Color.green;
                }

                print("" + noiseValue);
            }
        }
    }
    void Generate3DTerrain()
    {
        for (int i = 0; i <= 2*cubeCount; i++)
        {
            for (int j = 0; j <= 2*cubeCount; j++)
            {
                for (int k = 0; k <= 2 * cubeCount; k++)
                {
                    #region using Manual Perlin3D static float from YT tutorial
                    // float noiseValue = Perlin3D(i * noiseScale, j * noiseScale, k * noiseScale);
                    // if (noiseValue >= threshold)
                    // {
                    //     GameObject go = Instantiate(cubePrefab);
                    //     go.transform.parent = gameObject.transform;
                    //     go.transform.position = new Vector3(i, j, k);
                    // }
                    #endregion

                    //using Simple 3D Noise plugin
                    //use THRESHOLD to determine whether you're spawning or NOT spawning
                    float noiseValue = (float)NoiseS3D.Noise(i*noiseScale, j*noiseScale, k*noiseScale);
                    if (noiseValue >= threshold)
                    {
                        GameObject go = Instantiate(cubePrefab);
                        go.transform.parent = gameObject.transform;
                        go.transform.position = new Vector3(cubeCount, cubeCount, cubeCount);
                        Vector3 goalPos = new Vector3(i, j, k);
                        go.transform.DOMove(goalPos, 5f);
                    }
                }
            }
        }
    }

    //used as a tester to check a single cube's scale fluctuations
    void GenerateCube()
    {
        GameObject go = Instantiate(cubePrefab);
        go.transform.parent = gameObject.transform;
        go.transform.position = new Vector3(0, 0, 0);
    }
}
