using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(CrawlerMovement))]
public class PlayerPM : PowerManager
{
    AudioManager audioManager;

    CrawlerOutline outlineScript;
    Animator anim;
    Rigidbody2D rb;
    CrawlerMovement movementScript;
    BoxCollider2D col;

    public bool isAlive = true;

    void Awake()
    {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        outlineScript = GetComponent<CrawlerOutline>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        movementScript = GetComponent<CrawlerMovement>();

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura"))
        {
            audioManager.Play("crawlerTurnOn", 0);
            bitScript.AddPoweredUnit(gameObject);
            bitScript.AddControllableUnit(gameObject);
            TurnOn();
            bitScript.UpdateControlledUnit();
        }

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        GameObject unit = col.gameObject;

        if (unit.tag.Equals("PowerAura"))
        {
            audioManager.Play("crawlerTurnOff", 0.3f);
            bitScript.RemovePoweredUnit(gameObject);
            bitScript.RemoveControllableUnit(gameObject);
            bitScript.Invoke("UpdateControlledUnit", 0.3f);
            TurnOff();
        }
    }

    public void TurnOn()
    {
        
        base.TurnOn();
        anim.SetBool("isPowered", isPowered);
        movementScript.Invoke("PowerToggle", 0.6f);
        outlineScript.IsPowered();
        
    }

    public void TurnOff()
    {
        base.TurnOff();
        anim.SetBool("isPowered", isPowered);
        movementScript.isPowered = false;
        IsNotControlled();
        outlineScript.IsNotPowered();
    }

    public void IsControlled()
    {
        movementScript.canMove = true;
        outlineScript.IsControlled();
    }

    public void IsNotControlled()
    {
        movementScript.canMove = false;
        outlineScript.IsNotControlled();
    }
}
