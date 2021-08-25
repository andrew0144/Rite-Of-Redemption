using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOnEnemyDead : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject door;
    private Camera cam;

    //The amount the camera shakes upon hitting an enemy
    private float shakeOnPressAmount = 5f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        bool enemiesDead = true;
        for(int i = 0; i < _enemies.Count; i++) {
			if (_enemies[i] != null) {
                enemiesDead = false;
            }
        }
        if(enemiesDead) {
            if(door.gameObject.activeSelf) {
                cam.GetComponent<Follow>().setShake(shakeOnPressAmount);
                AudioManager.instance.Play("WallOpen");
                door.gameObject.SetActive(false);
            }
        }
    }

    public void AddEnemy(GameObject enemy) {
        _enemies.Add(enemy);
    }
}
