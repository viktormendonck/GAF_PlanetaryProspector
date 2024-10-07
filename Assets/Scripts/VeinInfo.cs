using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VeinInfo : MonoBehaviour
{
    // Start is called before the first frame update

    private float veinAmount = 100;
    private float totalAmount = 0;
    private Material veinMaterial;
    private Color depletedVeinColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    private Color veinColor = new Color(0.258f, 0.117f, 0.1f, 1f);
    void Start()
    {
        veinMaterial = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
    }


    public void  SetVeinSize(float size)
    {
        veinAmount*= size;
        totalAmount = veinAmount;
    }
    public float GetVeinAmount()
    {
        return veinAmount;
    }

    public bool MineVein(float drillSpeed)
    {
        if (veinAmount > 0)
        {
            veinAmount -= drillSpeed;
            //todo lerp between colors
            return true;
        }
        else
        {
            return false;
        }
    }
}
