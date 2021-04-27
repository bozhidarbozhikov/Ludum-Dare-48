using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int money = 2;

    public enum Size
    {
        Small,
        Medium
    }
    public Size treasureSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<ScoreCounter>().score += money;

            FindObjectOfType<Score>().ChangeText();

            switch (treasureSize)
            {
                case Size.Small:
                    FindObjectOfType<AudioManager>().Play("Gold_PickUp_Small");
                    break;
                case Size.Medium:
                    FindObjectOfType<AudioManager>().Play("Gold_PickUp_Medium");
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }

      

       
    }
}
