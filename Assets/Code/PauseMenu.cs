using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PauseMenu : MonoBehaviour
{
    public GameObject[] Touch;
    public GameObject PauseObject;
    public static bool PauseCheck = false;
    void Update()
    {
        if(PauseObject.activeSelf)
        {
            Time.timeScale = 0f;
            PauseCheck = true;
        }
        else
        {
            Time.timeScale = 1f;
            PauseCheck =false;
        }
    }
    public void Pause()
    {
        PauseObject.SetActive(true);
        foreach(GameObject button in Touch)
        {
            button.SetActive(false);
        }
    }
    public void resume()
    {
        PauseObject.SetActive(false);
        foreach(GameObject button in Touch)
        {
            button.SetActive(true);
        }
    }
}
