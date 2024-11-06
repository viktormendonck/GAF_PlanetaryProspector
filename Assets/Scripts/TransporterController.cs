using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TransporterController : MonoBehaviour
{
    
    public enum TransporterState
    {
        Gathering,
        Depositing,
        Traveling
    }
    private TransporterState state;
    public bool isSelling = false;
    private OreContainer transferContainer = null;
    private readonly int filledness_id = Animator.StringToHash("transportFullness");

    private TransportHandler handler;

    private OreContainer container;

    private Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        handler = GetComponentInParent<TransportHandler>();
        container = GetComponent<OreContainer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetFloat("transportFullness", container.GetCurrentOreAmount() / container.GetMaxOreAmount());
        

        switch (state)
        {
            case TransporterState.Gathering:
                transferContainer.TransferOre(container);
                break;
            case TransporterState.Depositing:
                container.TransferOre(transferContainer);
                break;
            case TransporterState.Traveling:
                break;
        }
    }
    public void Activate(TransporterState newState)
    {
        state = newState;
        transferContainer = GetClosestOreContainer();
    }
    public void Deactivate()
    {
        state = TransporterState.Traveling;
        transferContainer = null;
    }
    private OreContainer GetClosestOreContainer()
    {
        GameObject result = null;
        float minDistance = float.MaxValue;
        foreach (var miner in handler.MinerNodes)
        {
            float distance = Vector2.Distance(miner.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = miner;
            }
        }
        foreach (var silo in handler.SiloNodes)
        {
            float distance = Vector2.Distance(silo.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = silo;
            }
        }
        float d = Vector2.Distance(handler.marketNodes[0].transform.position, transform.position);
        if (d < minDistance)
        {
            minDistance = d;
            result = handler.marketNodes[0];
        }
        if (result == null)
        {
            return null;
        }
        //these getComponents don't need to be tried as to be one of the objects returned by the handler they need to have the components
        return result.GetComponent<TransporterNode>().GetOreContainer();
    }
}
