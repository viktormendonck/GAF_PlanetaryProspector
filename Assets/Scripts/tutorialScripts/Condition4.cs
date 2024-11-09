using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition4 : TutorialCondition
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
        if (transportHandler.Transporters.Count > 0)
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
