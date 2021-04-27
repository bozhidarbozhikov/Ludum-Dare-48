using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        scoreText.text = "Score: " + FindObjectOfType<ScoreCounter>().score.ToString();
    }
}
