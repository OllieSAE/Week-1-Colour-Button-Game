using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public SafetyZone safetyZone;
    public Transform target;
    
    private Vector3 myPos;
    private Vector3 nextPos;
    public Vector3 startPos;
    public Vector3 endPos;

    public float chaseSpeed;
    public float patrolSpeed;

    private bool preyActive;
    private bool digesting;
    
    public delegate void AteTheFish();
    public event AteTheFish ateTheFishEvent;

    private void OnEnable()
    {
        safetyZone.enteredSafetyEvent += PreySafe;
        safetyZone.exitSafetyEvent += PreyActive;
        myPos = transform.position;
        nextPos = endPos;
        preyActive = true;
    }

    private void OnDisable()
    {
        safetyZone.enteredSafetyEvent -= PreySafe;
        safetyZone.exitSafetyEvent -= PreyActive;
    }

    void FixedUpdate()
    {
        print(""+preyActive);
        print(""+nextPos);
        myPos = transform.position;
        if (preyActive && !digesting)
        {
            print("nomnom time");
            Vector3 desiredPosition = target.position;
            Vector3 smoothedPosition = Vector3.Lerp(myPos, desiredPosition, chaseSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            if (transform.position == startPos)
            {
                nextPos = endPos;
            }

            if (transform.position == endPos)
            {
                nextPos = startPos;
            }

            transform.position = Vector3.MoveTowards(myPos, nextPos, patrolSpeed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Fish")
        {
            ateTheFishEvent?.Invoke();
            print("delicious");
            StartCoroutine("DigestionCoroutine");
        }
    }

    private void PreyActive()
    {
        preyActive = true;
    }

    private void PreySafe()
    {
        preyActive = false;
    }

    public IEnumerator DigestionCoroutine()
    {
        digesting = true;
        yield return new WaitForSeconds(3f);
        digesting = false;
    }
}
