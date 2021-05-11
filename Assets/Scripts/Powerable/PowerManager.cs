using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{

    public bool isPowered;
    public BitBehaviour bitScript;

    public void Awake()
    {
        isPowered = false;
        bitScript = GameObject.Find("bit").GetComponent<BitBehaviour>();
    }

    public void TurnOn()
    {
        isPowered = true;
    }

    public void TurnOff()
    {
        isPowered = false;
        
    }

}
