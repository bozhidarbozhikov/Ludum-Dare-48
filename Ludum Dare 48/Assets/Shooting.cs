using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float addedForce = 20f;
    public Transform firePoint;
    Vector2 mousePos;
    public Camera cam;
    public Rigidbody2D rb2d;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            AimAndShoot();
        }
    }
    void AimAndShoot()
    {
        Vector2 lookDirection = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.eulerAngles = new Vector3(0, 0, angle);

        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * addedForce, ForceMode2D.Impulse);
        arrow.transform.Rotate(0, 0, angle + 90f);
        Destroy(arrow, 2f);

    }
}
