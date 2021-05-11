using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject bit;
    BitBehaviour bitScript;

    public Vector3 offset;
    public float intermediateOffset;
    public float lerpSpeed;

    public Vector3 overridePos;
    public bool isObserver;

    Vector3 targetPos;
    Vector3 finalPos;

    void Awake()
    {
        overridePos = Vector3.zero;
        isObserver = false;
        bit = GameObject.Find("bit");
        bitScript = bit.GetComponent<BitBehaviour>();
    }

    void Update() {
        Vector3 bitPos = bit.transform.position;
        Vector3 unitPos;

        if (bitScript.controlledUnit != null)
            unitPos = bitScript.controlledUnit.transform.position;
        else
            unitPos = bitPos;
        if (overridePos == Vector3.zero) { 
            if (bitScript.isHolded && bitScript.controlledUnit != null)
                targetPos = bitScript.controlledUnit.transform.position;
            else
                targetPos = FindCameraPosition(bitPos, unitPos);
        } else {
            targetPos = overridePos;
        }
        
        finalPos = transform.position;
        
        Vector3 distance = targetPos - transform.position;

        //Calculate horizontal lerpSpeed based on  horizontal distance of camera from target
        float lerpSpeedX;
        lerpSpeedX = Mathf.Abs(distance.x);
        if (!isObserver)
            lerpSpeedX = Map(0, 6, 0.05f, 0.4f, lerpSpeedX);
        else
            lerpSpeedX = Map(0, 6, 0.05f, 0.1f, lerpSpeedX);
        finalPos.x = Mathf.Lerp(finalPos.x, targetPos.x, lerpSpeedX);

        //Calculate vertical lerpSpeed based on vertical distance of camera from target
        float lerpSpeedY;
        lerpSpeedY = Mathf.Abs(distance.y);
        if (!isObserver)
            lerpSpeedY = Map(0, 6, 0.05f, 0.4f, lerpSpeedY);
        else
            lerpSpeedY = Map(0, 6, 0.05f, 0.1f, lerpSpeedY);
        finalPos.y = Mathf.Lerp(finalPos.y, targetPos.y, lerpSpeedY);

        finalPos.z = targetPos.z;
    }

    //Sets cam position by adding an offset to the y and z axis
    private void FixedUpdate() {
        transform.position = finalPos + new Vector3(0, offset.y, -20f);
    }

    //if bit is not holded, aligns the camera to the middle position between bit and controlled unit
    Vector3 FindCameraPosition(Vector3 bitPos, Vector3 unitPos) {
        if (bitPos.Equals(unitPos))
            return bitPos;

        Vector3 targetPos;

        float targetX = bitPos.x + (unitPos.x - bitPos.x) / 2;
        float targetY = bitPos.y + (unitPos.y - bitPos.y) / 2;

        targetPos = new Vector3(targetX, targetY, 0);

        return targetPos;
    }

    public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void ChangeMode() {
        isObserver = !isObserver;
    }


}
