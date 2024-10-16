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

    private const string _buildinglayer = "Building";
    [SerializeField] private LayerMask _buildinglayerMask;
    [SerializeField] private GameObject ParentObject;

    private Vector3 mouseWorldPos;
    private int currentConnections = 0;
    void Start()
    {
        groundBox = GameObject.Find("Ground").GetComponent<Collider2D>();
        previewLineRenderer = GetComponent<LineRenderer>();
        previewLineRenderer.material = previewLineRenderer.materials[0];
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
            isPreviewActive = false;
            if (isPipeValid)
            {
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
            

        }
    }
    private bool IsPipeValid()
    {
        if (Vector2.Distance(transform.position, mouseWorldPos) < 0.2f)
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
