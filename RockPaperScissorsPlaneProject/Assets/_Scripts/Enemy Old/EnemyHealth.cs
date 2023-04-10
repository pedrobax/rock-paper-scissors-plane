using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public string unitName;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float scoreValue;
    [SerializeField] float damageFlashDuration;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] public AudioSource soundSource;
    [SerializeField] public AudioClip hitClip;
    [SerializeField] AudioClip deathClip;
    bool canTakeDamage = false;
    Bullet _bullet;
    public bool isColliding;
    Color originalColor;
    bool destroyedByPlayer;

    private void Start()
    {
        originalColor = meshRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        /* if (isColliding) return;
        isColliding = true; deletar isso depois se a falta não fuder nada*/ 

        if (other.tag == "Player Bullet")
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage + " damage!");
            TakeDamage(_bullet.damage);
        }
        if (other.tag == "Player")
        {
            Debug.Log("Enemy Kamikaze'd!");
            TakeDamage(10);
        }
        if (other.CompareTag("Enemy Death Zone"))
        {
            Destroy(gameObject);
            Debug.Log(unitName + " destroyed by DZ");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BulletPlayerScissors") && canTakeDamage)
        {
            _bullet = other.GetComponent<Bullet>();
            Debug.Log(unitName + " has taken " + _bullet.damage + " damage!");
            TakeDamage(_bullet.damage);
            StartCoroutine(GetDamagedCooldown());
        }
    }

    void OnDestroy()
    {
        if (destroyedByPlayer)
        {
            GameManager.UpdateScore(scoreValue);
        }      
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        soundSource.Play();
        StartCoroutine(DamageFlash());
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void ChangeSound(AudioClip sound)
    {
        soundSource.clip = sound;
    }

    IEnumerator DamageFlash()
    {
        meshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(damageFlashDuration);
        meshRenderer.material.color = originalColor;
    }

    IEnumerator GetDamagedCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    IEnumerator Die()
    {
        destroyedByPlayer = true;
        this.GetComponent<Collider>().enabled = false;
        rb.useGravity = true;

        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezePositionY;

        ChangeSound(deathClip);
        soundSource.Play();
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        LowerTransformSize(0.1f);
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
        Debug.Log(unitName + " has been destroyed!");
    }

    void LowerTransformSize(float amount)
    {
        rb.transform.localScale = new Vector3(rb.transform.localScale.x - amount,
        rb.transform.localScale.y - amount, rb.transform.localScale.z - amount);
    }
}
