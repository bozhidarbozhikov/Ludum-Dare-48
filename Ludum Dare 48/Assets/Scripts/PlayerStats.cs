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

    public HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;

        speed = playerMovement.moveSpeed;
        halfSpeed = playerMovement.moveSpeed / 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
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
            FindObjectOfType<AudioManager>().Play("Player_Die");

            FindObjectOfType<SceneMaster>().RestartLevel();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Player_Hurt");
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
