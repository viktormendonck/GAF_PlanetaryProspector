using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreContainer : MonoBehaviour
{
    [SerializeField] private float maxOreAmount = 50;
    [SerializeField] private float currentOreAmount = 0;
    [SerializeField] private float transferSpeed = 1;
    private OreContainer parentContainer;

    private void Start()
    {

        if (TryGetComponent<DrillNodeController>(out DrillNodeController temp))
        {
            parentContainer = temp.GetParent().GetComponent<OreContainer>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ore")
        {
            if (maxOreAmount - currentOreAmount > transferSpeed)
            {
                currentOreAmount += collision.gameObject.GetComponent<VeinInfo>().MineVein(transferSpeed);
            }
        }
    }
    private void Update()
    {
        TransferOre();
    }

    private void TransferOre()
    {
        if (parentContainer != null)
        {
            if (parentContainer.maxOreAmount - parentContainer.currentOreAmount > transferSpeed && currentOreAmount >0)
            {
                parentContainer.currentOreAmount += transferSpeed * Time.deltaTime;
                currentOreAmount -= transferSpeed * Time.deltaTime;
            }
        }

    }
    public float GetCurrentOreAmount()
    {
        return currentOreAmount;
    }
    public float GetMaxOreAmount()
    {
        return maxOreAmount;
    }
}
