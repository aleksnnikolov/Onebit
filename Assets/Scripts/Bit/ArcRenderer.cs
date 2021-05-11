using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArcRenderer : MonoBehaviour {

    public LineRenderer lr;

    public float velocity;
    public float angle;

    public int resolution;
    public float velocityMultiplier;

    float g;
    float radianAngle;

    public bool isRendering;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
        lr.enabled = false;
    }

    void Update() {
        if (isRendering && !GameManager.inputBlocked) {
            RenderArc();
        }
    }

    public void ToggleLine() {
        isRendering = !isRendering;
        if (!lr.enabled)
            lr.enabled = true;
        else
            lr.enabled = false;
    }

    public void Activate() {
        isRendering = true;
        lr.enabled = true;
    }

    public void Deactivate() {
        isRendering = false;
        lr.enabled = false;
    }

    void RenderArc() {
        lr.positionCount = resolution + 1;
        CalculateForce();
        if (angle > 15f && angle < 165f)
            lr.SetPositions(CalculateArcArray());
    }

    void CalculateForce() {
        Vector3 throwDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 dir2D = new Vector2(throwDir.x, throwDir.y);
        float tempAngle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
        angle = tempAngle;

        float mag = dir2D.magnitude;

        mag = Mathf.Clamp(mag, -4.0f, 4.0f);
        mag = Map(1, 4, 10f, 20f, mag);
        if (angle > 165 || (angle >= -180 && angle <= -90))
            angle = 165;
        else if (angle < 15 || (angle <= 0 && angle > -90))
            angle = 15;

        dir2D.x = mag * Mathf.Cos(angle * Mathf.Deg2Rad);
        dir2D.y = mag * Mathf.Sin(angle * Mathf.Deg2Rad);

        velocity = dir2D.magnitude * velocityMultiplier;
    }

    Vector3[] CalculateArcArray() {

        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance =(velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++) {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;

    }

    Vector3 CalculateArcPoint(float t, float maxDistance) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }

    public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}