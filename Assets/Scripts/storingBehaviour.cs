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
        siloOreContainer = silo.GetComponent<TransporterNode>().GetOreContainer();
        TransporterOreContainer = node.GetOreContainer();
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
            
            float siloFill = siloOreContainer.GetCurrentOreAmount() - siloOreContainer.GetMaxOreAmount();
            float transporterFill = TransporterOreContainer.GetCurrentOreAmount();
            if (siloFill <= 0.5 || transporterFill <= 0.5) //if the silo is full or the transporter is empty
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
