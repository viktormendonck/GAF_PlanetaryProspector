using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupButton : MonoBehaviour
{
    [SerializeField] private float TimeScale = 5;
    // Start is called before the first frame update
    public void ButtonDown()
    {
        Time.timeScale = TimeScale;
    }
    public void ButtonUp() 
    {

        Time.timeScale = 1;

    }
    
}
