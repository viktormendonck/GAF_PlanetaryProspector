using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TransportNodeGetter : MonoBehaviour
{
    [SerializeField] private GameObject transportNode;

    void Awake()
    {
        if (!transportNode.TryGetComponent<TransporterNode>(out TransporterNode T))
        {
            Debug.LogError("Referenced object does not contain a transporterNode:" + gameObject.name);
        }
    }
    public GameObject GetTransportNode()
    {
        return transportNode;
    }
}
