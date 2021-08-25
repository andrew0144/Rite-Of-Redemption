using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The player's onDamage script
public class Damage : MonoBehaviour
{
    //The player's health
    private int health = 5;

    //The length of the player's invincibility frames
    private float iFrameLength = 1f;

    //The last time the player took damage
    private float oldTime;

    //A representation of the player object
    private GameObject playerObject;

    //The healthbar in the HUD.
    private GameObject healthbar;
    //scene transition component
    private SpriteRenderer Blackout;
    [SerializeField] float fadeSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        oldTime = -iFrameLength;
        playerObject = GameObject.Find("Player");
        healthbar = GameObject.Find("Healthbar");
        Blackout = GameObject.Find("Blackout").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        if(oldTime + iFrameLength <= Time.time){
            playerObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(health <= 0){
            Die();
        }
    }

    //The player will take damage on hit if they are not defending and are not invincible
    public void onHit(){
        if(!playerObject.GetComponent<PlayerCharacter>().isDefending() && oldTime + iFrameLength <= Time.time && !playerObject.GetComponent<PlayerCharacter>().isInvincible()){
            AudioManager.instance.Play("DamageSound");
            health -= 1;
            oldTime = Time.time;
            playerObject.GetComponent<SpriteRenderer>().color = Color.red;
            playerObject.transform.GetComponent<PlayerCharacter>().BackUp();
            healthbar.GetComponent<Healthbar>().displayHealth(health);
        }
    }

    //When the player reaches 0 health, they will die
    private void Die(){
        playerObject.GetComponent<SpriteRenderer>().color = Color.black;
        playerObject.GetComponent<PlayerCharacter>().Stop();
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel() {
        Color objColor = Blackout.color;
        float fadeAmount;
        while(Blackout.color.a < 1) {
            fadeAmount = objColor.a + (fadeSpeed*Time.deltaTime);
            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            Blackout.color = objColor;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
