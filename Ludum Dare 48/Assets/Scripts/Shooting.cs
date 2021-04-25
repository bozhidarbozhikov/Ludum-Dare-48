using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float addedForce = 20f;
    public Transform firePoint;
    Vector2 mousePos;
    public Camera cam;
    public Rigidbody2D rb2d;

    public float damage;
    public float buckshotRotationDeviation;

    private void Start()
    {

    }

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
        /*float rotationDeviation = -30;

        for (int i = 0; i < 3; i++)
        {
            firePoint.eulerAngles = new Vector3(0, 0, rotationDeviation);

            Vector2 lookDirection = mousePos - rb2d.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            bullet.GetComponent<PlayerBullet>().damage = damage;

            rb.AddForce(bullet.transform.up * addedForce, ForceMode2D.Impulse);

            rotationDeviation += 30f;
        }

        firePoint.eulerAngles = Vector3.zero;*/

        for (int i = 0; i < 3; i++)
        {
            float rotationDeviation = 0;

            if (i == 1) rotationDeviation = buckshotRotationDeviation;
            if (i == 2) rotationDeviation = -buckshotRotationDeviation;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(RotateVector2(firePoint.up, rotationDeviation * Mathf.Deg2Rad) * addedForce, ForceMode2D.Impulse);
        }
    }

    public static Vector2 RotateVector2(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
