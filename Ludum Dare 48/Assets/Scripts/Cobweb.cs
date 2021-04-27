using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : MonoBehaviour
{
    public float speedDecreaseModifier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Cobweb_Stuck");

            collision.GetComponent<PlayerStats>().numOfCobwebs++;
            collision.GetComponent<PlayerStats>().CalculateCobwebs();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().numOfCobwebs--;
            collision.GetComponent<PlayerStats>().CalculateCobwebs();
        }
    }
}
