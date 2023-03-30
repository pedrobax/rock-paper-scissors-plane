using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntagonistHealth : MonoBehaviour
{
    [SerializeField] public string unitName;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float scoreValue;
    [SerializeField] float damageFlashDuration;
    [SerializeField] EnemyType enemyType;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] public AudioSource soundSource;
    [SerializeField] public AudioClip hitClip;
    [SerializeField] public GameObject shieldVFX;
    [SerializeField] AudioClip deathClip;
    [SerializeField] public GameObject deathVFX;
    [SerializeField] public float deathShakeTime = 0.2f;
    [SerializeField] public float deathShakeIntensity = 0.5f;
    Bullet _bullet;
    public bool isColliding;
    Color originalColor;
    bool destroyedByPlayer;
    bool canTakeScissorsDamage = true;

    private void Start()
    {
        originalColor = meshRenderer.material.color;
    }

    private void Update()
    {
        ResetVelocity();
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemyType == EnemyType.Rock && other.CompareTag("BulletPlayerRock"))
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage + " damage!");
            TakeDamage(_bullet.damage);
        }
        if (enemyType == EnemyType.Rock && other.CompareTag("BulletPlayerPaper"))
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage * 2 + " damage!");
            TakeDamage(_bullet.damage * 2);
        }


        if (enemyType == EnemyType.Paper && other.CompareTag("BulletPlayerRock"))
        {
            IgnoreDamage();
        }
        if (enemyType == EnemyType.Paper && other.CompareTag("BulletPlayerPaper"))
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage + " damage!");
            TakeDamage(_bullet.damage);
        }


        if (enemyType == EnemyType.Scissors && other.CompareTag("BulletPlayerRock"))
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage * 2 + " damage!");
            TakeDamage(_bullet.damage * 2);
        }
        if (enemyType == EnemyType.Scissors && other.CompareTag("BulletPlayerPaper"))
        {
            IgnoreDamage();
        }
        if (other.CompareTag("Enemy Death Zone"))
        {
            Destroy(gameObject);
            Debug.Log(unitName + " destroyed by DZ");
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Rock && canTakeScissorsDamage)
            {
                IgnoreDamage();
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Paper && canTakeScissorsDamage)
            {
                Debug.Log(unitName + " has taken " + 2 + " damage!");
                TakeDamage(2);
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Scissors && canTakeScissorsDamage)
            {
                Debug.Log(unitName + " has taken " + 1 + " damage!");
                TakeDamage(1);
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
        }   
    }

    void OnDestroy()
    {
        if (destroyedByPlayer) GameManager.UpdateScore(scoreValue);
        Destroy(gameObject.transform.parent.gameObject);
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        soundSource.Play();
        StartCoroutine(DamageFlash());
        if (health <= 0)
        {
            Die();          
        }
    }

    void IgnoreDamage()
    {
        Debug.Log("Enemy ignored damage by type!");
        GameObject shieldVfx = Instantiate(shieldVFX, transform.position, transform.rotation);
        shieldVfx.transform.parent = this.transform;
    }

    public void ChangeSound(AudioClip sound)
    {
        soundSource.clip = sound;
    }

    void ResetVelocity()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator DamageFlash()
    {
        meshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(damageFlashDuration);
        meshRenderer.material.color = originalColor;
    }

    IEnumerator GetDamagedByScissorsCooldown()
    {
        canTakeScissorsDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeScissorsDamage = true;
    }

    public void Die()
    {
        destroyedByPlayer = true;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<ActionList>().enabled = false;
        rb.useGravity = true;

        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezePositionY;

        DisableVisuals();

        ChangeSound(deathClip);
        soundSource.Play();
        CinemachineShake.Instance.ShakeCamera(deathShakeIntensity, deathShakeTime);
        
     
        Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(gameObject, 1);
        Debug.Log(unitName + " has been destroyed!");
    }

    void DisableVisuals()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    enum EnemyType
    {
        Rock,
        Paper,
        Scissors
    }
}
