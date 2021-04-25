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
    public float score;

    public HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;

        speed = playerMovement.moveSpeed;
        halfSpeed = playerMovement.moveSpeed / 2;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        health--;
        healthBar.UpdateHealthUI(health, maxHealth);

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
