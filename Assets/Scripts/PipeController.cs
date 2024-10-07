using UnityEngine;

public class PipeController : MonoBehaviour
{
    public void SetPipePoints(Vector2 startPoint, Vector2 endPoint)
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        Vector3 midPoint = (startPoint + endPoint) / 2;
        midPoint = new Vector3(midPoint.x,midPoint.y, -0.75f);
        float angle = Mathf.Atan2(endPoint.y - startPoint.y, endPoint.x - startPoint.x) * Mathf.Rad2Deg;

        transform.position = midPoint;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(distance*(1 / gameObject.transform.parent.transform.lossyScale.x), 0.5f , transform.localScale.z);
    }
}
