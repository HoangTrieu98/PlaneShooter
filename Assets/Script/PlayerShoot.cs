using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AudioSource playerShoot;
    public Transform[] shootPosArray;

    public GameObject bulletPrefab;
    public float delayTime = 2f;
    public float cooldownTimer = 0f;

    public GameObject flash;
    public float flashTimer = 0.02f;
    public float flashCooldown = 0f;
  

    // Start is called before the first frame update
    void Start()
    {
        flash.SetActive(false);
        
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            playerShoot.Play();
            cooldownTimer = delayTime;
            flashCooldown = flashTimer;
            for (int i = 0; i < shootPosArray.Length; i++)
            {
                Transform shootPos = shootPosArray[i];
                Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                flash.SetActive(true);
            }
        }

        flashCooldown -= Time.deltaTime;
        if (flashCooldown <= 0)
        {
            flash.SetActive(false);
        }
    }
}
