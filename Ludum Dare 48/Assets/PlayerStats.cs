using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int health;

    [HideInInspector]
    public int numOfCobwebs;

    public PlayerMovement playerMovement;
    private float speed;
    private float halfSpeed;

    private void Start()
    {
        health = maxHealth;

        speed = playerMovement.moveSpeed;
        halfSpeed = playerMovement.moveSpeed / 2;
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CalculateCobwebs()
    {
        if (numOfCobwebs > 0)
        {
            playerMovement.moveSpeed = halfSpeed;
        }
        else
        {
            playerMovement.moveSpeed = speed;
        }
    }
}
