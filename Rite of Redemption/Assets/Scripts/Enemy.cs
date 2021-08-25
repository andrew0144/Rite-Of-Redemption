using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple enemy script
public class Enemy : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject;

    // Smoke puff prefab
    public GameObject smokePuff;

    //The main camera
    private Camera cam;

    //The amount the camera shakes upon hitting an enemy
    private float shakeOnHitAmount = 3f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Takes damage upon being hit
    public void onHit(){
        GameObject smoke = Instantiate(smokePuff) as GameObject;
        AudioManager.instance.Play("EnemyDeathSound");
        smoke.transform.position = this.transform.position;
        cam.GetComponent<Follow>().setShake(shakeOnHitAmount);
        Destroy(this.gameObject);
    }

    public void onStun(){
        
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Player") {
            playerObject.GetComponent<Damage>().onHit();
        }
    }
}
