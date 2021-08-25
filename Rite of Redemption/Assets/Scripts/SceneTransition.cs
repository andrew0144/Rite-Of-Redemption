using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string scene = "";
    [SerializeField] int fadeSpeed;
    private GameObject playerObject;
    private SpriteRenderer Blackout;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        Blackout = GameObject.Find("Blackout").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.Equals(playerObject)){
            StartCoroutine(FadetoNewScene());
        }
    }

    public IEnumerator FadetoNewScene() {
        Color objColor = Blackout.color;
        float fadeAmount;
        while(Blackout.color.a < 1) {
            fadeAmount = objColor.a + (fadeSpeed*Time.deltaTime);
            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            Blackout.color = objColor;
            yield return null;
        }
        if(scene == "Level 4"){
            if(playerObject.GetComponent<Inventory>().hasSword()){
                SceneManager.LoadScene("Level 4 Sword");
            }
            else if(playerObject.GetComponent<Inventory>().hasWand()){
                SceneManager.LoadScene("Level 4 Wand");
            }
            else{
                SceneManager.LoadScene("Level 4 Shield");
            }
        }
        else{
            SceneManager.LoadScene(scene);
        }
    }
}
