using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition6 : TutorialCondition
{
    private float cooldown = 0.5f;

    private TransportHandler transportHandler;
    private OreContainer siloContainer;
    private bool gottenContainer;

    void Start()
    {
        cooldown = 0.5f;
        transportHandler = GameObject.FindGameObjectWithTag("TransportHandler").GetComponent<TransportHandler>();
    }

    public override bool Condition()
    {
        if (transportHandler.SiloNodes.Count > 0 && !gottenContainer)
        {
            gottenContainer = true;
            siloContainer = transportHandler.SiloNodes[0].GetComponent<TransporterNode>().GetOreContainer();
        }

        if(gottenContainer)
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
