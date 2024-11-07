using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreScanning : MonoBehaviour
{
    public bool foundOre = false;
    [SerializeField] private int chanceToScan = 50;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ore" && Random.Range(0, 101) < chanceToScan)
        {
            if (collision.gameObject.TryGetComponent(out VeinInfo ore))
            {
                if (!ore.wasFound())
                {
                    foundOre = true;
                }
            }
        }
    }
}
