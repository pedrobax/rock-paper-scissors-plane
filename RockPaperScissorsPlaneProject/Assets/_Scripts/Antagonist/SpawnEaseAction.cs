using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEaseAction : Action
{
    Vector3 movementStartPosition;
    public float waitTime = 1;
    private float movementTime;
    float elapsedTime;
    float startTime;
    public bool randomSpawnPosition;
    public Vector3 spawnPosition;
    public SpawnType spawnType = SpawnType.vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
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
            float t = (Time.fixedTime - (startTime + waitTime)) / movementTime;
            t = EaseInOutQuad(t);
            rb.MovePosition(Vector3.Lerp(movementStartPosition, startingPosition, t));
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
        if (randomSpawnPosition) MoveToRandonSpawnPlaceLocation();
        else transform.position = spawnPosition;
        movementStartPosition = transform.position;
        startTime = Time.time;
        isActing = true;
        while (Time.time - startTime < duration)
        {
            yield return new WaitForFixedUpdate();
        }
        isActing = false;
        hasActed = true;
    }

    void MoveToRandonSpawnPlaceLocation()
    {
        if (spawnType == SpawnType.vertical)
        {
            Vector3 minPosition = new Vector3(-50, 40, 0);
            Vector3 maxPosition = new Vector3(50, 40, 100);
            transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
        }
        else if (spawnType == SpawnType.horizontal)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Vector3 minPosition = new Vector3(-100, 0, 0);
                Vector3 maxPosition = new Vector3(-50, 0, 0);
                transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
            }
            else
            {
                Vector3 minPosition = new Vector3(50, 0, 0);
                Vector3 maxPosition = new Vector3(100, 0, 0);
                transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
            }
        }
    }

    private float EaseInOutQuad(float t)
    {
        t = Mathf.Clamp01(t);
        if (t < 0.5f)
        {
            return 2 * t * t;
        }
        else
        {
            return -1 + (4 - 2 * t) * t;
        }
    }

    public enum SpawnType { vertical, horizontal };
}
