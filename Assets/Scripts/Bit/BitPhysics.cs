using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitPhysics : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rb;

    public float passiveThrowIntensity;
    public float activeThrowIntensity;
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
                audioManager.Play("bitLand", 0);
            }
        }
    }

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Grounded = isGrounded();
    }

    //Small passive throw when bit is dropped with E
    public void PassiveThrow(Vector2 direction) {
        rb.velocity = Vector2.zero;
        direction.x = Mathf.Clamp(direction.x, -1.0f, 1.0f);
        direction.y = Mathf.Clamp(direction.y, -0.25f, 0.75f);

        Vector2 force = direction * passiveThrowIntensity;
        rb.AddForce(force);
    }

    //Active throw with mouse click that boosts vertical force component
    public void ActiveThrow(Vector2 direction) {

        Vector2 dir = direction;
        rb.velocity = Vector2.zero;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float mag = dir.magnitude;

        mag = Mathf.Clamp(mag, -4.0f, 4.0f);
        mag = Map(1, 4, 10f, 20f, mag);
        if (angle > 165 || (angle >= -180 && angle <= -90))
            angle = 165;
        else if (angle < 15 ||(angle <= 0 && angle > -90))
            angle = 15;

        dir.x = mag * Mathf.Cos(angle * Mathf.Deg2Rad);
        dir.y = mag * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector2 force = dir * activeThrowIntensity;
        rb.AddForce(force);
    }

    public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public bool isGrounded()
    {
        bool grounded = true;

        Vector2 origin1 = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.01f);
        Vector2 origin2 = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.01f);

        RaycastHit2D hit1 = Physics2D.Raycast(origin1, Vector2.down, 0.02f, collisionMask);
        RaycastHit2D hit2 = Physics2D.Raycast(origin2, Vector2.down, 0.02f, collisionMask);

        if (hit1 || hit2)
            grounded = true;
        else
            grounded = false;

        return grounded;
    }
}
