using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The player's inventory
public class Inventory : MonoBehaviour
{
    //A boolean that evaluates to true if the player holds the sword
    [SerializeField] private bool sword;

    //A boolean that evaluates to true if the player holds the shield
    [SerializeField] private bool shield;

    //A boolean that evaluates to true if the player holds the wand
    [SerializeField] private bool wand;

    void Start(){
        sword = intToBool(PlayerPrefs.GetInt("sword", 1));
        shield = intToBool(PlayerPrefs.GetInt("shield", 1));
        wand = intToBool(PlayerPrefs.GetInt("wand", 1));
    }

    //Removes items from the player's inventory based on the id of the item:
    //0 is the sword
    //1 is the shield
    //2 is the wand
    public void removeItem(int id)
    {
        switch(id){
            case 0:
                sword = false;
            break;

            case 1:
                shield = false;
            break;

            case 2:
                wand = false;
            break;
        }
    }

    public bool hasSword(){
        return sword;
    }

    public bool hasWand(){
        return wand;
    }

    public bool hasShield(){
        return shield;
    }
    public bool intToBool(int convert) {
        if(convert == 0) {
            return false;
        } else {
            return true;
        }
    }

    public bool inInventory(int id) {
        switch(id){
            case 0:
                return sword;
            break;

            case 1:
                return shield;
            break;

            case 2:
                return wand;
            break;
        }
        return false;
    }
}
