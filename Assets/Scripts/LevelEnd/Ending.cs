using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    GameManager gameManager;
    public GameObject crossfadeImage;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameManager);
        crossfadeImage = GameObject.Find("CrossfadeImage");
        crossfadeImage.GetComponent<Animator>().SetTrigger("End");
    }

}
