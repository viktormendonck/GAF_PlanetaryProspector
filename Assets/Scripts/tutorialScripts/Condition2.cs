using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition2 : TutorialCondition
{
    private float cooldown = 0.5f;
    private bool foundDetector = false;
    private DetectorTutorial detector;
    public override bool Condition()
    {
        if (!foundDetector && GameObject.FindGameObjectsWithTag("Detector").Length > 0)
        {
            detector = GameObject.FindGameObjectWithTag("Detector").GetComponent<DetectorTutorial>();
            if (detector == null)
            {
                Debug.LogError("Couldnt find the detector " + GameObject.FindGameObjectWithTag("Detector").name);
            }
            foundDetector = true;
        }
        if (foundDetector)
        {
            if (detector.FoundOre())
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
        }
        return false;
    }
}

