using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rb;

    public LayerMask collisionMask;
    public bool grounded;
    public bool Grounded {
        get {
            return grounded;
        }
        set {
            if (value == grounded)
                return;

            grounded = value;
            if (grounded) {
                audioManager.Play("crawlerLand", 0);
            }
        }
    }

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Grounded = isGrounded();

        if (Mathf.Abs(rb.velocity.x) > 0.1f && Mathf.Abs(rb.velocity.y) <= 0.1f) {
            audioManager.Play("boxPush", 0f);
        } else {
            audioManager.Stop("boxPush");
        }
    }

    private bool isGrounded() {
        bool grounded = true;

        Vector2 origin1 = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.05f);
        Vector2 origin2 = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.05f);

        //In case of issues with grounding, distance should be 0.1
        RaycastHit2D hit1 = Physics2D.Raycast(origin1, Vector2.down, 0.01f, collisionMask);
        RaycastHit2D hit2 = Physics2D.Raycast(origin2, Vector2.down, 0.01f, collisionMask);

        if (hit1 || hit2)
            grounded = true;
        else
            grounded = false;

        return grounded;
    }

}
