using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition0 : TutorialCondition
{
    bool clicked = false;
    public override bool Condition()
    {
        if (clicked)
        {
            clicked = false;
            return true;
        }
        return false;
    }

    public void SetClickedTrue()
    {
        clicked = true;
    }
}