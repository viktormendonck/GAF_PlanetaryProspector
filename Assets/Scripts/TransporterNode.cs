using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransporterNode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private OreContainer oreContainer;

    void Awake()
    {
        if (oreContainer == null)
        {
            Debug.LogError("OreContainer not set to a reference of an oreContainer");
        }
    }
    public OreContainer GetOreContainer()
    {
        return oreContainer;
    }

    
}
