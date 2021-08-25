using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The player's melee attack script
public class Attack : MonoBehaviour
{
    //An array of all objects with the enemy tag
    private GameObject[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Finds the enemy the sword has hit and deals damage to it
    private void OnTriggerEnter2D(Collider2D col){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            if(col.gameObject.Equals(enemy)){
                if((enemy.gameObject.GetComponent("SlimeEnemy") as SlimeEnemy) != null)
                {
                    col.gameObject.GetComponent<SlimeEnemy>().onHit();
                }
                else{
                    col.gameObject.GetComponent<Enemy>().onHit();
                }
            }
        }
    }
}
