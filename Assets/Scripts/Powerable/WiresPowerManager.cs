using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresPowerManager : PowerManager
{

    public Color offColor;
    public Color onColor;
    public float onTime;
    public float offTime;

    bool crRunning;
    SpriteRenderer sr;

    public void Awake()
    {
        base.Awake();

        sr = GetComponent<SpriteRenderer>();
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

        if (sr != null)
        {
            if (crRunning)
                StopCoroutine("ChangeColor");
            StartCoroutine(ChangeColor(sr.color, onColor, onTime));
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Wire")
                transform.GetChild(i).GetComponent<WiresPowerManager>().TurnOn();
            else
                transform.GetChild(i).GetComponent<ElecCompPM>().TurnOn();
        }
    }

    public void TurnOff()
    {
        base.TurnOff();

        if (sr != null)
        {
            if (crRunning)
                StopCoroutine("ChangeColor");
            StartCoroutine(ChangeColor(sr.color, offColor, offTime));
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag =="Wire")
                transform.GetChild(i).GetComponent<WiresPowerManager>().TurnOff();
            else
                transform.GetChild(i).GetComponent<ElecCompPM>().TurnOff();
        }
    }

    IEnumerator ChangeColor(Color currentColor, Color targetColor, float overTime)
    {
        crRunning = true;

        float startTime = Time.time;

        while (Time.time < startTime + overTime)
        {
            sr.color = Color.Lerp(currentColor, targetColor, (Time.time - startTime) / overTime);
            yield return null;
        }

        crRunning = false;
    }
}
