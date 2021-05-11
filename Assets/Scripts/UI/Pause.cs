using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    AudioManager audioManager;

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public static bool isPaused;
    public static bool canPause = true;

    public string currentMenu = "none";

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause) {
            if (isPaused && currentMenu == "pause")
                ResumeGame();
            else if (isPaused && (currentMenu == "settings" || currentMenu == "confirm"))
                return;
            else {
                audioManager.StopAll();
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        currentMenu = "none";
        GameManager.inputBlocked = false;
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        currentMenu = "pause";
        GameManager.inputBlocked = true;
    }

    public void GoToPause() {
        currentMenu = "pause";
    }

    public void GoToSettings() {
        currentMenu = "settings";
    }

    public void GoToConfirm() {
        currentMenu = "confirm";
    }

    public void ExitGame() {
        Application.Quit();
    }

}
