using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptRestart : MonoBehaviour
{

    public GameObject text;

    public void OnTriggerEnter2D(Collider2D collision) {
        text.SetActive(true);
    }

}
