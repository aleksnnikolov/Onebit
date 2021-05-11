using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject crossfadeImage;
    AudioManager audioManager;
    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayGame() {
        StartCoroutine(StartGame());
    }


    public void ExitGame() {
        Application.Quit();
    }

    IEnumerator StartGame() {
        crossfadeImage.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1.5f);
        audioManager.Stop("menuBgMusic");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
