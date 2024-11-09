using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition5 : TutorialCondition
{
    [SerializeField]private OreContainer moneyOreContainer;
    private float cooldown = 0.5f;
    public override bool Condition()
    {
        if (moneyOreContainer.GetCurrentOreAmount() >0)
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
