using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the camera to follow the player
public class Follow : MonoBehaviour
{
    //Size of shake
    [SerializeField] private float shakeScale = 0.3f;

    //Decay rate of shake
    [SerializeField] private float shakeDecay = 0.1f;

    //Amount of shake
    [SerializeField] private  float shakeAmount = 0f;

    //A represenation of the player object
    private GameObject playerObject;

    private bool follow = true;

    // The tracking position of the camera
    private Vector2 trackPos;

    // Smoothness of the camera follow. Must be on the range (0, 1). greater = smoother
    private float smoothness = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        trackPos = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -10 );
        Vector2 targetPos = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        trackPos = (smoothness * trackPos) + ((1 - smoothness) * targetPos);
        if(shakeAmount > 0){
            shakeAmount -= shakeDecay;
            Vector2 shakeOffset = Random.insideUnitCircle * shakeScale * shakeAmount;
            this.transform.position = new Vector3(trackPos.x + shakeOffset.x, trackPos.y + shakeOffset.y, -10 );
        }
        else{
            shakeAmount = 0;
            if(follow){
                this.transform.position = new Vector3(trackPos.x, trackPos.y, -10);
            }
        }
    }

    public void setShake(float shake){
        shakeAmount = shake;
    }

    public void isFollowing(bool f){
        follow = f;
    }
}
