using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecCompPM : PowerManager
{

    Animator anim;
    float animSpeed;

    void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        animSpeed = Random.Range(0.3f, 0.7f);
        anim.speed = animSpeed;
    }

    public void OnTriggerEnter2D(Collider2D col) {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura")) {
            bitScript.AddPoweredUnit(gameObject);
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

    public void TurnOn()
    {
        base.TurnOn();
        anim.SetBool("isPowered", true);
    }

    public void TurnOff()
    {
        base.TurnOff();
        anim.SetBool("isPowered", false);
    }

}
