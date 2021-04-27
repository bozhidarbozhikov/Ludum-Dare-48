using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool mainMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mainMenu)
                FindObjectOfType<SceneMaster>().LoadNextLevel();
            else
                FindObjectOfType<SceneMaster>().LoadMainMenu();
        }
    }
}
