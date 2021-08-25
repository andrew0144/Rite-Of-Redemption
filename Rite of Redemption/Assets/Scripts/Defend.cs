using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The player's melee attack script
public class Defend : MonoBehaviour
{
    //An array of all objects with the enemy projectile tag
    private GameObject[] enemyProjectiles;
    
    //The angle of the reflected projectile
    private Vector3 angle;

    //The serialized field of the projectiles the player will fire
    [SerializeField] private GameObject projectilePrefab;
    
    //The projectiles the player will fire
    private GameObject projectile;

    //A representation of the player object
    private GameObject playerObject;

    //
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reflect(Vector2 v, GameObject oldLoc){
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.eulerAngles = new Vector3(0,0, (270+(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg)));
        projectile.transform.position = oldLoc.transform.position;
        projectile.GetComponent<Projectile>().setSpeed(-projectile.GetComponent<Projectile>().getSpeed());
    }

    public void reflect(Vector3 angle, GameObject oldLoc){
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.eulerAngles = new Vector3(angle.x, angle.y, angle.z-180);
        projectile.transform.position = oldLoc.transform.position;
    }
}
