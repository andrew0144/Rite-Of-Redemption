using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The projectile script
public class ShieldProjectile : MonoBehaviour
{
    //The speed of the bullets
    private float speed = 8.0f;
    
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
        this.transform.Translate(new Vector3(0, speed*Time.deltaTime, 0));
    }

    //Finds the enemy the projectile has hit and deals damage to it
    private void OnTriggerEnter2D(Collider2D col){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            if(enemy.gameObject.Equals(col.gameObject)){
                if ((enemy.gameObject.GetComponent("SlimeEnemy") as SlimeEnemy) != null)
                {
                    col.gameObject.GetComponent<SlimeEnemy>().onStun();
                }
                else{
                    col.gameObject.GetComponent<Enemy>().onStun();
                }
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
}
