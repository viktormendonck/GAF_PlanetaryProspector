using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreCollectionBehavior : StateMachineBehaviour
{
    private TransportHandler handler;

    private GameObject container;

    private OreContainer containerOreContainer;
    private OreContainer TransporterOreContainer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //no need to try, these are always present
        TransporterNode node = animator.GetComponent<TransporterNode>();
        animator.GetComponent<TransporterController>().Activate(TransporterController.TransporterState.Gathering);
        handler = animator.GetComponentInParent<TransportHandler>();
        container = GetClosestContainer(animator);
        containerOreContainer = container.GetComponent<TransporterNode>().GetOreContainer();
        TransporterOreContainer = node.GetOreContainer();
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
        animator.ResetTrigger("AllContainersEmpty");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (container == null)
        {
            animator.SetTrigger("DoneFilling");
        }
        else
        {

            float transporterFill = TransporterOreContainer.GetCurrentOreAmount() - TransporterOreContainer.GetMaxOreAmount();
            float containerFill = containerOreContainer.GetCurrentOreAmount();
            if (containerFill <= 0.5 || transporterFill <= 0.5) //if the container is empty or the transporter is full
            {
                animator.SetTrigger("DoneFilling");
            }
        }
    }

    private GameObject GetClosestContainer(Animator animator)
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
        foreach (var silo in handler.SiloNodes)
        {
            float distance = Vector3.Distance(silo.transform.position, animator.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = silo;
            }
        }

        return result;
    }
}
