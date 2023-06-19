using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoLaserTrail : MonoBehaviour
{
    public bool isActive = false;
    public bool canDamagePlayer = true;
    float trailSpeed = 5;
    public enum LaserType {Rock, Paper, Scissors};
    public LaserType type = LaserType.Rock;
    TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < trailRenderer.positionCount; i++)
        {
                Vector3 position = trailRenderer.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                trailRenderer.SetPosition(i, position);

                if(i == 0)
                {
                    i++;
                    Vector3 position2 = trailRenderer.GetPosition(i);
                    position2 += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                    trailRenderer.SetPosition(i, position2);
                } 

                Vector3 lineSegmentStart = trailRenderer.GetPosition(i - 1);
                Vector3 lineSegmentEnd = trailRenderer.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(lineSegmentStart, lineSegmentEnd);
        }
    }
    
    void Update()
    {
        if(transform.parent == null)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
        if(!isActive)
            {
                transform.position += new Vector3(0,0,-trailSpeed * 2) * Time.deltaTime;
            }  
    }

    void FixedUpdate()
    {
        for (int i = 0; i < trailRenderer.positionCount; i++)
            {
                Vector3 position = trailRenderer.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                trailRenderer.SetPosition(i, position);

                if(i == 0)
                {
                    i++;
                    Vector3 position2 = trailRenderer.GetPosition(i);
                    position2 += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                    trailRenderer.SetPosition(i, position2);
                } 

                Vector3 lineSegmentStart = trailRenderer.GetPosition(i - 1);
                Vector3 lineSegmentEnd = trailRenderer.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                foreach(RaycastHit trailHitObject in trailHit)
                {
                    if (trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && type == LaserType.Rock)
                    {    
                        Debug.Log("player hit by ROCK laser");
                        PlayerHealth ph = trailHitObject.collider.gameObject.GetComponent<PlayerHealth>();  
                        switch (ph.currentType)
                        {
                            case PlayerHealth.PlayerType.Rock:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break;
                            case PlayerHealth.PlayerType.Paper:
                                ph.IgnoreDamage();
                                break;
                            case PlayerHealth.PlayerType.Scissors:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break;
                        }
                    }
                    else if(trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && type == LaserType.Paper)
                    {
                        Debug.Log("player hit by Paper laser");
                        PlayerHealth ph = trailHitObject.collider.gameObject.GetComponent<PlayerHealth>();  
                        switch (ph.currentType)
                        {
                            case PlayerHealth.PlayerType.Rock:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break;
                            case PlayerHealth.PlayerType.Paper:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break;
                            case PlayerHealth.PlayerType.Scissors:
                                ph.IgnoreDamage();
                                break; 
                        }
                    } 
                    else if(trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && type == LaserType.Scissors)
                    {
                        Debug.Log("player hit by Scissors laser");
                        PlayerHealth ph = trailHitObject.collider.gameObject.GetComponent<PlayerHealth>();  
                        switch (ph.currentType)
                        {
                            case PlayerHealth.PlayerType.Rock:
                                ph.IgnoreDamage();
                                break;
                            case PlayerHealth.PlayerType.Paper:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break;
                            case PlayerHealth.PlayerType.Scissors:
                                ph.DieOrRespawnFunc();
                                StartCoroutine(DamageCooldown(3));
                                break; 
                         }
                    } 
            }
        }
    }

    IEnumerator DamageCooldown(int time)
    {
        canDamagePlayer = false;
        yield return new WaitForSeconds(time);
        canDamagePlayer = true;
    }

}
