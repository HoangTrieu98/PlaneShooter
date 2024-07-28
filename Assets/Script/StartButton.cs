using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class StartButton : MonoBehaviour
{
   
    
  

    public void StartGame()
    {
        PlayerStorage.instance.ClearData();
        SceneManager.LoadScene("Main");
    }

    public void CountinueGame()
    {
        SceneManager.LoadScene("Main");
    }
}