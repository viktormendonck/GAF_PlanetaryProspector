using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MetalDetectorController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject metalDetector;
    private OreScanning oreScanning;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxScanDelay = 1f;
    private float currentScanDelay = 0f;
    [SerializeField] private float scanRange = 1.5f;
    [SerializeField] private int maxScanDepth = 4;
    [SerializeField] private int currentScanDepth = 0;
    [SerializeField] private float maxX = 7.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float maxFlickerTime = 0.5f;
    private float currentFlickerTime = 0f;

    [SerializeField] private float waitingTime = 10f;
    void Start()
    {
        metalDetector = transform.GetChild(0).gameObject;
        metalDetector.transform.localScale = new Vector3(scanRange, scanRange, 1);
        metalDetector.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        oreScanning = metalDetector.GetComponent<OreScanning>();
        spriteRenderer.enabled = false
            ;
    }

    private bool isScanning = false;
    private bool foundOre = false;
    private Vector3 targetPosition;
    // Update is called once per frame
    void Update()
    {
        if (foundOre)
        {
            if (waitingTime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                waitingTime -= Time.deltaTime;
                
            }
            if (currentFlickerTime >= maxFlickerTime)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                currentFlickerTime = 0;
            }
            else
            {
                currentFlickerTime += Time.deltaTime;
            }
            return;
        }
        if (isScanning)
        {
            if (currentScanDelay <= 0)
            {
                currentScanDelay = maxScanDelay;
                currentScanDepth++;
                metalDetector.transform.position = new Vector3(metalDetector.transform.position.x, metalDetector.transform.position.y - scanRange, metalDetector.transform.position.z);

                if (oreScanning.foundOre) 
                {
                    foundOre = true;
                    spriteRenderer.enabled = true;

                }

                if (currentScanDepth >= maxScanDepth)
                {
                    isScanning = false;
                    metalDetector.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    currentScanDepth = 0;
                }
            }
            else
            {
                currentScanDelay -= Time.deltaTime;
            }
        }
        else
        {
            if (targetPosition == new Vector3(0, 0, 0))
            {
                targetPosition = new Vector3(Random.Range(-maxX, maxX), transform.position.y, transform.position.z);
            }
            else if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                isScanning = true;
                targetPosition = new Vector3(0, 0, 0);
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }

}
