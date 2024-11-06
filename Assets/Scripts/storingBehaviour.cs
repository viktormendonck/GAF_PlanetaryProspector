using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storingBehaviour : StateMachineBehaviour
{
    private TransportHandler handler;

    private GameObject silo;

    private OreContainer siloOreContainer;
    private OreContainer TransporterOreContainer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TransporterNode node = animator.GetComponent<TransporterNode>();
        animator.GetComponent<TransporterController>().Activate(TransporterController.TransporterState.Depositing);
        handler = animator.GetComponentInParent<TransportHandler>();
        silo = GetClosestSilo(animator);
        if (silo)
        {
            siloOreContainer = silo.GetComponent<TransporterNode>().GetOreContainer();
        }
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (silo == null)
        {
            animator.SetTrigger("DoneDepositing");
        }
        else
        {
            
            float siloFill =  siloOreContainer.GetMaxOreAmount() - siloOreContainer.GetCurrentOreAmount();
            if (siloFill <= 0.5 || animator.GetFloat("transportFullness") <= 0.05) //if the silo is full or the transporter is empty
            {
                animator.SetTrigger("DoneDepositing");
            }
        }
    }

    private GameObject GetClosestSilo(Animator animator)
    {
        GameObject result =null;
        float minDistance = float.MaxValue;
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
