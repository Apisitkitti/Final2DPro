using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{   
    public void OnClickstart()
    {
        ScoreManager.score =0;
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("HealthSlider");
        PlayerPrefs.SetFloat("PlayerHealth", 100.0f);
        PlayerPrefs.SetFloat("HealthSlider", 100.0f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
