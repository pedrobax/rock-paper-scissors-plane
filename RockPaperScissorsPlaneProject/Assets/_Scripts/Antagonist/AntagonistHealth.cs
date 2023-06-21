using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntagonistHealth : MonoBehaviour
{
    //despite being called AntagonistHealth, this class is used to store all the
    //antagonist's properties besides actions and bullets

    [SerializeField] public string unitName; //antagonist's name for debugging
    [SerializeField] float maxHealth;
    [SerializeField] public float health;
    [SerializeField] float scoreValue; //how much is added to the score when the antagonist is destroyed BY THE PLAYER
    [SerializeField] float damageFlashDuration = 0.025f; //how long the antagonist flashes when hit
    [SerializeField] public EnemyType enemyType; //the antagonist's type: rock, paper or scissors
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] public AudioSource soundSource;
    [SerializeField] public AudioClip hitClip; //sound played when the antagonist is hit by a player bullet
    [SerializeField] public GameObject shieldVFX; //vfx played when the antagonist is hit by an innefective player bullet
    [SerializeField] public GameObject deathVFX; //vfx played when the antagonist is destroyed
    [SerializeField] public float deathShakeTime = 0.2f; //duration the screen shakes when the antagonist is destroyed
    [SerializeField] public float deathShakeIntensity = 0.5f; //intensity of the screen shake when the antagonist is destroyed
    [SerializeField] public GameObject loot; //what the enemy drops when he dies, if anything
    public bool hasLoot = false; //is automatically set to true if the enemy has loot (see above)
    Bullet _bullet; //is used to read bullets that hit the antagonist
    public bool isColliding; //is the antagonist colliding with something?
    bool destroyedByPlayer; //was the antagonist destroyed by the player?
    bool canTakeScissorsDamage = true; //can the antagonist take damage from scissors? TODO rework and remove this
    public GameObject muzzleFlashVFX;

    Color[] originalColors; //stores the antagonist's original colors for the damage flash,
                            //should be removed and replaced with a shader for non skinned mesh enemies

    private void Start()
    {
        //stores the antagonist's original colors for the damage flash
        if ( meshRenderer != null)
        {
            originalColors = new Color[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                originalColors[i] = meshRenderer.materials[i].color;
            }
        }       

        //registers if the antagonist has loot
        if (loot != null)
        {
            hasLoot = true;
        }
    }

    private void Update()
    {
        //prevents the antagonist from spiralling out of control because of physics
        ResetVelocity();
    }

    void OnTriggerEnter(Collider other)
    {
        //checks if the antagonist has been hit by a player bullet and applies damage accordingly to the antagonist and bullet type
        if (enemyType == EnemyType.Rock && other.CompareTag("BulletPlayerRock"))
        {
            _bullet = other.GetComponent<Bullet>();
            TakeDamage(_bullet.damage);
        }
        if (enemyType == EnemyType.Rock && other.CompareTag("BulletPlayerPaper"))
        {
            _bullet = other.GetComponent<Bullet>();
            TakeDamage(_bullet.damage * 2);
        }


        if (enemyType == EnemyType.Paper && other.CompareTag("BulletPlayerRock"))
        {
            IgnoreDamage();
        }
        if (enemyType == EnemyType.Paper && other.CompareTag("BulletPlayerPaper"))
        {
            _bullet = other.GetComponent<Bullet>();
            TakeDamage(_bullet.damage);
        }


        if (enemyType == EnemyType.Scissors && other.CompareTag("BulletPlayerRock"))
        {
            _bullet = other.GetComponent<Bullet>();
            TakeDamage(_bullet.damage * 2);
        }
        if (enemyType == EnemyType.Scissors && other.CompareTag("BulletPlayerPaper"))
        {
            IgnoreDamage();
        }

        //destroys the antagonist if it collides with the death zone
        if (other.CompareTag("Enemy Death Zone"))
        {
            Destroy(gameObject);
        }

        //takes damage from the player if the antagonist collides with the player and the player is scissors
        if (other.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Rock && canTakeScissorsDamage)
            {
                IgnoreDamage();
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Paper && canTakeScissorsDamage && other.GetComponent<PlayerHealth>().isRespawning == false)
            {
                Debug.Log(unitName + " has taken " + 50000 + " damage! PAPER HIT BY SCISSORS: INSTANT DEATH");
                TakeDamage(50000);
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
            if (_playerHealth.currentType == PlayerHealth.PlayerType.Scissors && enemyType == EnemyType.Scissors && canTakeScissorsDamage)
            {
                IgnoreDamage();
                StartCoroutine(GetDamagedByScissorsCooldown());
            }
        }   
    }

    void OnDestroy()
    {
        //updates score if the antagonist is destroyed by the player
        if (destroyedByPlayer)
        {
            GameManager.UpdateScore(scoreValue);
            GameManager.ShakeScreen(deathShakeIntensity, deathShakeTime, CinemachineShake.ShakeType.FADING_OUT);
        }
        //destroys the parent along with the antagonist
        //this is needed because antagonists are always children of a parent object
        //along with its action's target transforms for organizational purposes
        if (gameObject.transform.parent != null) Destroy(gameObject.transform.parent.gameObject);   
    }

    //lowers antagonist health,flashes white for feedback, and destroys it if health reaches 0
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

    //ignores damage and plays a shield vfx for feedback
    void IgnoreDamage()
    {
        GameObject shieldVfx = Instantiate(shieldVFX, transform.position, transform.rotation);
        shieldVfx.transform.parent = this.transform;
    }

    //changes sound clip to be played on sound source
    public void ChangeSound(AudioClip sound)
    {
        soundSource.clip = sound;
    }

    //resets rigidbody velocity
    void ResetVelocity()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    //antagonist flashes white for damage feedback
    //uses material colors for non skinned mesh enemies
    //uses shader for skinned mesh enemies
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
            foreach (var mat in skinnedMeshRenderer.materials)
            {
                mat.SetFloat("_isDamageFlashing", 1);
            }
            yield return new WaitForSeconds(damageFlashDuration);
            foreach (var mat in skinnedMeshRenderer.materials)
            {
                mat.SetFloat("_isDamageFlashing", 0);
            }
        }
    }

    //counts cooldown for taking damage from scissors player
    IEnumerator GetDamagedByScissorsCooldown()
    {
        canTakeScissorsDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeScissorsDamage = true;
    }

    //is only called when KILLED BY THE PLAYER
    //drops loot if has any, plays death vfx, and destroys the antagonist
    public void Die()
    {
        destroyedByPlayer = true;  
        if (hasLoot) Instantiate(loot, transform.position, transform.rotation);
        Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //used for referencing the antagonist's type for damage calculations
    public enum EnemyType
    {
        Rock,
        Paper,
        Scissors
    }
}
