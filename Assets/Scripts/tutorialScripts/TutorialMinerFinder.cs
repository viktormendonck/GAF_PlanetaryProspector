using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMinerFinder : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hasMoved = false;
    private TransportHandler transportHandler;


    [SerializeField] private readonly Vector3 offset = new Vector2(0,-0.65f);
    // Update is called once per frame
    void Start()
    {
        transportHandler = GameObject.FindGameObjectWithTag("TransportHandler").GetComponent<TransportHandler>();
    }
    [SerializeField] RectTransform canvasRectT;

    void Update()
    {
        if (hasMoved == false)
        {
            if (transportHandler.MinerNodes.Count > 0)
            {
                hasMoved = true;
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transportHandler.MinerNodes[0].transform.position + offset);
                GetComponent<RectTransform>().anchoredPosition = (screenPoint - canvasRectT.sizeDelta / 2f);
            }
        }
    }
}
