using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    //A represenation of the enemy object
    private GameObject playerObject;
    private float distance;
    [SerializeField] private float speed = 2f;

    //The serialized field of the sword object
    [SerializeField] private GameObject swordObject;
    private Quaternion defaultRotation;
    //The sword hitbox
    private GameObject sword;

    //How long the sword hitbox will stay active
    private float meleeTime = 1f;

    // The GameObject for the charge indicator arrow
    public GameObject chargeArrow;

    // The color of the charge indicator arrow
    private float arrowOpacity = 0.0f;

    // The rate at which the charge indicator arrow fades
    private float arrowFadeSpeed = 0.02f;

    private bool isStunned = false;
    private bool attacking = false;
    private bool startCharge = false;
    private bool isCharged = false;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        sword = swordObject;
        defaultRotation = transform.rotation;

        // The opacity of the charge indicator arrow can only be modified via its sprite's color setting
        //arrowColor = chargeArrow.GetComponent<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(playerObject.transform.position, transform.position);
        if (isStunned)
        {
            StartCoroutine(awaitUnStun());
        }
        else if (distance < 8.0f)
        {
            //distance = Vector2.Distance(playerObject.transform.position, transform.position);
            if (startCharge)
            {
                // Make the indicator arrow always point towards the player
                chargeArrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(playerObject.transform.position.y - transform.position.y, playerObject.transform.position.x - transform.position.x) * Mathf.Rad2Deg);

                if (isCharged)
                {
                    Vector3 delta = playerObject.transform.position - this.transform.position;
                    this.transform.Translate(delta.normalized * (speed * 2.5f) * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, playerObject.gameObject.transform.rotation, Time.deltaTime * speed * 2);
                }
            }
            else
            {
                StartCoroutine(chargeDash());
                StartCoroutine(chargeDashIndicator());
            }
            //transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
            //RotateTowards(playerObject.transform.position);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * speed * 2);
        }

        // Charge indicator arrow fade
        if (arrowOpacity > 0)
        {
            arrowOpacity -= arrowFadeSpeed;
        }
        // Reconstruct and set the arrow indictor RGBA color
        chargeArrow.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, arrowOpacity);
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

    private IEnumerator chargeDash()
    {
        startCharge = true;
        yield return new WaitForSeconds(1.5f);
        isCharged = true;
        yield return new WaitForSeconds(1.5f);
        isCharged = false;
        startCharge = false;
    }

    private IEnumerator chargeDashIndicator()
    {
        arrowOpacity = 1f;
        yield return new WaitForSeconds(0.5f);
        arrowOpacity = 1f;
        yield return new WaitForSeconds(0.5f);
        arrowOpacity = 1f;
        yield return null;
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
        if(playerObject != null && col != null)
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
