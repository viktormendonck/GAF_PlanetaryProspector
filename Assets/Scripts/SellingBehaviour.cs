using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBehaviour : StateMachineBehaviour
{
    private OreContainer TransporterOreContainer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TransporterNode node = animator.GetComponent<TransporterNode>();
        animator.GetComponent<TransporterController>().Activate(TransporterController.TransporterState.Depositing);
        TransporterOreContainer = node.GetOreContainer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float transporterFill = TransporterOreContainer.GetCurrentOreAmount();
        if (transporterFill <= 0.5) //if the transporter is empty
        {
            animator.SetTrigger("DoneDepositing");
        }
        
    }
}
