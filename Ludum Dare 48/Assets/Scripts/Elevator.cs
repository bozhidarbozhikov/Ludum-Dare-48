using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool unlocked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && unlocked)
        {
            FindObjectOfType<SceneMaster>().LoadNextLevel();
        }
    }
}
