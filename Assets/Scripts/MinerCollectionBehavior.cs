using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerCollectionBehavior : StateMachineBehaviour
{
    private TransportHandler handler;

    private GameObject miner;

    private OreContainer minerOreContainer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TransporterNode node = animator.GetComponent<TransporterNode>();
        animator.GetComponent<TransporterController>().Activate(TransporterController.TransporterState.Gathering);
        handler = animator.GetComponentInParent<TransportHandler>();
        miner = GetClosestMiner(animator);
        minerOreContainer = miner.GetComponent<TransporterNode>().GetOreContainer();
        ClearAnimatorParams(animator);
    }

    private void ClearAnimatorParams(Animator animator)
    {
        //for some reason the triggers stay active instead of resetting
        animator.ResetTrigger("StartStoring");
        animator.ResetTrigger("DoneFilling");
        animator.ResetTrigger("StartSelling");
        animator.ResetTrigger("DoneDepositing");
        animator.ResetTrigger("Arrived");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (miner == null)
        {
            animator.SetTrigger("DoneFilling");
        }
        else
        {
            float minerFill = minerOreContainer.GetCurrentOreAmount();
            if (minerFill <= 0.5 || animator.GetFloat("transportFullness") >=0.95) //if the miner is empty or the transporter is full
            {
                animator.SetTrigger("DoneFilling");
            }
        }
    }

    private GameObject GetClosestMiner(Animator animator)
    {
        GameObject result = null;
        float minDistance = float.MaxValue;
        foreach (var miner in handler.MinerNodes)
        {
            float distance = Vector3.Distance(miner.transform.position, animator.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = miner;
            }
        }

        return result;
    }
}
