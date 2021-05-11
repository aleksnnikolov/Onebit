using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerOutline : MonoBehaviour
{

    public GameObject eye;
    public ParticleSystem deathParticles;
    bool isDestroyed = false;

    public Color offColor;
    public Color poweredColor;
    public Color controlledColor;
    public Color whiteColor;
    public Color blueColor;
    public Color redColor;

    public float offThickness;
    public float onThickness;

    float thickness;
    Color color;
    Color eyeColor;

    SpriteRenderer renderer;
    SpriteRenderer eyeRenderer;
    MaterialPropertyBlock propBlock;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        eyeRenderer = eye.GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
        thickness = 0;
        color = offColor;
        eyeColor = offColor;
    }

    void Update() {

        if (!isDestroyed) {
            eyeRenderer.color = eyeColor;

            renderer.GetPropertyBlock(propBlock);

            propBlock.SetFloat("Vector1_9EEEAB9A", thickness);
            propBlock.SetColor("Color_9692251C", color);

            renderer.SetPropertyBlock(propBlock);
        }
    }

    public void IsPowered() {
        thickness = onThickness;
        color = poweredColor;
        eyeColor = poweredColor;
    }

    public void IsNotPowered() {
        thickness = offThickness;
        color = offColor;
        eyeColor = offColor;
    }

    public void IsControlled() {
        thickness = onThickness;
        color = controlledColor;
        eyeColor = controlledColor;
    }

    public void IsNotControlled() {
        thickness = onThickness;
        color = poweredColor;
        eyeColor = poweredColor;
    }

    public void DestroyCrawler() {
        StartCoroutine(DestructionColors());
    }

    IEnumerator DestructionColors() {

        deathParticles.Play();
        thickness = onThickness;

        color = redColor;
        eyeColor = redColor;
        yield return new WaitForSeconds(0.05f);
        color = blueColor;
        eyeColor = blueColor;
        yield return new WaitForSeconds(0.05f);
        color = poweredColor;
        eyeColor = poweredColor;
        yield return new WaitForSeconds(0.05f);
        color = controlledColor;
        eyeColor = controlledColor;
        yield return new WaitForSeconds(0.05f);
        color = offColor;
        eyeColor = offColor;
        yield return new WaitForSeconds(0.05f);
        color = whiteColor;
        eyeColor = whiteColor;
        yield return new WaitForSeconds(0.05f);
        color = controlledColor;
        eyeColor = controlledColor;
        yield return new WaitForSeconds(0.05f);
        color = offColor;
        eyeColor = offColor;
        yield return new WaitForSeconds(0.05f);
        color = redColor;
        eyeColor = redColor;
        yield return new WaitForSeconds(0.05f);
        color = blueColor;
        eyeColor = blueColor;
        yield return new WaitForSeconds(0.05f);

        thickness = offThickness;
        deathParticles.Stop();
    }

}
