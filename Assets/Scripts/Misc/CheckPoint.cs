using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    GameManager manager;
    AudioManager audioManager;

    public bool activated = false;
    public static GameObject[] checkPointsList;

    bool bitIn = false;
    bool playerIn = false;

    void Awake() {
        checkPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
        manager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void ActivateCheckPoint() {
        if (!manager.hasActivatedCheckpoint)
            manager.hasActivatedCheckpoint = true;

        foreach (GameObject cp in checkPointsList) {
            cp.GetComponent<CheckPoint>().activated = false;
            cp.GetComponent<Animator>().SetBool("isActivated", false);
        }

        activated = true;
        if (gameObject.name != "invisibleCheckpoint")
            audioManager.Play("checkpoint", 0.1f);
        manager.InitiateGameSave();
        GetComponent<Animator>().SetBool("isActivated", true);
    }

    /*
    public static Vector3 GetActiveCheckpointPosition() {
        Vector3 pos = levelOrigin;

        if (checkPointsList != null) {
            foreach (GameObject cp in checkPointsList) {
                if (cp.GetComponent<CheckPoint>().activated) {
                    pos = cp.transform.position;
                    break;
                }
            }
        }

        return pos;
    }
    */

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Bit") {
            bitIn = true;
        }   

        if (col.gameObject.tag == "Player") {
            playerIn = true;
        }

        if (bitIn && playerIn && !activated)
            ActivateCheckPoint();
    }
}
