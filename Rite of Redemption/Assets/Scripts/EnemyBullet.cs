using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    //A represenation of the player object
    private GameObject playerObject;

    // speed of the bullet
    private float moveSpeed = 7f;

    // the rigidbody component of the bullet
    private Rigidbody2D rb;

    // direction of bullet
    private Vector2 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.Find("Player");
        moveDirection = (playerObject.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Finds the enemy the projectile has hit and deals damage to it
    private void OnTriggerEnter2D(Collider2D col)
    {
        {
            
        }
        if (col.gameObject.name == "Player")
        {   
            if(playerObject.GetComponentInChildren<Defend>() != null){
                playerObject.GetComponentInChildren<Defend>().reflect(rb.velocity, this.gameObject);
            }
            else{
                playerObject.GetComponent<Damage>().onHit();
            }
        }
        Destroy(this.gameObject);
    }
}
