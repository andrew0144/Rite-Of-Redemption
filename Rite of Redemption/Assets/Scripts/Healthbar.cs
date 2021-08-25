using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    public GameObject[] hearts;

    public Sprite heart_full;
    public Sprite heart_empty;

    private int numHearts;

    // Start is called before the first frame update
    void Start()
    {
        numHearts = 5;
    }

    // Display an amount of full hearts equal to 'health'.
    public void displayHealth(int health)
    {
        numHearts = health;
        for (int i = 0; i < 5; i++)
        {
            if (i < numHearts)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = heart_full;
            } else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = heart_empty;
            }
        }
    }
}
