using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetectorController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject metalDetector;
    private float metalDetectorRange = 1.5f;
    private float metalDetectorSpeed = 1f;
    void Start()
    {
        metalDetector = transform.GetChild(0).gameObject;
        metalDetector.transform.localScale = new Vector3(metalDetectorRange, metalDetectorRange, 1);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
