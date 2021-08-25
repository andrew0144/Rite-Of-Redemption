using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    //A represenation of the enemy object
    private GameObject playerObject;
    private float distance;
    [SerializeField] private float speed = 1.5f;

    //The serialized field of the sword object
    [SerializeField] private GameObject swordObject;
    private Quaternion defaultRotation;
    //The sword hitbox
    private GameObject sword;

    //How long the sword hitbox will stay active
    private float meleeTime = 1f;

    private bool isStunned = false;
    private bool attacking = false;
    private bool startCharge = false;
    private bool isCharged = false;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        sword = swordObject;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(playerObject.transform.position, transform.position);
        if (isStunned)
        {
            StartCoroutine(awaitUnStun());
        }
        else if (distance < 8.0f && !isStunned)
        {
            Vector3 delta = playerObject.transform.position - this.transform.position;
            this.transform.Translate(delta.normalized * speed * 2 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * speed * 2);
        }

        /*
        else if(!isStunned){
            if(!isGoingUp){
                transform.rotation = defaultRotation;
                transform.position -= transform.up * Time.deltaTime * speed;
            }
            else{
                transform.rotation = defaultRotation;
                transform.rotation *= Quaternion.Euler(0,180f,0);
                transform.position += transform.up * Time.deltaTime * speed;
                //Vector3 fwd = transform.TransformDirection(Vector3.forward);
            }
            //transform.LookAt(new Vector2(0, -1));
            //transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * speed * 2);
            //transform.position -= transform.up * Time.deltaTime * speed;
            //Vector3 fwd = transform.TransformDirection(Vector3.forward);
            /*
            if (Physics.Raycast(transform.position, -transform.up, 100)){
                StartCoroutine(turnAround());
                Debug.Log("Actually saw it");
            }*/
        //transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * speed);
        /*directionTranslation *= Time.deltaTime * speed;
        transform.Translate(directionTranslation);*//*
    }*/
    }

    private IEnumerator turnAround()
    {
        defaultRotation = Quaternion.Euler(-defaultRotation.eulerAngles);
        yield return new WaitForSeconds(4f);
    }
    //Simple melee coroutine, creates a damage hitbox and makes the player unable to defend for a set length of time
    private IEnumerator melee()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
        sword.gameObject.SetActive(true);
        yield return new WaitForSeconds(meleeTime);
        attacking = false;
    }

    public void stun()
    {
        isStunned = true;
        //this.transform.Translate(new Vector2(0, 1f));
    }

    private IEnumerator awaitUnStun()
    {
        yield return new WaitForSeconds(1.0f);
        isStunned = false;
    }

    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }


    //Deals damage to the player if the two make contact
    private void OnTriggerStay2D(Collider2D col)
    {
        if (playerObject != null && col != null)
        {
            if (playerObject.Equals(col.gameObject))
            {
                playerObject.GetComponent<Damage>().onHit();
            }
        }
    }

    public void changeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}