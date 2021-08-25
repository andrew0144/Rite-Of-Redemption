using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private List<GameObject> waves = new List<GameObject>();

    private List<GameObject> _enemies = new List<GameObject>();
    private Camera cam;
    public GameObject smokePuff;
    private int waveCount = 0;
    private Animator animator;

    private GameObject playerObject;

    //The amount the camera shakes upon hitting an enemy
    private float shakeOnPressAmount = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        waveCount = PlayerPrefs.GetInt("waveCount", 0);
        playerObject = GameObject.Find("Player");       
        animator = playerObject.GetComponent<Animator>(); 
        waves[waveCount].gameObject.SetActive(true);
        foreach(AltarScript altar in waves[waveCount].GetComponentsInChildren<AltarScript>()){
                altar.gameObject.transform.Translate(0.5f, 72.75f, 0f, Space.World);
        }
        for(int i = 0; i < waves.Count; i++) {
            if(i != waveCount) {
                waves[i].gameObject.SetActive(false);
            }
        }      
        foreach(Enemy enemy in waves[waveCount].GetComponentsInChildren<Enemy>()){
            _enemies.Add(enemy.gameObject);
        }
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
            cam.GetComponent<Follow>().setShake(shakeOnPressAmount);
            waves[waveCount].gameObject.SetActive(false);
            waveCount++;
            PlayerPrefs.SetInt("waveCount", waveCount);
            waves[waveCount].gameObject.SetActive(true);
            foreach(Enemy enemy in waves[waveCount].GetComponentsInChildren<Enemy>()){
                _enemies.Add(enemy.gameObject);
            }
            StartCoroutine(SpawnWave());
            enemiesDead = false;
            foreach(AltarScript altar in waves[waveCount].GetComponentsInChildren<AltarScript>()){
                altar.gameObject.transform.Translate(0.5f, 72.75f, 0f, Space.World);
            }
        }
    }
    IEnumerator SpawnWave(){
        playerObject.GetComponent<PlayerCharacter>().Stop();
        animator.SetBool("Walking", false);
        playerObject.GetComponent<PlayerCharacter>().setInvincible(true);
        Transform[] wave = waves[waveCount].GetComponentsInChildren<Transform>();
        wave[0].gameObject.SetActive(true);
        for (int i = 1; i < wave.Length; i++) {
            wave[i].gameObject.SetActive(false);
        }
        wave[0] = wave[1];
        foreach (Transform tr in wave) {
            if(tr!=null){
                tr.gameObject.SetActive(true);
                GameObject smoke = Instantiate(smokePuff) as GameObject;
                smoke.transform.position = tr.position;
                yield return new WaitForSeconds (0.035f);
            }
        }
        playerObject.GetComponent<PlayerCharacter>().StartMovement();
        playerObject.GetComponent<PlayerCharacter>().setInvincible(false);
	}

    public void AddEnemy(GameObject enemy) {
        _enemies.Add(enemy);
    }
}
