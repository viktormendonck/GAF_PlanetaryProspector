using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenButtonControl : MonoBehaviour
{
    public void OnQuit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void OnRestart()
    {
        Debug.Log("restart");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
