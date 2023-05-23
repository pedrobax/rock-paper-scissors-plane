using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTest : MonoBehaviour
{
    public GameObject volcano;
    public GameObject cellsParent;
    public float explosionForce = 1;
    public Vector3 explosionPosition;
    public float explosionRadius;
    public float upwardsModifier = 0.0f;
    public float gravity = 100;
    public GameObject volcanoPrefab;
    public GameObject phase2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyVolcano();
            Debug.Log("Volcano destroyed!");
        }       
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(volcanoPrefab, transform.position, transform.rotation);
        }
    }

    void FixedUpdate()
    {
        if (cellsParent.activeSelf == true)
        {
            foreach (Rigidbody cell in cellsParent.GetComponentsInChildren<Rigidbody>())
            {
                cell.AddForce(Vector3.down * gravity);
                cell.MovePosition(new Vector3(cell.position.x, cell.position.y, cell.position.z - 6 * Time.fixedDeltaTime));
            }
        }
    }
    
    void DestroyVolcano()
    {
        volcano.SetActive(false);
        cellsParent.SetActive(true);
        phase2.SetActive(true);

        foreach (Rigidbody cell in cellsParent.GetComponentsInChildren<Rigidbody>())
        {
            cell.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
        }
    }
}
