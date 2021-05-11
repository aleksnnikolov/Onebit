using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    public AudioMixerGroup musicGroup;
    public AudioMixerGroup soundGroup;

    void Awake() {
        
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 0;

            if (s.name == "bgMusic" || s.name == "menuBgMusic") {
                s.source.outputAudioMixerGroup = musicGroup;
            } else {
                s.source.outputAudioMixerGroup = soundGroup;
            }
        }

    }

    private void Start() {
        //This gets called by StartCutscene
        //Play("bgMusic", 0);
    }

    public void Play(string name, float delay) {
        Sound toPlay = FindSound(name);

        if (toPlay == null) {
            Debug.Log("Inexistent sound");
            return;
        }
        if (!toPlay.source.isPlaying) {
            if (delay == 0)
                toPlay.source.Play();
            else
                toPlay.source.PlayDelayed(delay);
        }
    }

    public void Stop(string name) {
        Sound toStop = FindSound(name);
        toStop.source.Stop();
    }

    Sound FindSound(string name) {
        Sound sound = null;

        foreach (Sound s in sounds) {
            if (s.name == name)
                sound = s;
        }

        return sound;
    }

    public void StopAll() {
        foreach (Sound s in sounds) {
            if (s.name != "bgMusic")
                s.source.Stop();
        }
    }
}
