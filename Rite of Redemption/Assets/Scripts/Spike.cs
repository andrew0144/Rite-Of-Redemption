using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple enemy script
public class Spike : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject; 
        //An array of all objects with the enemy tag
    private GameObject[] enemies;



    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Takes damage upon being hit
    public void onHit(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player" && !playerObject.GetComponent<PlayerCharacter>().isDefending())
        {
            AudioManager.instance.Play("SpikeHit");
        }
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerStay2D(Collider2D col){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (playerObject.Equals(col.gameObject)){           
            playerObject.GetComponent<Damage>().onHit();
        }
        foreach (GameObject enemy in enemies){
            if(col.gameObject.Equals(enemy)){
                if ((enemy.gameObject.GetComponent<SlimeEnemy>()) == null)
                {
                    col.gameObject.GetComponent<Enemy>().onHit();
                }
            }
        }
    }
}
