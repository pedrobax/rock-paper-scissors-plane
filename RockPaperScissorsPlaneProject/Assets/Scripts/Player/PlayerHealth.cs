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
    public float typeChangeCooldown = 0.1f;
    [SerializeField] public AudioSource soundSource;

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
    [SerializeField] GameObject typeChangeVFX;
    [SerializeField] GameObject rockDeathVFX;
    [SerializeField] GameObject paperDeathVFX;
    [SerializeField] GameObject scissorsDeathVFX;
    [SerializeField] GameObject rockShieldVFX;
    [SerializeField] GameObject paperShieldVFX;
    [SerializeField] GameObject scissorsShieldVFX;

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
        if (currentType == PlayerType.Scissors && other.CompareTag("EnemyScissors") && canTakeDamage) StartCoroutine(DieOrRespawn());

        if (other.CompareTag("Extra Life")) GetALife();
        if (other.CompareTag("RockPowerUp"))
        {
            canBecomeRock = true;
            StartCoroutine(SwitchTypeToRock());
        }
        if (other.CompareTag("ScissorsPowerUp"))
        {
            canBecomeScissors = true;
            StartCoroutine(SwitchTypeToScissors());
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Rock && canSwitchType) StartCoroutine(SwitchTypeToPaper());
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Rock && canSwitchType && canBecomeScissors) StartCoroutine(SwitchTypeToScissors());
        else if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Rock && canSwitchType && !canBecomeScissors) StartCoroutine(SwitchTypeToPaper());

        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Paper && canSwitchType && canBecomeScissors) StartCoroutine(SwitchTypeToScissors());
        else if (Input.GetButtonDown("NextType") && currentType == PlayerType.Paper && canSwitchType && !canBecomeScissors && canBecomeRock) StartCoroutine(SwitchTypeToRock());
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Paper && canSwitchType && canBecomeRock) StartCoroutine(SwitchTypeToRock());
        else if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Paper && canSwitchType && !canBecomeRock && canBecomeScissors) StartCoroutine(SwitchTypeToScissors());

        if (Input.GetButtonDown("NextType") && currentType == PlayerType.Scissors && canSwitchType && canBecomeRock) StartCoroutine(SwitchTypeToRock());
        else if (Input.GetButtonDown("NextType") && currentType == PlayerType.Scissors && canSwitchType && !canBecomeRock) StartCoroutine(SwitchTypeToPaper());
        if (Input.GetButtonDown("PreviousType") && currentType == PlayerType.Scissors && canSwitchType) StartCoroutine(SwitchTypeToPaper());
    }
    IEnumerator SwitchTypeToRock()
    {
        GameObject changeVfx = Instantiate(typeChangeVFX, transform.position, transform.rotation);
        changeVfx.transform.parent = this.transform;
        currentMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.15f);
        currentMeshRenderer.enabled = true;
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

    IEnumerator SwitchTypeToPaper()
    {
        GameObject changeVfx = Instantiate(typeChangeVFX, transform.position, transform.rotation);
        changeVfx.transform.parent = this.transform;
        currentMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.15f);
        currentMeshRenderer.enabled = true;
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

    IEnumerator SwitchTypeToScissors()
    {
        GameObject changeVfx = Instantiate(typeChangeVFX, transform.position, transform.rotation);
        changeVfx.transform.parent = this.transform;
        currentMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.15f);
        currentMeshRenderer.enabled = true;
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

    public IEnumerator DieOrRespawn()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Player lost a life! Remaining lives: " + lives);

            soundSource.Play();
            CinemachineShake.Instance.ShakeCamera(5f, 0.5f);

            if (currentType == PlayerType.Rock) Instantiate(rockDeathVFX, transform.position, transform.rotation);
            if (currentType == PlayerType.Paper) Instantiate(paperDeathVFX, transform.position, transform.rotation);
            if (currentType == PlayerType.Scissors) Instantiate(scissorsDeathVFX, transform.position, transform.rotation);

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
            Destroy(gameObject, 2);
            DisableVisuals();
            soundSource.Play();
            canTakeDamage = false;
            if (currentType == PlayerType.Rock) Instantiate(rockDeathVFX, transform.position, transform.rotation);
            if (currentType == PlayerType.Paper) Instantiate(paperDeathVFX, transform.position, transform.rotation);
            if (currentType == PlayerType.Scissors) Instantiate(scissorsDeathVFX, transform.position, transform.rotation);
        }
    }

    void DisableVisuals()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
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
        if(currentType == PlayerType.Rock)
        {
            GameObject shieldVfx = Instantiate(rockShieldVFX, transform.position, transform.rotation);
            shieldVfx.transform.parent = this.transform;
        }
        if (currentType == PlayerType.Paper)
        {
            GameObject shieldVfx = Instantiate(paperShieldVFX, transform.position, transform.rotation);
            shieldVfx.transform.parent = this.transform;
        }
        if (currentType == PlayerType.Scissors)
        {
            GameObject shieldVfx = Instantiate(scissorsShieldVFX, transform.position, transform.rotation);
            shieldVfx.transform.parent = this.transform;
        }
    }

    public IEnumerator CountTransformCooldown()
    {
        canSwitchType = false;
        yield return new WaitForSeconds(typeChangeCooldown);
        canSwitchType = true;
    }

    public enum PlayerType
    {
        Rock,
        Paper,
        Scissors
    }   
}
