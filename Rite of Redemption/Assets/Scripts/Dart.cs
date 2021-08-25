using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The dart script
public class Dart : MonoBehaviour
{
    //The speed of the bullets
    [SerializeField] private float speed = 8.0f;
    
    //An array of all objects with the enemy tag
    private GameObject[] enemies;

    //A represenation of the player object
    private GameObject playerObject; 

    // The angle of the dart
    private Vector3 angle = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, speed*Time.deltaTime, 0));
    }

    //Finds the enemy the projectile has hit and deals damage to it
    private void OnTriggerEnter2D(Collider2D col){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            if(col.gameObject.Equals(enemy)){
                if ((enemy.gameObject.GetComponent("SlimeEnemy") as SlimeEnemy) != null)
                {
                    col.gameObject.GetComponent<SlimeEnemy>().duplicate();
                }
                else
                {
                    col.gameObject.GetComponent<Enemy>().onHit();
                }
            }
            else if(col.gameObject.GetComponentInParent<Enemy>()){
                if ((enemy.gameObject.GetComponentInParent<Enemy>().GetComponent("SlimeEnemy") as SlimeEnemy) != null)
                {
                    col.gameObject.GetComponentInParent<SlimeEnemy>().duplicate();
                }
                else
                {
                    col.gameObject.GetComponentInParent<Enemy>().onHit();
                }
            }
        }
        if (col.gameObject.name.Equals("Player"))
        {   
            if(playerObject.GetComponentInChildren<Defend>() != null){
                playerObject.GetComponentInChildren<Defend>().reflect(angle, this.gameObject);
            }
            else{
            playerObject.GetComponent<Damage>().onHit();
            }
        }
        Destroy(this.gameObject);
    }

    public void setSpeed(float s){
        speed = s;
    }

    public float getSpeed(){
        return speed;
    }

    public void setAngle(Vector3 a){
        angle = a;
    }
}
