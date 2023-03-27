using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float lives = 3;
    public Vector3 respawnPosition;
    public float respawnTime = 1;
    public float respawnInvulnerabilityTime = 3;
    public bool canTakeDamage = true;

    [SerializeField] public MeshRenderer currentMeshRenderer;
    [SerializeField] public MeshRenderer rockMeshRenderer;
    [SerializeField] public MeshRenderer paperMeshRenderer;
    [SerializeField] public MeshRenderer scissorsMeshRenderer;
    Color originalColor;

    [SerializeField] public PlayerType currentType = PlayerType.Paper;
    public bool canSwitchType = true;
    public bool canBecomeRock = false;
    public bool canBecomeScissors = false;
    [SerializeField] GameObject playerRockObject;
    [SerializeField] GameObject playerPaperObject;
    [SerializeField] GameObject playerScissorsObject;
    [SerializeField] BoxCollider rockCollider;
    [SerializeField] BoxCollider paperCollider;
    [SerializeField] BoxCollider scissorsCollider;

    private void Start()
    {
        if (currentType == PlayerType.Paper) currentMeshRenderer = paperMeshRenderer;
        if (currentType == PlayerType.Scissors) currentMeshRenderer = scissorsMeshRenderer;
        if (currentType == PlayerType.Rock) currentMeshRenderer = rockMeshRenderer;
        originalColor = currentMeshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyRock") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyPaper") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyScissors") && canTakeDamage) IgnoreDamage();

        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyRock") && canTakeDamage) IgnoreDamage();
        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyPaper") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyScissors") && canTakeDamage) StartCoroutine(DieOrRespawn());

        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyRock") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyPaper") && canTakeDamage) IgnoreDamage();
        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyScissors") && canTakeDamage) StartCoroutine(DieOrRespawn());



        if (currentType == PlayerType.Rock && other.CompareTag("EnemyRock") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Rock && other.CompareTag("EnemyPaper") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Rock && other.CompareTag("EnemyScissors") && canTakeDamage) IgnoreDamage();

        if (currentType == PlayerType.Paper && other.CompareTag("EnemyRock") && canTakeDamage) IgnoreDamage();
        if (currentType == PlayerType.Paper && other.CompareTag("EnemyPaper") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Paper && other.CompareTag("EnemyScissors") && canTakeDamage) StartCoroutine(DieOrRespawn());

        if (currentType == PlayerType.Scissors && other.CompareTag("EnemyRock") && canTakeDamage) StartCoroutine(DieOrRespawn());
        if (currentType == PlayerType.Scissors && other.CompareTag("EnemyPaper") && canTakeDamage) IgnoreDamage();
        if (currentType == PlayerType.Scissors && other.CompareTag("EnemyScissors") && canTakeDamage) StartCoroutine(DieOrRespawn());

        if (other.CompareTag("Extra Life")) GetALife();
        if (other.CompareTag("RockPowerUp"))
        {
            canBecomeRock = true;
            SwitchTypeToRock();
        }
        if (other.CompareTag("ScissorsPowerUp"))
        {
            canBecomeScissors = true;
            SwitchTypeToScissors();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Rock && canSwitchType) SwitchTypeToPaper();
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Rock && canSwitchType && canBecomeScissors) SwitchTypeToScissors();
        else if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Rock && canSwitchType && !canBecomeScissors) SwitchTypeToPaper();

        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Paper && canSwitchType && canBecomeScissors) SwitchTypeToScissors();
        else if (Input.GetButtonDown("NextType") && currentType == PlayerType.Paper && canSwitchType && !canBecomeScissors && canBecomeRock) SwitchTypeToRock();
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Paper && canSwitchType && canBecomeRock) SwitchTypeToRock();
        else if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Paper && canSwitchType && !canBecomeRock && canBecomeScissors) SwitchTypeToScissors();

        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Scissors && canSwitchType && canBecomeRock) SwitchTypeToRock();
        else if (Input.GetButtonDown("NextType") && currentType == PlayerType.Scissors && canSwitchType && !canBecomeRock) SwitchTypeToPaper();
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Scissors && canSwitchType) SwitchTypeToPaper();
    }
    void SwitchTypeToRock()
    {
        currentType = PlayerType.Rock;
        playerRockObject.SetActive(true);
        playerPaperObject.SetActive(false);
        playerScissorsObject.SetActive(false);
        rockCollider.enabled = true;
        paperCollider.enabled = false;
        scissorsCollider.enabled = false;
        currentMeshRenderer = rockMeshRenderer;
        originalColor = rockMeshRenderer.material.color;
        StartCoroutine(CountTransformCooldown());
        Debug.Log("You are now a Rock!");
    }

    void SwitchTypeToPaper()
    { 
        currentType = PlayerType.Paper;
        playerPaperObject.SetActive(true);
        playerRockObject.SetActive(false);
        playerScissorsObject.SetActive(false);
        rockCollider.enabled = false;
        paperCollider.enabled = true;
        scissorsCollider.enabled = false;
        currentMeshRenderer = paperMeshRenderer;
        originalColor = paperMeshRenderer.material.color;
        StartCoroutine(CountTransformCooldown());
        Debug.Log("You are now a Paper!");
    }

    void SwitchTypeToScissors()
    {
        currentType = PlayerType.Scissors;
        playerScissorsObject.SetActive(true);
        playerRockObject.SetActive(false);
        playerPaperObject.SetActive(false);
        rockCollider.enabled = false;
        paperCollider.enabled = false;
        scissorsCollider.enabled = true;
        currentMeshRenderer = scissorsMeshRenderer;
        originalColor = scissorsMeshRenderer.material.color;
        StartCoroutine(CountTransformCooldown());
        Debug.Log("You are now Scissors!");
    }

    void GetALife()
    {
        lives++;
        Debug.Log("You got an extra life! You have " + lives + " remaining lives!");
    }

    void GetDamaged()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log(lives + " lives remaining!");
        }
        else
        {
            Debug.Log("Game over!");
            Destroy(gameObject);
        }
    }
    void GetDoubleDamaged()
    {
        if (lives > 1)
        {
            lives--;
            lives--;
            Debug.Log(lives + " lives remaining!");
        }
        else
        {
            Debug.Log("Game over!");
            Destroy(gameObject);
        }
    }

    public IEnumerator DieOrRespawn()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Player lost a life! Remaining lives: " + lives);
            currentMeshRenderer.enabled = false;
            canTakeDamage = false;
            yield return new WaitForSeconds(respawnTime);
            transform.position = respawnPosition;
            currentMeshRenderer.enabled = true;
            StartCoroutine(DamageFlash(respawnInvulnerabilityTime / 10));
            yield return new WaitForSeconds(respawnInvulnerabilityTime / 5);
            StartCoroutine(DamageFlash(respawnInvulnerabilityTime / 10));
            yield return new WaitForSeconds(respawnInvulnerabilityTime / 5);
            StartCoroutine(DamageFlash(respawnInvulnerabilityTime / 10));
            yield return new WaitForSeconds(respawnInvulnerabilityTime / 5);
            StartCoroutine(DamageFlash(respawnInvulnerabilityTime / 10));
            yield return new WaitForSeconds(respawnInvulnerabilityTime / 5);
            StartCoroutine(DamageFlash(respawnInvulnerabilityTime / 10));
            yield return new WaitForSeconds(respawnInvulnerabilityTime / 5);
            canTakeDamage = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageFlash(float duration)
    {
        currentMeshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(duration);
        currentMeshRenderer.material.color = originalColor;
    }

    void IgnoreDamage()
    {
        Debug.Log("Damage ignored by type!");
    }

    public IEnumerator CountTransformCooldown()
    {
        canSwitchType = false;
        yield return new WaitForSeconds(.1f);
        canSwitchType = true;
    }

    public enum PlayerType
    {
        Rock,
        Paper,
        Scissors
    }   
}
