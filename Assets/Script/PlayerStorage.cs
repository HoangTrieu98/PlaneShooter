using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    public static PlayerStorage instance;

    public int playerHealth;
    public int playerCoins;
    public int waveCount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        playerHealth = PlayerPrefs.GetInt("PlayerHealth");
        playerCoins = PlayerPrefs.GetInt("PlayerCoins");
        waveCount = PlayerPrefs.GetInt("WaveCount");
    }

    public void SaveCoins()
    {
        
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);        
    }

    public void SaveHealth()
    {
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);
    }

    public void SaveWave()
    {
        PlayerPrefs.SetInt("WaveCount", waveCount);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        waveCount = 0;
        playerCoins = 0;

    }
}
