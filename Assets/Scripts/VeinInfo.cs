
using UnityEngine;

public class VeinInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private bool found = false;
    private bool depleted = false;
    [SerializeField] private float veinAmount = 100;
    private float totalAmount = 0;
    private Material veinMaterialInstance;
    [SerializeField] private Color depletedVeinColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    [SerializeField] private Color veinColor = new Color(0.258f, 0.117f, 0.1f, 1f);
    private bool MarkedForDeletion = false;
    void Start()
    {
        veinMaterialInstance = GetComponent<MeshRenderer>().material;
        veinMaterialInstance.color = veinColor;
    }


    public void  SetVeinSize(float size)
    {
        veinAmount*= size;
        totalAmount = veinAmount;
    }
    public float GetVeinAmount()
    {
        return veinAmount;
    }


    public bool wasFound()
    {
        return found;
    }

    public float MineVein(float drillSpeed)
    {
        found = true;
        if (veinAmount > 0)
        {
            veinAmount -= drillSpeed * Time.deltaTime;
            veinMaterialInstance.color = Color.Lerp(depletedVeinColor, veinColor, veinAmount / totalAmount);
            return drillSpeed * Time.deltaTime;
        }
        else
        {
            veinMaterialInstance.color = depletedVeinColor;
            depleted = true;
            return 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ore" && !MarkedForDeletion)
        {
            collision.gameObject.GetComponent<VeinInfo>().MarkedForDeletion = true;
            Destroy(collision.gameObject);
        }
    }

    public bool IsDepleted()
    {
        return depleted;
    }

}
