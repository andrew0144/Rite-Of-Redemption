using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the player with WASD or arrow keys and allows them to attack and defend
//(SPACE for melee, Q for projectile, and E for shield)
public class PlayerCharacter : MonoBehaviour
{

    //The serialized field of the sword hitbox
    [SerializeField] private GameObject meleeAttackObject;
    
    //The serialized field of the shield hitbox
    [SerializeField] private GameObject shieldObject;
    
    //The serialized field of the projectiles the player will fire
    [SerializeField] private GameObject projectilePrefab;

    private GameObject altar;
    
    //The projectiles the player will fire
    private GameObject projectile;
    
    //The sword hitbox
    private GameObject meleeAttack;
    
    //The shield hitbox
    private GameObject shield;
    
    //A represenation of the player hitbox
    private GameObject playerObject;

    //The player's animator component
    private Animator animator;
   
    //The player's speed
    private float speed = 4.0f;

    //The player's walking speed while attacking
    private float attackingSpeed = 2.0f;

    //The distance the player will back up by when taking damage
    private float backUpDist = 1f;

    //A unit vector representing the direction the player is visually facing
    private Vector2 facingVector;

    //A float to keep track of the last time the player fired a projectile
    private float oldTime;
    
    //The projectile cooldown time
    private float cooldownTime = 0.4f;

    //How long the shield will stay active
    private float defendTime = 0.8f;

    //The projectile cooldown time
    private float shieldCooldown = 1.15f;

    //How long the shield will stay active
    private float oldShieldTime;
    //How long the sword hitbox will stay active
    private float meleeTime = 0.4f;

    //A boolean to keep track of the player's shield
    private bool defending;

    //A boolean to keep track of the player's sword
    private bool attacking;

    //A boolean to keep track of the player's ability to move
    private bool stopped;

    //A boolean to keep track of the player's ability to take damage
    private bool invincible = false;

    void Start()
    {
        stopped = false;
        playerObject = GameObject.Find("Player");
        altar = GameObject.Find("Altar");
        animator = this.GetComponent<Animator>();
        facingVector = new Vector2(0.0f, -1.0f);
        meleeAttack = meleeAttackObject;
        shield = shieldObject;
        oldTime = -cooldownTime;
        oldShieldTime = -shieldCooldown;
        defending = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopped){
            // The encoding for the animator Direction parameter is:
            // 0 = down     1 = right     2 = up     3 = left

            // I redesigned this movement code to avoid rotating the player visually.

            /*
            if((Input.GetKey("w") && Input.GetKey("d") ) || (Input.GetKey("up") && Input.GetKey("right"))){
                this.transform.eulerAngles = new Vector3(0, 0, 315);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 1);
            }
            else if((Input.GetKey("s") && Input.GetKey("d") ) || (Input.GetKey("down") && Input.GetKey("right"))){
                this.transform.eulerAngles = new Vector3(0, 0, 225);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 1);
            }
            else if((Input.GetKey("w") && Input.GetKey("a") ) || (Input.GetKey("up") && Input.GetKey("left"))){
                this.transform.eulerAngles = new Vector3(0, 0, 45);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 3);
            }
            else if((Input.GetKey("s") && Input.GetKey("a") ) || (Input.GetKey("down") && Input.GetKey("left"))){
                this.transform.eulerAngles = new Vector3(0, 0, 135);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 3);
            }
            else if(Input.GetKey("w") || Input.GetKey("up")){
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 2);
            }
            else if(Input.GetKey("s") || Input.GetKey("down")){
                this.transform.eulerAngles = new Vector3(0, 0, 180);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 0);
            }
            else if(Input.GetKey("d") || Input.GetKey("right")){
                this.transform.eulerAngles = new Vector3(0, 0, 270);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 1);
            }
            else if(Input.GetKey("a") || Input.GetKey("left")){
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                this.transform.Translate(new Vector2(0, speed*Time.deltaTime));
                animator.SetInteger("Direction", 3);
            }
            */

            //Default movement is 0
            int xx = 0;
            int yy = 0;

            //Translate inputs to movement components
            if (Input.GetKey("a") || Input.GetKey("left")) { xx -= 1; }
            if (Input.GetKey("d") || Input.GetKey("right")) { xx += 1; }
            if (Input.GetKey("w") || Input.GetKey("up")) { yy += 1; }
            if (Input.GetKey("s") || Input.GetKey("down")) { yy -= 1; }

            //Choose animation parameters based on movement components
            if (xx != 0)
            {
                animator.SetBool("Walking", true);
                if (xx > 0) { 
                    animator.SetInteger("Direction", 1); 
                    facingVector = new Vector2(1.0f, 0.0f); 
                }
                else { 
                    animator.SetInteger("Direction", 3);
                    facingVector = new Vector2(-1.0f, 0.0f);
                }
            } else if (yy != 0)
            {
                animator.SetBool("Walking", true);
                if (yy > 0) { 
                    animator.SetInteger("Direction", 2);
                    facingVector = new Vector2(0.0f, 1.0f);
                }
                else { 
                    animator.SetInteger("Direction", 0);
                    facingVector = new Vector2(0.0f, -1.0f);
                }
            } else
            {
                animator.SetBool("Walking", false);
            }

            //Move player
            if (!attacking) { this.transform.Translate(new Vector2(speed * Time.deltaTime * xx, speed * Time.deltaTime * yy)); }
            else { this.transform.Translate(new Vector2(attackingSpeed * Time.deltaTime * xx, attackingSpeed * Time.deltaTime * yy)); }

            if (Input.GetKeyDown("space") && !defending && !attacking && playerObject.GetComponent<Inventory>().hasSword()){
                StartCoroutine(melee());
            }

            //Fires a projectile if the player has not fired anything within cooldown time
            if((Input.GetKeyDown("o") || Input.GetKeyDown("q")) && !defending && playerObject.GetComponent<Inventory>().hasWand()){
                if(oldTime + cooldownTime <= Time.time){
                    AudioManager.instance.Play("SpellSound");
                    projectile = Instantiate(projectilePrefab) as GameObject;
                    projectile.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(facingVector.y, facingVector.x) * Mathf.Rad2Deg) - 90);
                    setPosition(projectile);
                    oldTime = Time.time;
                }
            }

            if((Input.GetKeyDown("p") || Input.GetKeyDown("e")) && !attacking && playerObject.GetComponent<Inventory>().hasShield()){
                StartCoroutine(defend());               
            }

            if(Input.GetKeyDown("i") && !attacking && !defending) {
                altar = GameObject.Find("Altar");
                animator.SetBool("Walking", false);
                altar.GetComponent<AltarScript>().AltarMenuSetup();
                altar.GetComponent<AltarScript>().RemoveItem();
            }
        }
    }

    //Simple melee coroutine, creates a damage hitbox and makes the player unable to defend for a set length of time
    private IEnumerator melee(){
        AudioManager.instance.Play("MeleeSound");
        attacking = true;
        animator.SetTrigger("Attacking");
        meleeAttack.gameObject.transform.position = this.transform.position + new Vector3(facingVector.x, facingVector.y, 0.0f);
        meleeAttack.gameObject.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(facingVector.y, facingVector.x) * Mathf.Rad2Deg) + 90);
        meleeAttack.gameObject.SetActive(true);
        yield return new WaitForSeconds(meleeTime);
        meleeAttack.gameObject.SetActive(false);    
        attacking = false;
    }

    //Simple defending coroutine, makes the player unable to attack and immune from damage for a set length of time
    private IEnumerator defend(){
        if(oldShieldTime + shieldCooldown <= Time.time){
            oldShieldTime = Time.time;
            AudioManager.instance.Play("ShieldSound");
            defending = true;
            shield.gameObject.SetActive(true);
            yield return new WaitForSeconds(defendTime);
            shield.gameObject.SetActive(false);    
            defending = false;
        }
    }

    public void setInvincible(bool inv){
        invincible = inv;
    }
    public bool isInvincible(){
        return invincible;
    }

    public bool isDefending(){
        return defending;
    }

    public void Stop(){
        stopped = true;
    }

    public void StartMovement() {
        stopped = false;
    }

    //Makes the player back up when they take damage
    public void BackUp(){
        this.transform.Translate(facingVector * - backUpDist);
    }

    //Sets the position of the projectile so it spawns in front of the player
    private void setPosition(GameObject weapon){
        /*
        if(this.transform.eulerAngles.z == 0){
            weapon.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+1f);
        }
        else if(this.transform.eulerAngles.z == 90){
            weapon.transform.position = new Vector2(this.transform.position.x-1f, this.transform.position.y);
        }
        else if(this.transform.eulerAngles.z == 180){
            weapon.transform.position = new Vector2(this.transform.position.x, this.transform.position.y-1f);
        }
        else if(this.transform.eulerAngles.z == 270){
            weapon.transform.position = new Vector2(this.transform.position.x+1f, this.transform.position.y);
        }
        */
        weapon.transform.position = this.transform.position + new Vector3(facingVector.x, facingVector.y, 0.0f);
    }
}
