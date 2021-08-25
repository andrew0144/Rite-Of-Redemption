using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneStart : MonoBehaviour
{
    // The Text component of the text dump
    private Text comp_text;

    // The full string to be displayed
    private string fullstring;

    // The string currently being displayed
    private string displaystring;

    // Whether or not to start scrolling
    public bool shouldIScroll = true;

    // The time taken to display each character, in seconds
    public float oneCharacterTime = 0.08f;

    // The Audio Source component of the text dump
    private AudioSource comp_audiosource;

    // A following text object
    public CutsceneStart nextTextObject;

    // The 'Press Enter to Continue' text
    public GameObject continueObject;

    // The cutscene controller
    public IntroCutsceneController controllerObject;

    // Start is called before the first frame update
    void Start()
    {
        comp_text = GetComponent<Text>();
        comp_audiosource = GetComponent<AudioSource>();

        fullstring = comp_text.text;
        displaystring = "";

        comp_text.text = displaystring;
        if (shouldIScroll)
        {
            StartCoroutine(scrollText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startScroll()
    {
        shouldIScroll = true;
        StartCoroutine(scrollText());
    }

    private IEnumerator scrollText()
    {
        foreach (char c in fullstring)
        {
            displaystring += c;
            comp_text.text = displaystring;

            if (c != ' ' && c != '\n')
            {
                comp_audiosource.Play();
            }
            
            if (c == '.')
            {
                yield return new WaitForSeconds(oneCharacterTime * 5);
            } else if (c == ',')
            {
                yield return new WaitForSeconds(oneCharacterTime * 3);
            } else
            {
                yield return new WaitForSeconds(oneCharacterTime);
            }
        }
        if (continueObject != null)
        {
            continueObject.SetActive(true);
        }
        if (nextTextObject != null)
        {
            nextTextObject.startScroll();
        }
        if (controllerObject != null)
        {
            controllerObject.MakeReady();
        }
        yield return null;
    }
}
