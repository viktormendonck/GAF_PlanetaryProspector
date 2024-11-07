using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 0;
    public float flickerTime = 0.5f;
    private Image image;
    void Start()
    {
        if (TryGetComponent(out Image i))
        {
            image = i;
        } else
        {
            Debug.LogError("Object doesn't contain the Indicator image");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time > flickerTime)
        {
            time = 0;
            image.enabled = !image.enabled;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
