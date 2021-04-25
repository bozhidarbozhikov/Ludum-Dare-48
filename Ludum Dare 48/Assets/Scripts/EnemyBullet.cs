using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public bool isCobweb;
    public GameObject cobwebPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCobweb)
        {
            Vector3 spawnPos = FindObjectOfType<Grid>().WorldToCell(transform.position);
            spawnPos += new Vector3(0.5f, 0.5f);

            Instantiate(cobwebPrefab, spawnPos, Quaternion.identity);
        }
        else if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerStats>().TakeDamage();
        }

        Destroy(gameObject);
    }
}
