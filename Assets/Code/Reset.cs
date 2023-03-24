using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private int currentLevel;
    public int Send = 0;
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {

    }
    public void Check()
    {
        
            SceneManager.LoadScene(currentLevel);
        
            
    }
    
}
