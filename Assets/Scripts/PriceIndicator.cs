using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceIndicator : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isPreviewActive = false;
    public bool canAfford = true;
    public bool fixedHeight = false;
    void Update()
    {
        //go to mouse pos
        if (!isPreviewActive)
        {
            transform.position = new Vector3(-1000, -1000, 0);

        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!fixedHeight)
            {
                transform.position = new Vector3(mousePos.x+2.5f, mousePos.y+1, -1) ;
            }
            else
            {
                transform.position = new Vector3(mousePos.x+2.5f, 3, -1) ;
            }
            if (canAfford)
            {
                GetComponent<TextMeshPro>().color = Color.white;
            }
            else
            {
                GetComponent<TextMeshPro>().color = Color.red;
            }
        }
    }

    public void Activate(int price)
    {
        isPreviewActive = true;
        GetComponent<TextMeshPro>().text = "$"+price + "k";
    }

    public void setPrice(int price)
    {
        GetComponent<TextMeshPro>().text = "$" + price + "k";
    }
    public void Deactivate()
    {
        isPreviewActive = false;
    }
}
