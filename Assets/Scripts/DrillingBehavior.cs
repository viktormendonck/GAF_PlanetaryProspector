using UnityEngine;

public class DrillingBehavior : MonoBehaviour
{
    [SerializeField] private float drillSpeed = 1.0f;
    [SerializeField] private Vector2 startingPoint = new Vector2(0,0);
    [SerializeField] private Vector2 endPoint = new Vector2(0,0);
    [SerializeField] private GameObject connectionPoint;
    private bool isDrilling = true;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint;
        Vector2 direction = endPoint - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = rotation;
    }

    public void SetEndPoint(Vector2 endPoint)
    {
        this.endPoint = endPoint;
        Vector2 direction = endPoint - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = rotation;
    }
    public void SetStartingPoint(Vector2 startingPoint)
    {
        this.startingPoint = startingPoint;
        Vector2 direction = endPoint - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrilling)
        {
            GameObject node= Instantiate(connectionPoint,new Vector3(endPoint.x,endPoint.y,-1), Quaternion.identity,this.gameObject.transform.parent.transform);
            PipeController pipe = node.gameObject.transform.GetChild(0).GetComponent<PipeController>();
            pipe.SetPipePoints(startingPoint, endPoint);
            
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, drillSpeed * Time.deltaTime);
            //rotate the dril to the direction of the movement
            

            if ( Vector2.Distance(transform.position, endPoint) < 0.1)
            {
                isDrilling = false;
            }
        }
    }
}
