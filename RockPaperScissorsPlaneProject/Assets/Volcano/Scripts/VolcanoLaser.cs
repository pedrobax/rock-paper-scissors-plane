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
        Debug.Log(hit.point);
        if (hit.point.y > -0.2 || hit.point.y < 0.2)
        {
         
            fireTargetRb.MovePosition(hit.point);
        }

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, fireTarget.transform.position);

        if (trailRenderer.enabled == true)
        {
            for (int i = 0; i < trailRenderer.positionCount; i++)
            {
                Vector3 position = trailRenderer.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                trailRenderer.SetPosition(i, position);

                Vector3 lineSegmentStart = trailRenderer.GetPosition(i - 1);
                Vector3 lineSegmentEnd = trailRenderer.GetPosition(i);

                float distance = (lineSegmentStart - lineSegmentEnd).magnitude;
                RaycastHit[] trailHit = Physics.RaycastAll(lineSegmentStart, lineSegmentEnd - lineSegmentStart, distance);

                foreach(RaycastHit trailHitObject in trailHit)
                {
                    if (trailHitObject.collider.gameObject.tag == "Player")
                    {    
                        Debug.Log("Player hit by laser");
                        PlayerHealth ph = trailHitObject.collider.gameObject.GetComponent<PlayerHealth>();  

                        if (canDamagePlayer && isLaserActive)
                        {
                            switch (currentType)
                        {
                            case LaserType.Rock:
                                switch (ph.currentType)
                                    {
                                        case PlayerHealth.PlayerType.Rock:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                        case PlayerHealth.PlayerType.Paper:
                                            ph.IgnoreDamage();
                                            StartCoroutine(DamageCooldown(1));
                                            break;
                                        case PlayerHealth.PlayerType.Scissors:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                    }
                                    break;

                            case LaserType.Paper:
                                switch (ph.currentType)
                                    {
                                        case PlayerHealth.PlayerType.Rock:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                        case PlayerHealth.PlayerType.Paper:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                        case PlayerHealth.PlayerType.Scissors:
                                            ph.IgnoreDamage();
                                            StartCoroutine(DamageCooldown(1));
                                            break;
                                    }
                                    break;

                            case LaserType.Scissors:
                                switch (ph.currentType)
                                    {
                                        case PlayerHealth.PlayerType.Rock:
                                            ph.IgnoreDamage();
                                            StartCoroutine(DamageCooldown(1));
                                            break;
                                        case PlayerHealth.PlayerType.Paper:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                        case PlayerHealth.PlayerType.Scissors:
                                            ph.DieOrRespawnFunc();
                                            StartCoroutine(DamageCooldown(5));
                                            break;
                                    }
                                    break;
                        }
                        }
                    }
                }
            }

            for (int i = 0; i < rockTrail.positionCount; i++)
            {
                Vector3 position = rockTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                rockTrail.SetPosition(i, position);
            }
            for (int i = 0; i < paperTrail.positionCount; i++)
            {
                Vector3 position = paperTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                paperTrail.SetPosition(i, position);
            }
            for (int i = 0; i < scissorsTrail.positionCount; i++)
            {
                Vector3 position = scissorsTrail.GetPosition(i);
                position += new Vector3(0,0,-trailSpeed) * Time.deltaTime;
                scissorsTrail.SetPosition(i, position);
            }
        }      
    }

    void StartLaser()
    {
        isLaserActive = true;
        lineRenderer.enabled = true;
        trailRenderer.emitting = true;
    }

    void EndLaser()
    {
        isLaserActive = false;
        lineRenderer.enabled = false;
        trailRenderer.emitting= false;
    }

    void ChangeLaserType(int type)
    {
        switch (type)
        {
            case 0:
                currentType = LaserType.Rock;
                break;
            case 1:
                currentType = LaserType.Paper;
                break;
            case 2:
                currentType = LaserType.Scissors;
                break;
        }
    }

    IEnumerator DamageCooldown(int time)
    {
        canDamagePlayer = false;
        yield return new WaitForSeconds(time);
        canDamagePlayer = true;
    }
}
