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
    TrailRenderer trailRenderer;
    public float trailSpeed = 5;

    void Start()
    {
        fireTargetRb = fireTarget.GetComponent<Rigidbody>();
        lineRenderer = firePoint.GetComponent<LineRenderer>();
        trailRenderer = fireTarget.GetComponent<TrailRenderer>();
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
            }
        }
    }

    void StartLaser()
    {
        lineRenderer.enabled = true;
        trailRenderer.enabled = true;
    }

    void EndLaser()
    {
        lineRenderer.enabled = false;
        trailRenderer.enabled = false;
    }
}
