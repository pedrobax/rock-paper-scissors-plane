using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public GameObject paperVolcano, rockVolcano, scissorsVolcano;   //store the volcanos for each phase
    public GameObject paperCells, rockCells, scissorsCells;        //store the cells for each phase
    public AntagonistHealth antagonistHealth;                      //store the antagonist health script
    public GameObject summonPhase01;                               //store the summons in different phases
    public List<GameObject> summonsPhase03 = new List<GameObject>();
    public Animator paperAnimator, rockAnimator, scissorsAnimator; //store the animators for each phase
    public GameObject paperBullet, rockBullet;                     //store the bullets for each phase
    public enum CurrentPhase { PAPER, ROCK, SCISSORS };            
    public CurrentPhase currentPhase;                              //store the current phase


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
            StartNextPhase();
        }
    }

    void FixedUpdate()
    {
        DoCellMovement();
    }


    public float gravity = 100;
    void DoCellMovement()
    {
        if (paperCells.activeSelf == true)
        {
            foreach (Rigidbody rb in paperCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForce(Vector3.down * gravity);
                if (rb.transform.position.y < -10)
                {
                    rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z - 6 * Time.fixedDeltaTime));
                }
            }
        }
        if (rockCells.activeSelf == true)
        {
            foreach (Rigidbody rb in rockCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForce(Vector3.down * gravity);
                if (rb.transform.position.y < -10)
                {
                    rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z - 6 * Time.fixedDeltaTime));
                }
            }
        }
        if (scissorsCells.activeSelf == true)
        {
            foreach (Rigidbody rb in scissorsCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForce(Vector3.down * gravity);
                if (rb.transform.position.y < -10)
                {
                    rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z - 6 * Time.fixedDeltaTime));
                }
            }
        }
    }


    public float explosionForce = 1;
    public Vector3 explosionPosition;
    private float explosionRadius = 0;
    public float upwardsModifier = 0.0f;
    void Explode()
    {
        if (currentPhase == CurrentPhase.PAPER)
        {
            paperVolcano.SetActive(false);
            paperCells.SetActive(true);
            foreach(Rigidbody rb in paperCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
            }
        }
        if (currentPhase == CurrentPhase.ROCK)
        {
            rockVolcano.SetActive(false);
            rockCells.SetActive(true);
            foreach (Rigidbody rb in rockCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
            }
        }
        if (currentPhase == CurrentPhase.SCISSORS)
        {
            scissorsVolcano.SetActive(false);
            scissorsCells.SetActive(true);
            foreach (Rigidbody rb in scissorsCells.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
            }
        }
    }


    void StartNextPhase()
    {
        if (currentPhase == CurrentPhase.PAPER)
        {
            rockVolcano.SetActive(true);
            currentPhase = CurrentPhase.ROCK;
        }
        else if (currentPhase == CurrentPhase.ROCK)
        {
            scissorsVolcano.SetActive(true);
            currentPhase = CurrentPhase.SCISSORS;
        }
    }
}
