using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnAction : Action
{
    Vector3 movementVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!isActing && !hasActed)
        {
            StartCoroutine(Spawn());
        }
    }

    public override void Act()
    {
        if (isActing)
        {
            rb.MovePosition(rb.position + movementVelocity / duration * Time.deltaTime);
        }
    }

    private Vector3 startingPosition;

    IEnumerator Spawn()
    {
        startingPosition = transform.position;
        MoveToRandonSpawnPlaceLocation();
        movementVelocity = startingPosition - transform.position;
        isActing = true;
        yield return new WaitForSeconds(duration);
        if (transform.position != startingPosition)
        {
            transform.position = startingPosition;
        }
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
