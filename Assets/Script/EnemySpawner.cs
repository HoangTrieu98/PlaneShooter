using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public float respawnTime = 3.0f;
    public int enemySpawnCount = 10;
    public GameController gameController;
    public bool lastEnemySpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (lastEnemySpawn && FindAnyObjectByType<EnemyScript>()== null)
        {
            StartCoroutine(gameController.GameComplete());
        }
    }

    IEnumerator EnemySpawner()
    {
        for (int i = 0; i < enemySpawnCount; i++ )
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }

        lastEnemySpawn = true;
    }  

    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length);
        int randomSpawnPosition = Random.Range(-5, 5);
        Instantiate(enemy[randomValue], new Vector2(randomSpawnPosition, transform.position.y) , Quaternion.identity);
    }
}
