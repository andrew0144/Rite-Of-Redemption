using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the altar, where the player leaves behind their items
public class Altar : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject; 

    //The ID of the item the player will remove from their inventory
    [SerializeField] private int itemID; 

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.Equals(playerObject)){
            playerObject.GetComponent<Inventory>().removeItem(itemID);
        }
    }
}
