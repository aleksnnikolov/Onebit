using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    AudioManager audioManager;

    public GameObject[] poweredDoors;
    public GameObject[] paths;

    Animator buttonAnim;
    Animator doorAnim;

    int objectsInTrigger = 0;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        buttonAnim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        objectsInTrigger++;

        if (objectsInTrigger == 1) {

            buttonAnim.SetBool("isPressed", true);
            foreach (GameObject door in poweredDoors) {

                if (door.gameObject.tag == "CrawlerBlock") {
                    door.transform.Find("source").gameObject.SetActive(false);
                    door.GetComponent<CrawlerBlockPM>().TurnOff();
                } else {
                    door.GetComponent<DoorPM>().TurnOn();
                }
            }

            audioManager.Play("doorOpen", 0);

            foreach (GameObject path in paths) {
                path.GetComponent<Animator>().Play("turn_path_off");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        objectsInTrigger--;

        if (objectsInTrigger <= 0) {

            buttonAnim.SetBool("isPressed", false);
            foreach (GameObject door in poweredDoors) {

                if (door.gameObject.tag == "CrawlerBlock") {
                    door.transform.Find("source").gameObject.SetActive(true);
                    door.GetComponent<CrawlerBlockPM>().TurnOn();
                } else {
                    door.GetComponent<DoorPM>().TurnOff();
                }
            }

            audioManager.Play("doorClose", 0);

            foreach (GameObject path in paths) {
                path.GetComponent<Animator>().Play("turn_path_on");
            }

          
        }
    }
}
