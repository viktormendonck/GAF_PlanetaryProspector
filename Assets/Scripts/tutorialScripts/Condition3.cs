using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Condition3 : TutorialCondition
{
    private VeinInfo vein;
    private float cooldown = 0.5f;
    void Start()
    {
        vein = GameObject.FindGameObjectWithTag("Ore").GetComponent<VeinInfo>();
    }
    public override bool Condition()
    {
        if(vein.wasFound())
        {
            if (cooldown <= 0)
            {
                return true;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }

        return false;
    }
}
