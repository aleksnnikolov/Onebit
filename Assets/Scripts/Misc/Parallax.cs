using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    float lenght, startPosX, startPosY;
    private GameObject cam;
    public float horizontalParallaxEffect;
    public float verticalParallaxEffect;


    void Awake()
    {
        cam = Camera.main.gameObject;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - horizontalParallaxEffect));
        float distX = (cam.transform.position.x * horizontalParallaxEffect);
        float distY = (cam.transform.position.y * verticalParallaxEffect);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (temp > startPosX + lenght)
            startPosX += lenght;
        else if (temp < startPosX - lenght)
            startPosX -= lenght;

    }
}
