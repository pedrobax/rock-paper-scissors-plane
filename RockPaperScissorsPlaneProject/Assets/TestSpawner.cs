using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject pedrito;
    public GameObject papelito;
    public GameObject tesourito;
    [SerializeField] Enemy enemy;

    public void SpawnEnemy()
    {
        if (enemy == Enemy.Pedrito)
        {
            Instantiate(pedrito, transform.position, transform.rotation);
        }
        if (enemy == Enemy.Papelito)
        {
            Instantiate(papelito, transform.position, transform.rotation);
        }
        if (enemy == Enemy.Tesourito)
        {
            Instantiate(tesourito, transform.position, transform.rotation);
        }
    }

    enum Enemy { Pedrito, Papelito, Tesourito}
}
