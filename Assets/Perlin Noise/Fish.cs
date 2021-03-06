using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public SafetyZone safetyZone;
    public Shark shark;
    
    private Vector3 mousePos;
    public float moveSpeed = 0.05f;
    public float respawnTimer = 2f;

    private UnityEngine.Camera cam;
    
    private bool isSafe;

    private void OnEnable()
    {
        safetyZone.enteredSafetyEvent += IsSafe;
        safetyZone.exitSafetyEvent += IsHunted;
        shark.ateTheFishEvent += Respawn;
        cam = UnityEngine.Camera.main;
    }

    private void OnDisable()
    {
        safetyZone.enteredSafetyEvent -= IsSafe;
        safetyZone.exitSafetyEvent -= IsHunted;
        shark.ateTheFishEvent -= Respawn;
    }

    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        
        // Event currentEvent = Event.current;
        // Vector2 point = new Vector2();
        //
        // point.x = currentEvent.mousePosition.x;
        // point.y = cam.pixelHeight - currentEvent.mousePosition.y;

        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        
        if (Input.GetButtonDown("Fire1"))
        {
            print(mousePos);
        }
        
        //if currently hunted...
        if (isSafe == false)
        {
            transform.position = Vector3.Lerp(transform.position, mousePos, moveSpeed);
        }
        //if in safe zone...
        else
        {
            transform.position = Vector3.Lerp(transform.position, mousePos, moveSpeed / 4);
        }
    }

    void IsSafe()
    {
        isSafe = true;
    }

    void IsHunted()
    {
        isSafe = false;
    }

    void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(respawnTimer);
        GetComponent<Renderer>().enabled = true;
    }
}
