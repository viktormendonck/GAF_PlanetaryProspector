using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Condition1 : TutorialCondition
{

    private float cooldown = 0.5f;

    private TransportHandler transportHandler;

    void Start()
    {
        cooldown = 0.5f;
        transportHandler = GameObject.FindGameObjectWithTag("TransportHandler").GetComponent<TransportHandler>();
    }

    public override bool Condition()
    {
        if (transportHandler.MinerNodes.Count > 0)
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
