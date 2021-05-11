using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPM : PowerManager
{
    AudioManager audioManager;
    Animator anim;

    float opened = 0f;

    public void Awake() {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.AddPoweredUnit(gameObject);
            audioManager.Play("doorOpen", 0);
            TurnOn();
        }

    }

    public void OnTriggerExit2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.RemovePoweredUnit(gameObject);
            audioManager.Play("doorClose", 0);
            TurnOff();
        }
    }

    public void TurnOn() {
        base.TurnOn();
        StopAllCoroutines();
        StartCoroutine(Open());
    }

    public void TurnOff() {
        base.TurnOff();
        StopAllCoroutines();
        StartCoroutine(Close());
    }


    IEnumerator Open() {
        if (opened <= 0.05f)
            yield return new WaitForSecondsRealtime(0.2f);

        while(opened < 1f) {
            opened += Time.deltaTime * 2;
            anim.SetFloat("Opened", opened);
            yield return null;
        }
    }

    IEnumerator Close() {
        while (opened > 0f) {
            opened -= Time.deltaTime * 2;
            anim.SetFloat("Opened", opened);
            yield return null;
        }
    }
}
