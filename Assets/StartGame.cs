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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
