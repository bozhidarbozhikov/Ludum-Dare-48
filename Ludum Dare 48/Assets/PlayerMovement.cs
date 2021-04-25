using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mouse_pos;
    Vector3 object_pos;
    float angle;

    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = movement * moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;

        //rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
    }


    void LookAtMouse()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}

