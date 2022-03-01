using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float noiseScale;
    private float threshold;
    private float uniqueX, uniqueY, uniqueZ, uniqueTimer;
    private Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        noiseScale = PerlinTerrain.noiseScale;
        threshold = PerlinTerrain.threshold;
        uniqueX = Random.Range(0, 10f);
        uniqueY = Random.Range(0, 10f);
        uniqueZ = Random.Range(0, 10f);
        uniqueTimer = Random.Range(0, 4f);
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Attempt At NoiseS3D scale fluctuation

        //float noiseValue = (float)NoiseS3D.Noise(noiseScale, noiseScale, noiseScale);
        //transform.localScale = new Vector3(noiseValue*(Random.Range(0,1f)),noiseValue*(Random.Range(0,1f)),noiseValue*(Random.Range(0,1f)));
        
        //transform.localScale = new Vector3((float)NoiseS3D.Noise((Random.Range(0, 1f))),transform.position.y,transform.position.z);
        //print(""+ transform.localScale.ToString());

        //below works with PerlinNoise (Cam's solution)

        #endregion
        
        transform.localScale = new Vector3(Mathf.PerlinNoise(Time.time+uniqueX, 0), Mathf.PerlinNoise(Time.time+uniqueY, 0),Mathf.PerlinNoise(Time.time+uniqueZ, 0));

        ChangeColour();
    }

    void ChangeColour()
    {
        int R = Random.Range(0, 10);
        if ((renderer.material.color == Color.red || renderer.material.color == Color.blue ||
             renderer.material.color == Color.green||renderer.material.color == Color.white))
        {
            if (R <= 3)
            {
                renderer.material.DOColor(Color.red,uniqueTimer);
            }
            else if (R <= 6)
            {
                renderer.material.DOColor(Color.blue, uniqueTimer);
            }
            else
            {
                renderer.material.DOColor(Color.green, uniqueTimer);
            }
        }
    }

    #region Cam's Maths Approach
    
    //TAKE TWO OR THREE NUMBERS YOU'RE GETTING AND CONVERT THEM TO WHAT YOU WANT
    //EG
    //HAVE: -1, 0, 1, 0.25, 0.6
    //WANT: 0, 0.5, 1, 0.25, 0.6
    //x 0.5
    //+1 is bad, * whatever is bad, +1 then /2 is GOOD

    #endregion
}
