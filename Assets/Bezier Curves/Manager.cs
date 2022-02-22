using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float timer;
    private float timeScale;
    
    void Start()
    {
        timeScale = Time.deltaTime;
    }
    
    void Update()
    {
        TimeFlip();
    }

    void TimeFlip()
    {
        timer += timeScale;
        if (timer >= 1)
        {
            timer = 1;
            timeScale = Time.deltaTime * -1;
        }

        if (timer <= 0)
        {
            timer = 0;
            timeScale = Time.deltaTime;
        }
    }
}
