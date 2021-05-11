using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAura : MonoBehaviour {
    BitBehaviour bitScript;
    CircleCollider2D circle;

    // Start is called before the first frame update
    void Start() {

        bitScript = GetComponentInParent<BitBehaviour>();
        circle = GetComponent<CircleCollider2D>();

    }

    public void OnTriggerEnter2D(Collider2D col) {

        GameObject unit = col.gameObject;
        bitScript.AddPoweredUnit(unit);

    }

    public void OnTriggerExit2D(Collider2D col) {

        GameObject unit = col.gameObject;
        bitScript.RemovePoweredUnit(unit);

    }
}
