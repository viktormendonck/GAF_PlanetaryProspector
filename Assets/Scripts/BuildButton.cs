using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private Vector2 buildingSize;
    [SerializeField] private float heightOffset;
    [SerializeField] private float PreviewHeightOffset;
    [SerializeField] public bool hasCollisions = true;
    [SerializeField] private MoneyContainer moneyContainer;
    [SerializeField] private int cost;
    [SerializeField] private TransportHandler transportHandler;

    private readonly float maxClickDelay = 0.2f;
    private float currentlClickDelay = 0.2f;

    private bool isBuilding = false;
    private float buildHeight = 2.3f;

    GameObject buildingVisualizer;
    PreviewColorHandler visualizing;
    void Start()
    {
        buildingVisualizer = transform.GetChild(0).gameObject;
        buildingVisualizer.transform.localScale = new Vector3(buildingSize.x* (1/transform.localScale.x), buildingSize.y*(1/ transform.localScale.y), 1);
        buildingVisualizer.transform.localPosition = new Vector3(0, buildHeight+ PreviewHeightOffset, 1);
        buildingVisualizer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        buildingVisualizer.GetComponent<SpriteRenderer>().enabled = false;
        visualizing = buildingVisualizer.GetComponent<PreviewColorHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        visualizing.hasEnoughMoney = cost <= moneyContainer.GetMoney();
        if (isBuilding)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingVisualizer.transform.position = new Vector3(mousePos.x, buildHeight + PreviewHeightOffset, buildingVisualizer.transform.position.z);
            currentlClickDelay -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && visualizing.CanPlace && currentlClickDelay < 0)
            {
                isBuilding = false;
                if (cost <= moneyContainer.GetMoney())
                {
                    buildingVisualizer.GetComponent<SpriteRenderer>().enabled = false;
                    GameObject go= Instantiate(buildingPrefab, parent.transform);
                    go.transform.position = new Vector3(mousePos.x, buildHeight + heightOffset, buildingVisualizer.transform.position.z);
                    moneyContainer.AddMoney(-cost);
                    if (go.GetComponent<TransportNodeGetter>() != null)
                    {
                        GameObject node = go.GetComponent<TransportNodeGetter>().GetTransportNode();
                        if (go.CompareTag("Transporter"))
                        {
                            transportHandler.Transporters.Add(node);
                        } 
                        else if(go.CompareTag("Silo"))
                        {
                            transportHandler.SiloNodes.Add(node);
                        }
                        else if (go.CompareTag("Miner"))
                        {
                            transportHandler.MinerNodes.Add(node);
                        }
                    }
                   
                }
            }
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isBuilding)
            {
                isBuilding = false;
                buildingVisualizer.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                isBuilding = true;
                currentlClickDelay = maxClickDelay;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = buildHeight;
                visualizing.hasEnoughMoney = cost <= moneyContainer.GetMoney();
                buildingVisualizer.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
