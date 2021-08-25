using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneController : MonoBehaviour
{
    private bool ready = false;

    // The name of the scene to send the player to
    public string goToScene = "";

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(goToScene);
        }

        if (ready)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(goToScene);
            }
        }
    }

    public void MakeReady()
    {
        ready = true;
    }
}
