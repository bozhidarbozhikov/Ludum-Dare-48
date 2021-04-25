using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePos;
    Vector2 lookDir;
    float angle;
    public Camera cam;

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
        //LookAtMouse();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        LookAtMouse();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


    void LookAtMouse()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = angle;
    }

   
}

