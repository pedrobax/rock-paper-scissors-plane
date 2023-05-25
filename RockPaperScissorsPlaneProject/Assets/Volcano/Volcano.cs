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
    public GameObject cart;
    public bool isSpawning = false;


    void Update()
    {
        if (isSpawning && transform.position.z > 16.5f)
        {
            transform.Translate(Vector3.forward * 400 * Time.deltaTime);
            cart.transform.Translate(Vector3.forward * 400 * Time.deltaTime);
        }
        if (isSpawning && transform.position.z <= 16.5f)
        {
            isSpawning = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 16.5f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExplosionSequence());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CannonFire();
        }
    }

    void FixedUpdate()
    {
        DoCellMovement();
    }

    public GameObject firePointL, firePointML, firePointM, firePointMR, firePointR;
    void CannonFire()
    {
        if (currentPhase == CurrentPhase.PAPER)
        {
            Instantiate(paperBullet, firePointL.transform.position, firePointL.transform.rotation);
            Instantiate(paperBullet, firePointML.transform.position, firePointML.transform.rotation);
            Instantiate(paperBullet, firePointM.transform.position, firePointM.transform.rotation);
            Instantiate(paperBullet, firePointMR.transform.position, firePointMR.transform.rotation);
            Instantiate(paperBullet, firePointR.transform.position, firePointR.transform.rotation);
        }
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

    public IEnumerator ExplosionSequence()
    {
        CinemachineShake.Instance.ShakeCamera(1f, 6f, CinemachineShake.ShakeType.FADING_IN);
        yield return new WaitForSeconds(6f);
        Explode();
        CinemachineShake.Instance.ShakeCamera(15f, 1f, CinemachineShake.ShakeType.FADING_OUT);

        switch (currentPhase)
        {
            case CurrentPhase.PAPER:
                currentPhase = CurrentPhase.ROCK;
                rockVolcano.SetActive(true);
                break;
            case CurrentPhase.ROCK:
                currentPhase = CurrentPhase.SCISSORS;
                scissorsVolcano.SetActive(true);
                break;
            case CurrentPhase.SCISSORS:
                currentPhase = CurrentPhase.PAPER;
                break;
        }
    }
}
