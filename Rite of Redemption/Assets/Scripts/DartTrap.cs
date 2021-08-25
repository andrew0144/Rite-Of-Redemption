using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [SerializeField] GameObject dartPrefab;
    private GameObject dart;

    //The firing cooldown time
    [SerializeField] private float cooldownTime = 0.4f;

    //A float to keep track of the last time the player fired a projectile
    private float oldTime;

    private Vector3 angle = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        oldTime = -cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldTime + cooldownTime <= Time.time){
            dart = Instantiate(dartPrefab) as GameObject;
            if(angle.z == 360f){
                angle.z = 0f;
            }
            dart.transform.position = this.transform.position;
            dart.transform.eulerAngles = angle;
            dart.GetComponent<Dart>().setAngle(angle);
            angle = new Vector3(angle.x, angle.y, angle.z+45f);
            oldTime = Time.time;
        }
    }
}
