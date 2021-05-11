using System.Collections;
using UnityEngine;

public class ZapperBehaviour : MonoBehaviour
{
    public GameObject zapperSource;
    AudioSource source;

    public Color greenColor;
    public Color purpleColor;

    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.color = greenColor;
        if (zapperSource != null) {
            source = zapperSource.GetComponent<AudioSource>();
            StartCoroutine(ZapperAudio());
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Bit") {
            col.gameObject.GetComponent<BitBehaviour>().CallDestroyBit();
        }
    }

    IEnumerator ZapperAudio() {
        float pitch = 0f;
        while (true) {
            pitch = Random.Range(0.8f, 1.2f);
            source.pitch = pitch;
            if (!source.isPlaying)
                source.Play();
            yield return new WaitForSecondsRealtime(0.08f);
        }
    }

}
