using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoLaser : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject fireTarget;
    Rigidbody fireTargetRb;
    LayerMask layerMask = 1 << 5;
    RaycastHit hit;
    LineRenderer lineRenderer;
    public TrailRenderer trailRenderer, rockTrail, paperTrail, scissorsTrail;
    public float trailSpeed = 5;
    public enum LaserType {Rock, Paper, Scissors};
    public LaserType currentType = LaserType.Rock;
    bool canDamagePlayer = true;
    bool isLaserActive = false;

    void Start()
    {
        fireTargetRb = fireTarget.GetComponent<Rigidbody>();
        lineRenderer = firePoint.GetComponent<LineRenderer>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(firePoint.transform.position, hit.point);
    }

    void FixedUpdate()
    {   
        Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, Mathf.Infinity);
        if (hit.point.y > -0.2 || hit.point.y < 0.2)
        {
         
            fireTargetRb.MovePosition(hit.point);
        }

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, fireTarget.transform.position);

        if (trailRenderer.enabled == true)
        {
            for (int i = 0; i < rockTrail.positionCount; i++)
            {
                Vector3 position = rockTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                rockTrail.SetPosition(i, position);

                Vector3 lineSegmentStart = rockTrail.GetPosition(i - 1);
                Vector3 lineSegmentEnd = rockTrail.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                foreach(RaycastHit trailHitObject in trailHit)
                {
                    if (trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && isLaserActive)
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
                }       
            }
            for (int i = 0; i < paperTrail.positionCount; i++)
            {
                Vector3 position = paperTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                paperTrail.SetPosition(i, position);

                Vector3 lineSegmentStart = paperTrail.GetPosition(i - 1);
                Vector3 lineSegmentEnd = paperTrail.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                foreach(RaycastHit trailHitObject in trailHit)
                {
                    if (trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && isLaserActive)
                    {    
                        Debug.Log("player hit by PAPER laser");
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
                }    
            }
            for (int i = 0; i < scissorsTrail.positionCount; i++)
            {
                Vector3 position = scissorsTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                scissorsTrail.SetPosition(i, position);

                Vector3 lineSegmentStart = scissorsTrail.GetPosition(i - 1);
                Vector3 lineSegmentEnd = scissorsTrail.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                foreach(RaycastHit trailHitObject in trailHit)
                {
                    if (trailHitObject.collider.gameObject.tag == "Player" && canDamagePlayer && isLaserActive)
                    {    
                        Debug.Log("player hit by SCISSORS laser");
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
    }

    void StartLaser()
    {
        isLaserActive = true;
        lineRenderer.enabled = true;
        rockTrail.emitting = true;
    }

    void EndLaser()
    {
        isLaserActive = false;
        lineRenderer.enabled = false;
        rockTrail.emitting = false;
        paperTrail.emitting = false;
        scissorsTrail.emitting = false;
    }

    void ChangeLaserType(int type)
    {
        /*switch (type)
        {
            case 0:
                currentType = LaserType.Rock;
                rockTrail.emitting = true;
                paperTrail.emitting = false;
                scissorsTrail.emitting = false;
                break;
            case 1:
                currentType = LaserType.Paper;
                rockTrail.emitting = false;
                paperTrail.emitting = true;
                scissorsTrail.emitting = false;
                break;
            case 2:
                currentType = LaserType.Scissors;
                rockTrail.emitting = false;
                paperTrail.emitting = false;
                scissorsTrail.emitting = true;
                break;
        }*/
    }

    IEnumerator DamageCooldown(int time)
    {
        canDamagePlayer = false;
        yield return new WaitForSeconds(time);
        canDamagePlayer = true;
    }
}
