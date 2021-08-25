using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        enemy = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Finds the enemy the sword has hit and deals damage to it
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            playerObject.gameObject.GetComponent<Damage>().onHit();
            if(enemy!=null && col.gameObject.GetComponent<MeleeEnemy>() != null){
                enemy.GetComponent<MeleeEnemy>().stun();
            }
            else if(enemy!=null && col.gameObject.GetComponent<SlimeEnemy>() != null){
                enemy.GetComponent<SlimeEnemyMovement>().stun();
            }
        }
    }
}
