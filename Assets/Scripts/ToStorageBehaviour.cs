using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToStorageBehaviour : StateMachineBehaviour
{
    private TransportHandler handler;
    [SerializeField] private float speed = 5;
    private GameObject goal;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = animator.GetFloat("Speed");
        TransporterNode node = animator.GetComponent<TransporterNode>();
        animator.GetComponent<TransporterController>().Deactivate();
        handler = animator.GetComponentInParent<TransportHandler>();
        goal = GetStorageObject();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move towards goal
        if (Vector2.Distance(animator.transform.position, goal.transform.position) > 0.01)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, goal.transform.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("Arrived");
        }
    }

    private GameObject GetStorageObject()
    {
        float temp = 0;
        GameObject result = null;
        foreach (GameObject Object in handler.MinerNodes)
        {
            OreContainer container = Object.GetComponent<TransporterNode>().GetOreContainer();
            float orePercentage = container.GetCurrentOreAmount() / container.GetMaxOreAmount();
            if (orePercentage >= temp)
            {
                temp = orePercentage;
                result = Object;
            }
        }
        if (temp < 0.75)
        {
            foreach (GameObject Object in handler.SiloNodes)
            {
                OreContainer container = Object.GetComponent<TransporterNode>().GetOreContainer();
                if (container.GetCurrentOreAmount() >= temp)
                {
                    temp = container.GetCurrentOreAmount();
                    result = Object;
                }
            }
        }
        return result;

    }
}