using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public GameObject fadePanel;

    public float loadingScreenTime;

    public Vector3 elevatorPos;

    public IEnumerator LoadLevel(int levelIndex)
    {
        fadePanel.GetComponent<Animator>().SetTrigger("Fade In");

        yield return new WaitForSeconds(loadingScreenTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Clicked");

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));

        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(loadingScreenTime);

        player.position = elevatorPos;

        FindObjectOfType<ScoreCounter>().score /= 3;
        FindObjectOfType<Score>().ChangeText();

        player.gameObject.SetActive(true);


        yield return new WaitForSeconds(loadingScreenTime * 0.75f);

    }
}
