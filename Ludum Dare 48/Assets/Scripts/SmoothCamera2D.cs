using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 goalPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
    }
}