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
    [SerializeField] float damageFlashDuration = 0.025f;
    [SerializeField] EnemyType enemyType;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] public AudioSource soundSource;
    [SerializeField] public AudioClip hitClip;
    [SerializeField] public GameObject shieldVFX;
    [SerializeField] public GameObject deathVFX;
    [SerializeField] public float deathShakeTime = 0.2f;
    [SerializeField] public float deathShakeIntensity = 0.5f;
    [SerializeField] public GameObject loot;
    public bool hasLoot = false;
    Bullet _bullet;
    public bool isColliding;
    bool destroyedByPlayer;
    bool canTakeScissorsDamage = true;

    Color[] originalColors;

    private void Start()
    {
        if ( meshRenderer != null)
        {
            originalColors = new Color[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                originalColors[i] = meshRenderer.materials[i].color;
            }
        }       

        if (loot != null)
        {
            hasLoot = true;
        }
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
        if (destroyedByPlayer)
        {
            GameManager.UpdateScore(scoreValue);
            GameManager.ShakeScreen(deathShakeIntensity, deathShakeTime);
        }
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
        if (meshRenderer != null)
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i].color = Color.white;
            }

            yield return new WaitForSeconds(damageFlashDuration);

            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i].color = originalColors[i];
            }
        }
        if (skinnedMeshRenderer != null)
        {

        }
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
        if (hasLoot) Instantiate(loot, transform.position, transform.rotation);
        Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(gameObject);
        Debug.Log(unitName + " has been destroyed!");
    }

    enum EnemyType
    {
        Rock,
        Paper,
        Scissors
    }
}
