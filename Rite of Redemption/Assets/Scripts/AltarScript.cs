using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the altar, where the player leaves behind their items
public class AltarScript : MonoBehaviour
{
    private float radius = 1.6f;
    private int itemsInPlay = 3;
    private int menuPos = 0;
    private float menuInc = 0.6f;
    private GameObject ItemMenuBackdrop;
    private GameObject altarSword;
    private GameObject altarShield;
    private GameObject altarFire;
    private bool hasItem = false;
    private GameObject menuCursor;
    [SerializeField] private GameObject door;
    private Camera cam;
    private float shakeOnPressAmount = 5f;
    //A represenation of the player object
    private GameObject playerObject; 

    //The ID of the item the player will remove from their inventory
    private int itemID = 0; 

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        ItemMenuBackdrop = this.transform.Find("ItemMenuBackdrop").gameObject;
        menuCursor = ItemMenuBackdrop.transform.Find("menu-cursor").gameObject;
        GameObject altar = GameObject.Find("Altar");
        altarSword = altar.transform.Find("altarSword").gameObject;
        altarShield = altar.transform.Find("altarShield").gameObject;
        altarFire = altar.transform.Find("altarFire").gameObject;
        itemsInPlay = PlayerPrefs.GetInt("itemsInPlay", 3);
        cam = Camera.main;
        if(itemsInPlay == 3) {
            menuCursor.transform.localPosition = new Vector3(-0.1666667f, 0.3000001f, 0);
        } else if(itemsInPlay == 2) {
            menuCursor.transform.localPosition = new Vector3(-0.22f, 0.235f, 0);
            menuInc = 0.95f;
        } else {
            menuCursor.transform.localPosition = new Vector3(-.25f, 0, 0);
            menuInc = 0.95f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && menuPos != 0 && ItemMenuBackdrop.activeSelf){
            menuCursor.transform.position = new Vector3(menuCursor.transform.position.x, menuCursor.transform.position.y + menuInc, menuCursor.transform.position.z);
            itemID--;
            int count = 0;
             while(!playerObject.GetComponent<Inventory>().inInventory(itemID)) {
                itemID--;
                count++;
                if(itemID < 0) {
                    itemID = itemsInPlay - 1;
                }
                if(count == 10) {
                    break;
                }
            }
            menuPos--;
        }
        if((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && menuPos < (itemsInPlay - 1) && ItemMenuBackdrop.activeSelf) {
            menuCursor.transform.position = new Vector3(menuCursor.transform.position.x, menuCursor.transform.position.y - menuInc, menuCursor.transform.position.z);
            itemID++;
            int count = 0;
            while(!playerObject.GetComponent<Inventory>().inInventory(itemID)) {
                itemID = (itemID+1)%3;
                count++;
                if(count == 10) {
                    break;
                }
            }
            menuPos++;
        }
        if((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) && ItemMenuBackdrop.activeSelf) {
            AudioManager.instance.Play("SacrificeItem");
            playerObject.GetComponent<Inventory>().removeItem(itemID);
            ItemMenuBackdrop.SetActive(false);
            hasItem = true;
            if(itemID == 0) {
                PlayerPrefs.SetInt("sword", 0);
                altarSword.SetActive(true);
            } else if(itemID == 1) {
                PlayerPrefs.SetInt("shield", 0);
                altarShield.SetActive(true);
            } else {
                PlayerPrefs.SetInt("wand", 0);
                altarFire.SetActive(true);
            }
            if(door.gameObject.activeSelf){
                cam.GetComponent<Follow>().setShake(shakeOnPressAmount);
            }
            PlayerPrefs.SetInt("itemsInPlay", itemsInPlay-1);
            door.gameObject.SetActive(false);
            playerObject.GetComponent<PlayerCharacter>().StartMovement();
        }
    }

    //Deals damage to the player if the two make contact
    public void RemoveItem(){
        if(Vector3.Distance(playerObject.transform.position, this.transform.position) < radius && !hasItem){
            playerObject.GetComponent<PlayerCharacter>().Stop();
            ItemMenuBackdrop.SetActive(true);
            while(!playerObject.GetComponent<Inventory>().inInventory(itemID) && itemID < 2) {
                itemID++;
            }
        }
    }

    public void AltarMenuSetup() {
        Inventory pi = playerObject.GetComponent<Inventory>();
        switch(itemsInPlay){
            case 0:
            break;

            case 1:
                if(pi.inInventory(0)) {
                    ItemMenuBackdrop.transform.Find("menuShield").gameObject.SetActive(false);
                    ItemMenuBackdrop.transform.Find("menuFire").gameObject.SetActive(false);
                    GameObject sword = ItemMenuBackdrop.transform.Find("menuSword").gameObject;
                    sword.transform.localPosition = new Vector3(0.39f, 0, 0);
                    sword.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                } else if(pi.inInventory(1)) {
                    ItemMenuBackdrop.transform.Find("menuSword").gameObject.SetActive(false);
                    ItemMenuBackdrop.transform.Find("menuFire").gameObject.SetActive(false);
                    GameObject shield = ItemMenuBackdrop.transform.Find("menuShield").gameObject;
                    shield.transform.localPosition = new Vector3(0.22f, 0, 0);
                    shield.transform.localScale = new Vector3(0.25f, 0.18f, 1);
                } else {
                    ItemMenuBackdrop.transform.Find("menuSword").gameObject.SetActive(false);
                    ItemMenuBackdrop.transform.Find("menuShield").gameObject.SetActive(false);
                    GameObject fire = ItemMenuBackdrop.transform.Find("menuFire").gameObject;
                    fire.transform.localPosition = new Vector3(0.22f, 0.049f, 0);
                    fire.transform.localScale = new Vector3(0.4f, 0.35f, 1);
                }
            break;

            case 2:
               if(pi.inInventory(0)) {
                    GameObject sword = ItemMenuBackdrop.transform.Find("menuSword").gameObject;
                    sword.transform.localPosition = new Vector3(0.39f, 0.2f, 0);
                    sword.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                    if(pi.inInventory(1)) {
                        ItemMenuBackdrop.transform.Find("menuFire").gameObject.SetActive(false);
                        GameObject shield = ItemMenuBackdrop.transform.Find("menuShield").gameObject;
                        shield.transform.localPosition = new Vector3(0.22f, -.23f, 0);
                        shield.transform.localScale = new Vector3(0.22f, 0.16f, 1);
                    } else {
                        ItemMenuBackdrop.transform.Find("menuShield").gameObject.SetActive(false);
                        GameObject fire = ItemMenuBackdrop.transform.Find("menuFire").gameObject;
                        fire.transform.localPosition = new Vector3(0.22f, -.2f, 0);
                        fire.transform.localScale = new Vector3(0.35f, 0.33f, 1);
                    }
               } else {
                   ItemMenuBackdrop.transform.Find("menuSword").gameObject.SetActive(false);
                    GameObject shield = ItemMenuBackdrop.transform.Find("menuShield").gameObject;
                    shield.transform.localPosition = new Vector3(0.22f, .23f, 0);
                    shield.transform.localScale = new Vector3(0.22f, 0.16f, 1);
                    GameObject fire = ItemMenuBackdrop.transform.Find("menuFire").gameObject;
                    fire.transform.localPosition = new Vector3(0.22f, -.2f, 0);
                    fire.transform.localScale = new Vector3(0.35f, 0.33f, 1);
               }
            break;

            case 3:
            break;
        }
    }
}
