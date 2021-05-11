using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPM : PowerManager {

    AudioManager audioManager;
    Animator anim;
    float animSpeed;

    public int tutorialNumber;

    void Awake() {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        animSpeed = Random.Range(0.3f, 0.7f);
        anim.speed = animSpeed;
    }

    public void OnTriggerEnter2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.AddPoweredUnit(gameObject);
            audioManager.Play("checkpoint", 0);
            TurnOn();
        }

    }

    public void OnTriggerExit2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.RemovePoweredUnit(gameObject);
            TurnOff();
        }
    }

    public void TurnOn() {
        base.TurnOn();
        anim.SetBool("isPowered", true);
        Invoke("WaitTurnOn", 0.5f);
    }

    public void TurnOff() {
        base.TurnOff();
        anim.SetBool("isPowered", false);
    }

    void WaitTurnOn() {
        if (isPowered) {
            anim.SetInteger("tutorialNumber", tutorialNumber);
        }
    }

}
