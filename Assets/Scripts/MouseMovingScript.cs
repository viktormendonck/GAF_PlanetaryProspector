using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

    }
}
