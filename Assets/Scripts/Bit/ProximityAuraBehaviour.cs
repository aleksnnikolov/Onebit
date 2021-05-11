using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAuraBehaviour : MonoBehaviour
{
    BitBehaviour bitScript;
    CircleCollider2D circle;

    // Start is called before the first frame update
    void Awake()
    {
        bitScript = GetComponentInParent<BitBehaviour>();
        circle = GetComponent<CircleCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        GameObject unit = col.gameObject;
        bitScript.AddCloseUnit(unit);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        GameObject unit = col.gameObject;
        bitScript.RemoveCloseUnit(unit);
    }
}
