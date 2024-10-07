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
                GameObject drillObject = Instantiate(drill, this.transform.position, Quaternion.identity,this.transform.parent.transform);
                DrillingBehavior drillBehavior = drillObject.GetComponent<DrillingBehavior>();
                drillBehavior.SetEndPoint(mouseWorldPos);
                drillBehavior.SetStartingPoint(this.transform.position);
                currentConnections++;
            }
        }

        
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && maxConnections > currentConnections)
        {
            isPreviewActive = true;
            

        }
    }
    private bool IsPipeValid()
    {
        if (Vector2.Distance(this.transform.position, mouseWorldPos) < 0.2f)
        {
            print("too short");
            return false;
        }
        if (!groundBox.OverlapPoint(mouseWorldPos))
        {
            print("not on the ground");
            return false;
        }
        Vector3 dir = (mouseWorldPos - this.transform.position).normalized;
        
        if (Physics2D.LinecastAll(this.transform.position+ dir, mouseWorldPos, 1 << LayerMask.NameToLayer(_buildinglayer)).Length > 1 )
        {
            print("something in the way" + Physics2D.LinecastAll(this.transform.position, mouseWorldPos, 1 << LayerMask.NameToLayer(_buildinglayer)).Length);
            return false;
        }
        return true;
    }
}
