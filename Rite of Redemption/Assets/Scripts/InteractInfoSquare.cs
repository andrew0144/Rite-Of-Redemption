using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractInfoSquare : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private TextMeshPro InfoText;
    [SerializeField] private GameObject InfoTextBackdrop;
    [SerializeField] private string info;

    private Animator animator;
    private bool hasSpoken;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        animator = playerObject.GetComponent<Animator>();
        hasSpoken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
            if(col.gameObject.Equals(playerObject)){
                    if(!hasSpoken) {
                        animator.SetBool("Walking", false);
                        playerObject.GetComponent<PlayerCharacter>().Stop();
                        AudioManager.instance.Play("ScrollSound");
                        Vector3 pos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z);
                        InfoTextBackdrop.transform.position = pos;
                        InfoText.transform.position = pos;
                        InfoTextBackdrop.SetActive(true);
                        StartCoroutine(PlayText());
                    }
            }
    }

    IEnumerator PlayText()
	{
        AudioManager.instance.Play("DialogueSound");
        foreach (char c in info) 
		{
            if (Input.GetKey("space") || Input.GetKey("return")){
                break;
            }
			InfoText.text += c;
			yield return new WaitForSeconds (0.035f);
		}
        InfoText.text = info;
        AudioManager.instance.Stop("DialogueSound");
        hasSpoken = true;
        playerObject.GetComponent<PlayerCharacter>().StartMovement();
	}

    private void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.Equals(playerObject)) {
            InfoText.SetText("");
            InfoTextBackdrop.SetActive(false);
        }
    }
}
