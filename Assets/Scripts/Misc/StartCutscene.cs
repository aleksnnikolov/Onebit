using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    AudioManager audioManager;

    GameObject bit;
    GameObject aura;

    SpriteRenderer sr;
    ParticleSystem bitParticles;

    public Color onColor;
    public Color offColor;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        bit = GameObject.FindGameObjectWithTag("Bit");
        aura = bit.GetComponent<BitBehaviour>().powerAura;
        bitParticles = bit.GetComponent<BitBehaviour>().idle_fx;

        sr = bit.GetComponent<SpriteRenderer>();

        StartCoroutine(BeginCutscene());
    }

    IEnumerator BeginCutscene() {
        bit.GetComponent<Rigidbody2D>().simulated = false;
        sr.color = offColor;
        aura.SetActive(false);
        bitParticles.Stop();
        Pause.canPause = false;

        yield return new WaitForSecondsRealtime(2.0f);
        //flicker and turn on bit
        sr.color = onColor;
        yield return new WaitForSecondsRealtime(0.1f);
        audioManager.Play("hum", 0);
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.05f);
        sr.color = onColor;
        yield return new WaitForSecondsRealtime(0.2f);
        audioManager.Play("hum", 0);
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.1f);
        sr.color = onColor;
        yield return new WaitForSecondsRealtime(0.3f);
        audioManager.Play("hum", 0);
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.15f);
        sr.color = onColor;
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.05f);
        audioManager.Play("hum", 0);
        sr.color = onColor;
        yield return new WaitForSecondsRealtime(0.2f);
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.1f);
        sr.color = onColor;
        yield return new WaitForSecondsRealtime(0.3f);
        audioManager.Play("hum", 0);
        sr.color = offColor;
        yield return new WaitForSecondsRealtime(0.15f);
        sr.color = onColor;

        bitParticles.Play();
        audioManager.Play("hum", 0);

        yield return new WaitForSecondsRealtime(1.0f);
        //flicker and turn on aura
        yield return new WaitForSecondsRealtime(0.1f);
        aura.SetActive(true);
        yield return new WaitForSecondsRealtime(0.05f);
        audioManager.Play("hum", 0);
        aura.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        aura.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        aura.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        audioManager.Play("hum", 0);
        aura.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);
        aura.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        aura.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        audioManager.Play("hum", 0);
        aura.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        aura.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);
        aura.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        audioManager.Play("hum", 0);
        aura.SetActive(true);

        bit.GetComponent<Rigidbody2D>().simulated = true;
        Pause.canPause = true;

        yield return new WaitForSecondsRealtime(1f);
        audioManager.Play("bgMusic", 0);
    }
 
}
