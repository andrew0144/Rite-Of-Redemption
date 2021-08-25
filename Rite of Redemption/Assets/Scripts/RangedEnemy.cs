using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedEnemy : MonoBehaviour
{
    //A represenation of the player object
    private GameObject playerObject;

    private BoxCollider2D teleportBoundary;
    private BoxCollider2D enemyCollider;
    private Vector3 enemyExtents;
    private float xRange;
    private float yRange;
    private Vector3 min;
    private Vector3 max;
    public float fireRate = 2.5f;
    private float distance;
    private float nextFire;
    private bool isTriggered;
    private GameObject parent;

    public GameObject particles_tele;

    // The bullet the ranged enemies will fire
    [SerializeField] private GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
        enemyExtents = enemyCollider.bounds.extents;
        isTriggered = false;
        parent = transform.parent.gameObject;
        teleportBoundary = parent.transform.GetChild(1).GetComponent<BoxCollider2D>();
        min = teleportBoundary.bounds.min;       
        max = teleportBoundary.bounds.max;
        playerObject = GameObject.Find("Player");              
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(playerObject.transform.position, transform.position);
        if(distance < 6.0f && isTriggered == false)
        {
            xRange = UnityEngine.Random.Range((min.x + enemyExtents.x), (max.x - enemyExtents.x));
            yRange = UnityEngine.Random.Range((min.y + enemyExtents.y), (max.y - enemyExtents.y));
            StartCoroutine(shootPlayer());
            isTriggered = true;
        }
    }

    private IEnumerator shootPlayer()
    {
        int shotsBeforeMove = UnityEngine.Random.Range(2, 4);
        for (int i = 0; i < shotsBeforeMove; i++){
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
        GameObject beforeParticles = Instantiate(particles_tele) as GameObject;
        beforeParticles.transform.position = transform.position;
        beforeParticles.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        transform.position = new Vector3(xRange, yRange, 1);
        GameObject afterParticles = Instantiate(particles_tele) as GameObject;
        afterParticles.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);
        isTriggered = false;
    }

}
