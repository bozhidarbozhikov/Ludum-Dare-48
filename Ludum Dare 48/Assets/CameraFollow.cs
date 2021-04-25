using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D targetRb;

    public Rigidbody2D rb;

    public float smoothSpeed;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(targetRb.position.x + offset.x, targetRb.position.y + offset.y, offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        rb.position = smoothedPosition;
    }
}
