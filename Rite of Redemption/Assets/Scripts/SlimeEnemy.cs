using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple enemy script
public class SlimeEnemy : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject controller;

    //The main camera
    private Camera cam;

    //The amount the camera shakes upon hitting an enemy
    private float shakeOnHitAmount = 3f;

    private float maxDuplications = 10;

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
    public void onHit()
    {
        AudioManager.instance.Play("EnemyDeathSound");
        cam.GetComponent<Follow>().setShake(shakeOnHitAmount);
        Destroy(this.gameObject);
    }

    public void onStun()
    {

    }

    public void duplicate()
    {
        StartCoroutine(helpDuplicate());
    }

    public IEnumerator helpDuplicate()
    {
        if(maxDuplications > 0)
        {
            this.gameObject.GetComponent<SlimeEnemyMovement>().stun();
            yield return new WaitForSeconds(1f);
            GameObject firstEnemy = Instantiate(enemy) as GameObject;
            GameObject secondEnemy = Instantiate(enemy) as GameObject;
            if(controller.GetComponent<DoorOnEnemyDead>() != null) {
            controller.GetComponent<DoorOnEnemyDead>().AddEnemy(firstEnemy);
            controller.GetComponent<DoorOnEnemyDead>().AddEnemy(secondEnemy);
            } else if(controller.GetComponent<Waves>() != null) {
                controller.GetComponent<Waves>().AddEnemy(firstEnemy);
                controller.GetComponent<Waves>().AddEnemy(secondEnemy);
            }
            maxDuplications--;
            firstEnemy.gameObject.GetComponent<SlimeEnemy>().setDuplications(maxDuplications-1);
            secondEnemy.gameObject.GetComponent<SlimeEnemy>().setDuplications(maxDuplications-1);
            Vector3 parentSlimePos = this.gameObject.transform.position;
            firstEnemy.transform.position = new Vector3(parentSlimePos.x - 0.5f, parentSlimePos.y, parentSlimePos.z);
            secondEnemy.transform.position = new Vector3(parentSlimePos.x + 0.5f, parentSlimePos.y, parentSlimePos.z);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //firstEnemy.gameObject.GetComponent<SlimeEnemyMovement>().changeSpeed(1.2f);
        //secondEnemy.gameObject.GetComponent<SlimeEnemyMovement>().changeSpeed(1.2f);
        //firstEnemy.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //secondEnemy.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && col.name == "Player")
        {
            playerObject.GetComponent<Damage>().onHit();
        }
    }

    public void setDuplications(float newDup)
    {
        maxDuplications = newDup;
    }
}

