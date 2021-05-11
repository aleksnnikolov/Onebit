using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExtraElevator : MonoBehaviour
{
    
    public float velocity = 0;
    public float maxVelocity;

    Rigidbody2D rb;

    public bool ascending = false;

    void Awake() {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update() {
        if (ascending)
            transform.Translate(Vector2.up * maxVelocity * Time.deltaTime);
    }

}
