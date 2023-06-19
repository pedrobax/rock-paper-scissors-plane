using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public GameObject paperVolcano, rockVolcano, scissorsVolcano;   //store the volcanos for each phase
    public GameObject paperCells, rockCells, scissorsCells;        //store the cells for each phase
    AntagonistHealth antagonistHealth;                      //store the antagonist health script
    public GameObject summonPointPaper, summonPointRock, summonPointScissors; //store the summon points for each phase
    public GameObject summonPhase01;                               //store the summons in different phases
    public List<GameObject> summonsPhase02 = new List<GameObject>();
    public List<GameObject> summonsPhase03 = new List<GameObject>();
    public Animator paperAnimator, rockAnimator, scissorsAnimator; //store the animators for each phase
    public SkinnedMeshRenderer rockRenderer;
    public GameObject paperBullet, rockBullet;                     //store the bullets for each phase
    public enum CurrentPhase { PAPER, ROCK, SCISSORS };            
    public CurrentPhase currentPhase;                              //store the current phase
    public bool isSpawning = false;
    public Mesh paperMesh, rockMesh, scissorsMesh;
    public MeshCollider meshCollider;
    public GameObject volcanoHolder;
    public bool isDefeated = false;
    public AudioSource rockEruptionSource, tremorExplosionSource;

    void Start()
    {
        antagonistHealth = GetComponent<AntagonistHealth>();
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = paperMesh;
    }

    void Update()
    {
        if (!isSpawning) DoActionLoop();
        
        if (isSpawning && volcanoHolder.transform.position.z > 0)
        {
            volcanoHolder.transform.Translate(Vector3.back * 200 * Time.deltaTime);
        }
        if (isSpawning && volcanoHolder.transform.position.z <= 0)
        {
            isSpawning = false;
            volcanoHolder.transform.position = new Vector3(volcanoHolder.transform.position.x, volcanoHolder.transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExplosionSequence());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Erupt(0);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Erupt(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Erupt(2);
        }

        if(isDefeated)
        {
            FindObjectOfType<GameManager>().DisablePlayerTakeDamage();
        }
    }

    void FixedUpdate()
    {
        DoCellMovement();
    }

    public enum PaperState { NONE, IDLE, ATTACK, SUMMON, DEATH, EXPLODE };
    public PaperState paperState = PaperState.NONE;
    public float paperIdleDuration, paperAttackDuration, paperSummonDuration, paperDeathDuration;
    public float paperCurrentDuration;
    public float paperHealthThreshold = 2000;

    public enum RockState { NONE, IDLE, ERUPT, PAUSE, LASER, DEATH, EXPLODE };
    public RockState rockState = RockState.NONE;
    public float rockIdleDuration, rockEruptDuration, rockPauseDuration, rockLaserDuration;
    public float rockCurrentDuration;
    public float rockHealthThreshold = 1000;
    void DoActionLoop()
    {
        if (currentPhase == CurrentPhase.PAPER)
        {
            if (paperState == PaperState.NONE)
            {
                paperState = PaperState.IDLE;
                paperCurrentDuration = Time.time;
            }
            else if (paperState == PaperState.IDLE)
            {
                paperAnimator.SetBool("isIdle", true);
                if (paperCurrentDuration + paperIdleDuration <= Time.time)
                {
                    paperState = PaperState.ATTACK;
                    paperCurrentDuration = Time.time;
                    paperAnimator.SetBool("isIdle", false);
                }
            }
            else if (paperState == PaperState.ATTACK)
            {
                paperAnimator.SetBool("isAttacking", true);
                if (antagonistHealth.health <= paperHealthThreshold && paperState != PaperState.DEATH)
                {
                    paperState = PaperState.DEATH;
                    paperCurrentDuration = Time.time;
                    paperAnimator.SetBool("isAttacking", false);
                }
                else if (paperCurrentDuration + paperAttackDuration <= Time.time)
                {
                    paperState = PaperState.SUMMON;
                    paperCurrentDuration = Time.time;
                    paperAnimator.SetBool("isAttacking", false);
                }
            }
            else if (paperState == PaperState.SUMMON)
            {
                paperAnimator.SetBool("isSummoning", true);
                if (antagonistHealth.health <= paperHealthThreshold && paperState != PaperState.DEATH)
                {
                    paperState = PaperState.DEATH;
                    paperCurrentDuration = Time.time;
                    paperAnimator.SetBool("isAttacking", false);
                }
                else if (paperCurrentDuration + paperSummonDuration <= Time.time)
                {
                    paperState = PaperState.ATTACK;
                    paperCurrentDuration = Time.time;
                    paperAnimator.SetBool("isSummoning", false);
                }
            }
            if (paperState == PaperState.DEATH)
            {
                paperAnimator.SetBool("isDead", true);
                StartCoroutine(ExplosionSequence());
                paperState = PaperState.EXPLODE;
            }
        }
        if (currentPhase == CurrentPhase.ROCK)
        {
            if (rockState == RockState.NONE)
            {
                rockState = RockState.IDLE;
                rockCurrentDuration = Time.time;
            }
            else if (rockState == RockState.IDLE)
            {
                rockAnimator.SetBool("isIdle", true);
                if (rockCurrentDuration + rockIdleDuration <= Time.time)
                {
                    rockState = RockState.ERUPT;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isIdle", false);
                }
            }
            else if (rockState == RockState.ERUPT)
            {
                rockAnimator.SetBool("isErupting", true);
                if (antagonistHealth.health <= rockHealthThreshold && rockState != RockState.DEATH)
                {
                    rockState = RockState.DEATH;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isErupting", false);
                }
                else if (rockCurrentDuration + rockEruptDuration <= Time.time)
                {
                    rockState = RockState.PAUSE;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isErupting", false);
                }
            }
            else if (rockState == RockState.PAUSE)
            {
                rockAnimator.SetBool("isPausing", true);
                if (antagonistHealth.health <= rockHealthThreshold && rockState != RockState.DEATH)
                {
                    rockState = RockState.DEATH;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isPausing", false);
                }
                else if (rockCurrentDuration + rockPauseDuration <= Time.time)
                {
                    rockState = RockState.LASER;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isPausing", false);
                }
            }
            else if (rockState == RockState.LASER)
            {
                rockAnimator.SetBool("isLaser", true);
                if (antagonistHealth.health <= rockHealthThreshold && rockState != RockState.DEATH)
                {
                    rockState = RockState.DEATH;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isLaser", false);
                }
                else if (rockCurrentDuration + rockLaserDuration <= Time.time)
                {
                    rockState = RockState.IDLE;
                    rockCurrentDuration = Time.time;
                    rockAnimator.SetBool("isLaser", false);
                }
            }
            else if (rockState == RockState.DEATH)
            {
                rockAnimator.SetBool("isDead", true);
                StartCoroutine(ExplosionSequence());
                rockState = RockState.EXPLODE;
            }
        }
        else if (currentPhase == CurrentPhase.SCISSORS && !isDefeated)
        {
            if(antagonistHealth.health <= 200)
            {
                StartCoroutine(ExplosionSequence());
                isDefeated = true;
            }
        }
    }

    public GameObject firePointL, firePointML, firePointM, firePointMR, firePointR;
    public void CannonFire()
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

    public void SummonPaper()
    {
        Instantiate(summonPhase01, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
    }

    public void Erupt(int index)
    {
        Instantiate(summonsPhase02[index], summonPointRock.transform.position, summonPointRock.transform.rotation);
        rockEruptionSource.Play();
        
    }

    public IEnumerator ExplosionSequence()
    {
        CinemachineShake.Instance.ShakeCamera(1f, 6f, CinemachineShake.ShakeType.FADING_IN);
        tremorExplosionSource.Play();
        yield return new WaitForSeconds(6f);
        Explode();
        if (isDefeated)
        {
            GameManager.FinishExam();
            Time.timeScale = 0.25f;
        }
         
        CinemachineShake.Instance.ShakeCamera(15f, 1f, CinemachineShake.ShakeType.FADING_OUT);

        switch (currentPhase)
        {
            case CurrentPhase.PAPER:
                meshCollider.sharedMesh = rockMesh;
                antagonistHealth.skinnedMeshRenderer = rockRenderer;
                antagonistHealth.enemyType = AntagonistHealth.EnemyType.Rock;
                currentPhase = CurrentPhase.ROCK;
                rockVolcano.SetActive(true);
                break;
            case CurrentPhase.ROCK:
                meshCollider.sharedMesh = scissorsMesh;
                antagonistHealth.skinnedMeshRenderer = scissorsVolcano.GetComponentInChildren<SkinnedMeshRenderer>();
                antagonistHealth.enemyType = AntagonistHealth.EnemyType.Scissors;
                currentPhase = CurrentPhase.SCISSORS;
                scissorsVolcano.SetActive(true);
                break;
            case CurrentPhase.SCISSORS:
                currentPhase = CurrentPhase.PAPER;
                break;
        }
    }
}
