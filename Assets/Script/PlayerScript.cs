using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinCollection;

    public GameController gameController;
 
    public int Health = 20;
    public int maxHealth = 20;
    private float barFillAmount = 1f;
    
    
    public PlayerHealthBar playerHealthBar;

    public GameObject explosion;
    public float speed = 10.0f;
    public float Padding = 0.8f;
    private float minX, maxX;
    private float minY, maxY;

    public GameObject prefabDamageEffect;
    public CoinCount coinCountScript;
    void Start()
    {
        FindBoundaries();
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            Health = PlayerStorage.instance.playerHealth;
            barFillAmount = Health * 1.0f/ maxHealth;
            playerHealthBar.SetAmount(barFillAmount);
        }
        


    }

    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + Padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - Padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Padding;

    }
    
    void Update()
    {
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);

        transform.position = new Vector2(newXpos, newYpos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            audioSource.PlayOneShot(damageSound, 0.5f );
            GameObject DamageEffect = Instantiate(prefabDamageEffect, collision.transform.position, Quaternion.identity);
            Destroy(DamageEffect, 0.07f);
            damagePlayerHealthBar();
            Destroy(collision.gameObject);
            if (Health <= 0)
            {
                gameController.GameOVer();
                Destroy(gameObject);
                GameObject playerExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, 0.5f);
                Destroy(playerExplosion, 2f);
                PlayerPrefs.DeleteAll();
            }
           
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(damageSound, 0.5f);
            GameObject DamageEffect = Instantiate(prefabDamageEffect, collision.transform.position, Quaternion.identity);
            Destroy(DamageEffect, 0.07f);
            damagePlayerHealthBar();
            Destroy(collision.gameObject);
            if (Health <= 0)
            {
                gameController.GameOVer();
                Destroy(gameObject);
                GameObject playerExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, 0.5f);
                Destroy(playerExplosion, 2f);
                PlayerPrefs.DeleteAll();
            }
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            AudioSource.PlayClipAtPoint(coinCollection, transform.position, 0.5f); 
            Destroy(collision.gameObject);
            coinCountScript.AddCount();
            
        }
    }

    void damagePlayerHealthBar()
    {
        if(Health > 0)
        {
            Health -= 1;
            PlayerStorage.instance.playerHealth = Health;
            PlayerStorage.instance.SaveHealth();
            barFillAmount = Health * 1.0f / maxHealth;
            playerHealthBar.SetAmount(barFillAmount);
        }
    }

}
