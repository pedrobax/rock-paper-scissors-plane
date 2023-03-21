using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float lives = 3;
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

    private void OnTriggerEnter(Collider other)
    {
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyRock")) GetDamaged();
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyPaper")) GetDoubleDamaged();
        if (currentType == PlayerType.Rock && other.CompareTag("BulletEnemyScissors")) IgnoreDamage();

        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyRock")) IgnoreDamage();
        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyPaper")) GetDamaged();
        if (currentType == PlayerType.Paper && other.CompareTag("BulletEnemyScissors")) GetDoubleDamaged();

        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyRock")) GetDoubleDamaged();
        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyPaper")) IgnoreDamage();
        if (currentType == PlayerType.Scissors && other.CompareTag("BulletEnemyScissors")) GetDamaged();

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

    /*private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("EnemyScissors"))
        {
            GetDamaged();
        }
    }*/

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
