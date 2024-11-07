using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenButtonControl : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
