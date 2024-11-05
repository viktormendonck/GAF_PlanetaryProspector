using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransporterNode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private OreContainer oreContainer;

    public OreContainer GetOreContainer()
    {
        return oreContainer;
    }

    
}
