using UnityEngine;


public class DrillNodeController : MonoBehaviour
{
    private LineRenderer previewLineRenderer;
    private bool isPreviewActive = false;
    [SerializeField] public int maxConnections = 1;
    [SerializeField] private GameObject drill;
    private Collider2D groundBox;
    [SerializeField] private Color previewColor;
    [SerializeField] private Color invalidColor;
    [SerializeField] private float pricePerUnit = 10.0f;
    [SerializeField] private float basePrice = 25.0f;

    private const string _buildinglayer = "Building";
    [SerializeField] private GameObject ParentObject;

    private MoneyContainer money;
    private PriceIndicator priceIndicator;

    private Vector3 mouseWorldPos;
    private int currentConnections = 0;
    void Start()
    {
        groundBox = GameObject.Find("Ground").GetComponent<Collider2D>();
        previewLineRenderer = GetComponent<LineRenderer>();
        previewLineRenderer.material = previewLineRenderer.materials[0];
        money = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyContainer>();
        priceIndicator = GameObject.FindGameObjectWithTag("PriceIndicator").GetComponent<PriceIndicator>();
    }

    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isPipeValid = false;
        if (isPreviewActive)
        {
            previewLineRenderer.enabled = true;
            previewLineRenderer.SetPosition(0, this.transform.position);
            previewLineRenderer.SetPosition(1, mouseWorldPos);
            //for some reason linerenderers work with a lerped line, so i guess this works
            if (isPipeValid = IsPipeValid())
            {
                previewLineRenderer.startColor = previewColor;
                previewLineRenderer.endColor = previewColor;
            }
            else
            {
                previewLineRenderer.startColor = invalidColor;            
                previewLineRenderer.endColor = invalidColor;            
            }
        }
        else
        {
            previewLineRenderer.enabled = false;
        }
        if (Input.GetMouseButtonUp(0) && isPreviewActive )
        {
            priceIndicator.Deactivate();
            isPreviewActive = false;
            if (isPipeValid)
            {
                money.AddMoney(-((Vector2.Distance(transform.position, mouseWorldPos) * pricePerUnit) + basePrice));
                GameObject drillObject = Instantiate(drill, transform.position, Quaternion.identity, transform);
                DrillingBehavior drillBehavior = drillObject.GetComponent<DrillingBehavior>();
                drillBehavior.SetConnectionParent(gameObject);
                drillBehavior.SetEndPoint(mouseWorldPos);
                drillBehavior.SetStartingPoint(transform.position);
                currentConnections++;
            }
        }
    }

    public void SetParentObject(GameObject parent)
    {
        ParentObject = parent;
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPreviewActive = true;
            priceIndicator.Activate(0);
            priceIndicator.fixedHeight = false;

        }
    }
    private bool IsPipeValid()
    {
        float distance = Vector2.Distance(transform.position, mouseWorldPos);
        float price = distance * pricePerUnit+ basePrice;
        priceIndicator.setPrice((int)price);
        //check if you can afford to place the pipe
        if (price > money.GetMoney())
        {
            priceIndicator.canAfford = false;
            return false;
        }
        else
        {
            priceIndicator.canAfford = true;
        }
        //check if the pipe isnt too short
        if (distance < 0.2f)
        {
            return false;
        }
        //check if ur not mining air
        if (!groundBox.OverlapPoint(mouseWorldPos))
        {
            return false;
        }
        //check if the line isnt crossing any other pipes
        Vector3 dir = (mouseWorldPos - transform.position).normalized;
        if (Physics2D.LinecastAll(transform.position+ dir, mouseWorldPos, 1 << LayerMask.NameToLayer(_buildinglayer)).Length > 1 )
        {
            return false;
        }
        //check if pipe has any branches left
        if (currentConnections + 1 > maxConnections)
        {
            return false;
        }
        return true;
    }
    public GameObject GetParent()
    {
        return ParentObject;
    }
}
