using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public AudioMixer mixer;

    public GameObject musicSlider;
    public GameObject soundSlider;

    private void Start() {
        gameObject.SetActive(false);
        mixer.SetFloat("musicVolume", musicSlider.GetComponent<Slider>().value);
        mixer.SetFloat("soundVolume", soundSlider.GetComponent<Slider>().value);
    }

    public void SetMusicVolume(float volume) {
        mixer.SetFloat("musicVolume", volume);
        GeneralManager.musicVolume = volume;
    }

    public void SetSoundVolume(float volume) {
        mixer.SetFloat("soundVolume", volume);
        GeneralManager.soundVolume = volume;
    }

    public void SetVolumes(float musicVolume, float soundVolume) {
        musicSlider.GetComponent<Slider>().value = musicVolume;
        soundSlider.GetComponent<Slider>().value = soundVolume;
        mixer.SetFloat("musicVolume", musicVolume);
        mixer.SetFloat("soundVolume", soundVolume);
    }

}
