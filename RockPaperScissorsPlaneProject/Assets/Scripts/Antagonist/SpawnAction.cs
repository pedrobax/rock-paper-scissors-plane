using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnAction : Action
{
    Vector3 movementVelocity;
    public float waitTime = 1;
    private float movementTime;
    float elapsedTime;
    float startTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementTime = duration - waitTime;
        if (!isActing && !hasActed)
        {
            StartCoroutine(Spawn());
        }
    }

    public override void Act()
    {
        elapsedTime = Time.time - startTime;
        if (isActing && elapsedTime > waitTime)
        {
            rb.MovePosition(rb.position + movementVelocity / movementTime * Time.deltaTime);
        }
        else if (isActing && elapsedTime < waitTime)
        {
            Debug.Log("Waiting to spawn");
        }
        if (hasActed == true)
        {
            rb.MovePosition(startingPosition);
        }
    }

    private Vector3 startingPosition;

    IEnumerator Spawn()
    {
        startingPosition = transform.position;
        MoveToRandonSpawnPlaceLocation();
        movementVelocity = startingPosition - transform.position;
        startTime = Time.time;
        isActing = true;
        yield return new WaitForSeconds(duration);            
        hasActed = true;
    }

    void MoveToRandonSpawnPlaceLocation()
    {
        Vector3 minPosition = new Vector3(-50, 40, 0);
        Vector3 maxPosition = new Vector3(50, 40, 100);
        transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
    }
}
