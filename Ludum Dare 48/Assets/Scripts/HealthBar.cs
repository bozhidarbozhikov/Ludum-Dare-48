using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats playerStats;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;

    public void UpdateHealthUI(int health, int maxHealth)
    {
        if (maxHealth > hearts.Length * 2)
        {
            maxHealth = hearts.Length * 2;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth / 2)
            {
                hearts[i].enabled = true;

                if (i < health / 2)
                {
                    hearts[i].sprite = fullHeart;
                }
                else if (i == health / 2 + health % 2 - 1)
                {
                    hearts[i].sprite = halfHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

   
    }
}