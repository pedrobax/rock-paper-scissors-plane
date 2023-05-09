using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //destroys enemies and bullets when they enter the death zone
        if (other.CompareTag("EnemyRock") || other.CompareTag("EnemyPaper") || other.CompareTag("EnemyScissors") ||
            other.CompareTag("BulletEnemyRock") || other.CompareTag("BulletEnemyPaper") || other.CompareTag("BulletEnemyScissors"))
        {
            Destroy(other.gameObject);
            Debug.Log(other.gameObject.name + " destroyed by death zone!");
        }
    }
}
