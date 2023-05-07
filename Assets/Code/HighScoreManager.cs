using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static int highScore = 0;
    public string highScoreKey = "HighScore";
    void Start()
{
    highScore = PlayerPrefs.GetInt(highScoreKey, 0);
}

}

