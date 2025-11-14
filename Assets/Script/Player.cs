using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private LineRenderer lr;

    private float power = 2.0f;//はじく力
    private float maxpower = 5.0f;//最大威力

    private Vector3 StartPos;
    private Vector3 EndPos;
    private bool isDragging = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.enabled = false;
    }

    private void OnMouseDown()
    {
        float z = Camera.main.ScreenToWorldPoint(transform.position).z;

        StartPos = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x , Input.mousePosition.y,z));

        isDragging = true;
        lr.enabled = true;
        lr.SetPosition(0,transform.position);
    }

    private void OnMouseDrag()
    {
        float z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 dragPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, z)
        );

        // LineRenderer に引っ張り線を描く（オブジェクトは動かさない）
        lr.SetPosition(1, dragPos);
    }
    private void OnMouseUp()
    {
        isDragging = false;
        lr.enabled = false;

        float z = Camera.main.WorldToScreenPoint(transform.position).z ;

        Vector3 endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));


        Vector3 force = Vector3.ClampMagnitude((StartPos - EndPos),maxpower);
        rb.AddForce(force * power,ForceMode.Impulse);
    }
}
