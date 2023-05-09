using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Restart : MonoBehaviour
{
    
    public void RestartGame()
    {
        ScoreManager.score = 0;
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("HealthSlider");
        PlayerPrefs.SetFloat("PlayerHealth", 100.0f);
        PlayerPrefs.SetFloat("HealthSlider", 100.0f);
        SceneManager.LoadScene("Wave");
        
    }
}
