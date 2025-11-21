using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private LineRenderer lr;

    [SerializeField]
    private float power = 5.0f;//ÇÕÇ∂Ç≠óÕ
    [SerializeField]
    private float maxpower = 5.0f;//ç≈ëÂà–óÕ
    

    private Vector3 StartPos;
    private Vector3 EndPos;
    private bool isDragging = false;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 3;
        lr.enabled = false;
    }

    private bool GetMoyseWorldPos(out Vector3 worldPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);//y=0Ç…Ç∑ÇÈ

        if (plane.Raycast(ray, out float distance))
        {
            worldPos = ray.GetPoint(distance);
            return true;
        }
        worldPos = Vector3.zero;
        return false;
    }

    private void OnMouseDown()
    {
        GetMoyseWorldPos(out StartPos);
    }

    private void OnMouseDrag()
    {
        GetMoyseWorldPos(out Vector3 dragPos);

        Vector3 direction = StartPos - dragPos;//îÚÇ‘ï˚å¸

        //ñÓàÛé©ëÃ
        Vector3 arrowStart = transform.position;
        Vector3 arrowEmd = transform.position + direction.normalized * 2.0f;
        lr.SetPosition(0, arrowStart);
        lr.SetPosition(1, arrowEmd);

        float headSize = 0.3f;
        Vector3 right = Quaternion.Euler(0, 20, 0) * -direction.normalized;
        Vector3 left  = Quaternion.Euler(0, -20,0) * -direction.normalized;

        Vector3 arrowHead = arrowEmd + (right + left) * headSize;
        lr.SetPosition(2, arrowHead);

    }
    private void OnMouseUp()
    {
        lr.enabled = false;

        GetMoyseWorldPos(out EndPos);
        Vector3 force = Vector3.ClampMagnitude((StartPos - EndPos), maxpower);
        rb.AddForce(force * power, ForceMode.Impulse);

    }
}
