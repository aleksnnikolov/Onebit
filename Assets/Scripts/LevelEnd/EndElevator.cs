using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndElevator : MonoBehaviour
{

    GameManager manager;

    int crawlersIn;
    public bool bitIn;

    public float maxVelocity;
    float velocity = 0;

    public GameObject elevator;
    public GameObject extraElevator;
    GameObject collider;
    ExtraElevator extraScript;

    public bool ascending;

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        crawlersIn = 0;
        bitIn = false;
        collider = transform.GetChild(1).gameObject;
    }

    
    void Update()
    {
        if (crawlersIn == 1 && bitIn) {
            crawlersIn = 0;
            bitIn = false;
            ascending = true;
            collider.GetComponent<BoxCollider2D>().enabled = true;
            if (extraElevator != null) {
                extraElevator.GetComponent<ExtraElevator>().ascending = true;
                extraElevator.GetComponent<ManageExtraCrawlers>().SendExtraCrawlers();
            }
        }

        if (ascending)
            elevator.transform.Translate(Vector2.up * maxVelocity * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
            crawlersIn++;

        if (col.gameObject.tag == "Bit")
            bitIn = true;
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
            crawlersIn--;

        if (col.gameObject.tag == "Bit")
            bitIn = false;
    }
}
