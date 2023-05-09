using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEaseAction : Action
{
    /* This action is used to move the antagonist to its starting location from a set point or a random point.
     * The movement has and easing in and easing out effect.
     * When used, it should always be the first action in the antagonist's action list.
     * 
     * The way it works is, it registers the antagonist's starting position, as set in the scene by the designer,
     * when the action starts the antagonist will teleport to the set position, or a random position if the 
     * randomSpawnPosition bool is set to true, and then move back to its starting position with a speed based on the
     * duration. There's also a wait time variable, that will make the antagonist wait for the set time before moving
     * into place.
     * 
     * There are 5 different ways to spawn the antagonist: Vertical, Horizontal, Left, Right and Set Position.
     * Set position is the only one that doesn't use the randomSpawnPosition bool, since it's set by the designer.
     * 
     * Vertical starts from a random position above the level area, and moves down to the starting position.
     * Horizontal starts from a random position to the sides of the level area, and moves to the starting position.
     * Right starts from a random position to the right of the level area, and moves to the starting position.
     * Left starts from a random position to the left of the level area, and moves to the starting position.
     */

    Vector3 movementStartPosition; //the position the antagonist will move from
    public float waitTime = 1; 
    private float movementTime; //the time it takes for the antagonist to move from the spawn position to the starting position
    float elapsedTime;
    float startTime;
    public bool randomSpawnPosition; //the bool that determines if the antagonist will spawn in a random position or not
    public Vector3 spawnPosition; //the final position the antagonist will move to
    public SpawnType spawnType = SpawnType.Vertical; //sets the type of spawn the antagonist will use

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

    //moves from set position to spawn starting position
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

    //saves the starting position, moves to spawn position, and then moves back to starting position
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

    //moves the antagonist to a random position based on the spawn type
    //each type has set parameters to determine the area available for the random position
    void MoveToRandonSpawnPlaceLocation()
    {
        if (spawnType == SpawnType.Vertical)
        {
            Vector3 minPosition = new Vector3(-50, 40, 0);
            Vector3 maxPosition = new Vector3(50, 40, 100);
            transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
        }
        else if (spawnType == SpawnType.Horizontal)
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
        else if (spawnType == SpawnType.Left)
        {
            Vector3 minPosition = new Vector3(-100, 0, 0);
            Vector3 maxPosition = new Vector3(-50, 0, 0);
            transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
        }
        else if (spawnType == SpawnType.Right)
        {
            Vector3 minPosition = new Vector3(100, 0, 0);
            Vector3 maxPosition = new Vector3(50, 0, 0);
            transform.position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
        }
    }

    //easing function for the movement
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

    public enum SpawnType { Vertical, Horizontal, Left, Right };
}
