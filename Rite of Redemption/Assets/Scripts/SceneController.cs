using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private TextMeshPro InfoText;
    [SerializeField] private GameObject InfoTextBackdrop;
    private GameObject ItemMenuBackdrop;
    private GameObject altarSword;
    private GameObject altarShield;
    private GameObject altarFire;

    // Pausing variables
    private bool isPaused;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        ItemMenuBackdrop = GameObject.Find("ItemMenuBackdrop");
        altarSword = GameObject.Find("altarSword");
        altarShield = GameObject.Find("altarShield");
        altarFire = GameObject.Find("altarFire");
        altarSword.SetActive(false);
        altarShield.SetActive(false);
        altarFire.SetActive(false);
        ItemMenuBackdrop.SetActive(false);
        InfoText.SetText("");
        InfoTextBackdrop.SetActive(false);
    }

    void Update()
    {
        // Check for pause inputs
        if (Input.GetKeyDown("escape"))
        {
            if (!isPaused)
            {
                PauseGame();
                AudioManager.instance.Play("PauseSound");
            }
            else ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        // Rather than quit the game entirely, this button 'quits' by returning the player to the main menu.
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        isPaused = false;
    }
}
