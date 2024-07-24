using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip damageSound;

    public GameObject damageEffectPrefab;
    public GameObject enemyExplosionPrefab;
    public GameObject bulletPrefab;
    public float timeBulletSpawn;
    public Transform[] shootPosArray;
    public GameObject flash;
    public float speed;

    public HealthBar healthBar;

    public float Health = 10f;
    float barSize = 1f;
    float damage = 0f;

    public GameObject coinPrefab;


    void Start()
    {
        flash.SetActive(false);
        StartCoroutine(Shoot());
        damage = barSize / Health;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            audioSource.PlayOneShot(damageSound);
            DamageHealthbar();
            Destroy(collision.gameObject);
            GameObject damageEffect = Instantiate(damageEffectPrefab, collision.transform.position, Quaternion.identity);
            Destroy(damageEffect, 0.07f);
            if (Health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f);
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
           
        }
    }

    void DamageHealthbar()
    {
        if (Health > 0)
        {
            Health -= 1;
            barSize = barSize - damage;
            healthBar.SetSize(barSize);
        }
    }


    void Fire()
    {
        for (int i = 0; i < shootPosArray.Length; i++)
        {
            Transform shootPos = shootPosArray[i];
            Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBulletSpawn);
        Fire();
        audioSource.PlayOneShot(shootSound, 0.5f); 
        flash.SetActive(true);
        yield return new WaitForSeconds(0.04f);
        flash.SetActive(false);
        StartCoroutine(Shoot());
    }
}