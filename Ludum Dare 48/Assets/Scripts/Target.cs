using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Enemy_Die");

            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Enemy_Hurt");
        }
    }
}
