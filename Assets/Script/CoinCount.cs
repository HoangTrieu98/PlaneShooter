using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;
    int count = 0;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = count.ToString();
        coinText.text = "  Coin : " + count.ToString();
    }

    public void AddCount()
    {
        count++;
    }
}