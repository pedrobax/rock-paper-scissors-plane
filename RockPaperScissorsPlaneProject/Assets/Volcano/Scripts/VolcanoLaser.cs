using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoLaser : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject fireTarget;
    public Material rockLaserMat, paperLaserMat, scissorsLaserMat;
    Rigidbody fireTargetRb;
    LayerMask layerMask = 1 << 5;
    RaycastHit hit;
    LineRenderer lineRenderer;
    public GameObject rockTrailObj, paperTrailObj, scissorsTrailObj;
    GameObject currentTrail;
    public float trailSpeed = 5;
    public enum LaserType {Rock, Paper, Scissors};
    public LaserType currentType = LaserType.Rock;

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
         
            fireTargetRb.MovePosition(new Vector3(hit.point.x, 0, hit.point.z));
        }

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, fireTarget.transform.position); 
    }

    void StartLaser()
    {
        firePoint.GetComponent<AudioSource>().Play();
        lineRenderer.enabled = true;
        currentTrail = Instantiate(rockTrailObj, fireTarget.transform.position, Quaternion.identity);
        currentTrail.transform.parent = fireTarget.transform;
    }

    void EndLaser()
    {
        lineRenderer.enabled = false;
        currentTrail.transform.parent = null;
    }

    void ChangeLaserType(int type)
    {
        switch (type)
        {
            case 0:
                currentType = LaserType.Rock;
                currentTrail.transform.parent = null;
                lineRenderer.material = rockLaserMat;
                currentTrail = Instantiate(rockTrailObj, fireTarget.transform.position, Quaternion.identity);
                currentTrail.transform.parent = fireTarget.transform;
                break;
            case 1:
                currentType = LaserType.Paper;
                currentTrail.transform.parent = null;
                lineRenderer.material = paperLaserMat;
                currentTrail = Instantiate(paperTrailObj, fireTarget.transform.position, Quaternion.identity);
                currentTrail.transform.parent = fireTarget.transform;
                break;
            case 2:
                currentType = LaserType.Scissors;
                currentTrail.transform.parent = null;
                lineRenderer.material = scissorsLaserMat;
                currentTrail = Instantiate(scissorsTrailObj, fireTarget.transform.position, Quaternion.identity);
                currentTrail.transform.parent = fireTarget.transform;
                break;
        }
    }
}
