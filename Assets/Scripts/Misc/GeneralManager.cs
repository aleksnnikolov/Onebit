using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    AudioManager audioManager;

    public static Settings settingsMenu;
    public static float musicVolume = -10f;
    public static float soundVolume = -10f;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        SetVolumes();

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            audioManager.Play("menuBgMusic", 0f);
    }

    public static void SetVolumes() {
        settingsMenu = FindObjectOfType<Settings>();
        settingsMenu.SetVolumes(musicVolume, soundVolume);
    }


}
