using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBlockPM : PowerManager
{
    AudioManager audioManager;
    Animator anim;

    public void Awake() {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.AddPoweredUnit(gameObject);
            //audioManager.Play("doorOpen", 0);
            TurnOn();
        }

    }

    public void OnTriggerExit2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.RemovePoweredUnit(gameObject);
            //audioManager.Play("doorClose", 0);
            TurnOff();
        }
    }

    public void TurnOn() {
        base.TurnOn();
        
        anim.SetBool("isPowered", isPowered);
    }

    public void TurnOff() {
        base.TurnOff();
        
        anim.SetBool("isPowered", isPowered);
    }
}
