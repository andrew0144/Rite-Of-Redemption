using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple enemy script
public class RollingRock : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject; 
    
    //An array of all objects with the enemy tag
    private GameObject[] enemies;

    //Speed
    [SerializeField] private float speed = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
    }

    //Takes damage upon being hit
    public void onHit(){
        Destroy(this.gameObject);
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerEnter2D(Collider2D col){
        bool hitEnemy = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            if(col.gameObject.Equals(enemy) || col.gameObject.GetComponentInParent<Enemy>()){
                col.gameObject.GetComponentInParent<Enemy>().onHit();
                hitEnemy = true;
            }
        }
        if(playerObject.Equals(col.gameObject) && !playerObject.GetComponent<PlayerCharacter>().isDefending()){
            playerObject.GetComponent<Damage>().onHit();
        }
        else if (!hitEnemy){
            speed*=-1;
        }
    }
}
