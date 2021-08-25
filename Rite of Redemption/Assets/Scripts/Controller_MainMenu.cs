using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_MainMenu : MonoBehaviour
{
    public string StartSceneName;
    public GameObject MainMenuObject;
    public GameObject ControlsObject;
    public GameObject Logo;

    public void StartGame()
    {
        AudioManager.instance.Play("AcceptUISound");
        PlayerPrefs.SetInt("sword", 1);
        PlayerPrefs.SetInt("shield", 1);
        PlayerPrefs.SetInt("wand", 1);
        PlayerPrefs.SetInt("itemsInPlay", 3);
        PlayerPrefs.SetInt("waveCount", 0);
        SceneManager.LoadScene(StartSceneName);
    }

    public void Controls()
    {
        AudioManager.instance.Play("AcceptUISound");
        Logo.SetActive(false);
        MainMenuObject.SetActive(false);
        ControlsObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.Play("AcceptUISound");
        Logo.SetActive(true);
        MainMenuObject.SetActive(true);
        ControlsObject.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("AcceptUISound");
        Application.Quit();
    }
}
