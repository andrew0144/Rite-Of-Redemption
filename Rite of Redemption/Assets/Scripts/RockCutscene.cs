using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCutscene : MonoBehaviour
{
    private GameObject playerObject;

    //The main camera
    private Camera cam;
    // Start is called before the first frame update

    [SerializeField] private GameObject rock;
    void Start()
    {
        playerObject = GameObject.Find("Player");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (playerObject.Equals(col.gameObject))
        {
            StartCoroutine(cutscene());
        }
    }

    private IEnumerator cutscene(){
        rock.SetActive(true);
        playerObject.GetComponent<PlayerCharacter>().Stop();
        cam.transform.Translate(0, 5f, 0, Space.Self);
        cam.GetComponent<Follow>().isFollowing(false);
        yield return new WaitForSeconds(4f);{
        cam.transform.Translate(0, -5f, 0, Space.Self); 
        cam.GetComponent<Follow>().isFollowing(true);
        playerObject.GetComponent<PlayerCharacter>().StartMovement();
        this.gameObject.SetActive(false);
        }
    }
}
