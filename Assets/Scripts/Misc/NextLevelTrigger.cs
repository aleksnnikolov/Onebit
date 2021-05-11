using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    GameManager manager;

    private void Awake() {
        manager = FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        manager.LoadNextScene();
    }
}
