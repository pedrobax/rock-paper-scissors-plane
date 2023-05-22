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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyVolcano();
            Debug.Log("Volcano destroyed!");
        }
    }

    void DestroyVolcano()
    {
        volcano.SetActive(false);
        cellsParent.SetActive(true);

        foreach (Rigidbody cell in cellsParent.GetComponentsInChildren<Rigidbody>())
        {
            cell.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
        }
    }
}
