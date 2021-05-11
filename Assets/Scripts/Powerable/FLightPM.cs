using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLightPM : PowerManager
{
    Animator anim;

    void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura"))
        {
            bitScript.AddPoweredUnit(gameObject);
            TurnOn();
        }

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura"))
        {
            bitScript.RemovePoweredUnit(gameObject);
            TurnOff();
        }
    }

    public void TurnOn()
    {
        base.TurnOn();
        anim.SetBool("isPowered", isPowered);
    }

    public void TurnOff()
    {
        base.TurnOff();
        anim.SetBool("isPowered", isPowered);
    }
}
