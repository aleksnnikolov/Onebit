using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
    //Attach to a child of a platform to make a crawler or bit a child of a platform upon trigger
    //This works even with the rigidbody constraints on the crawlers

    Rigidbody2D rb;

    private void Awake() {
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        col.transform.parent = transform;
    }

    public void OnTriggerExit2D(Collider2D col) {
        col.transform.parent = null;
    }
}
