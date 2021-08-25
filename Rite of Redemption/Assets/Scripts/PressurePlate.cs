using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script for a door-pressure plate combo
public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject door;
    //A represenation of the player object
    private GameObject playerObject; 

    //The main camera
    private Camera cam;

    //The amount the camera shakes upon hitting an enemy
    private float shakeOnPressAmount = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deals damage to the player if the two make contact
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.Equals(playerObject)){
            if(door.gameObject.activeSelf){
                cam.GetComponent<Follow>().setShake(shakeOnPressAmount);
                AudioManager.instance.Play("WallOpen");
            }           
            door.gameObject.SetActive(false);
            
        }
    }
}
