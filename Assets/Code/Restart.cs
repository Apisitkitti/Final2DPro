using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Restart : MonoBehaviour
{
    
    public void RestartGame()
    {
        ScoreManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
