using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public float[] bitPosition;
    public float[] camPosition;

    public int crawlersNumber;
    public float[,] crawlersPosition;

    public int boxesNumber;
    public float[,] boxesPosition;

    public SaveData(Vector3 _bitPos, Vector3 _camPos, GameObject[] crawlers, GameObject[] boxes) {
        bitPosition = new float[3];
        bitPosition[0] = _bitPos.x;
        bitPosition[1] = _bitPos.y;
        bitPosition[2] = _bitPos.z;

        camPosition = new float[3];
        camPosition[0] = _camPos.x;
        camPosition[1] = _camPos.y;
        camPosition[2] = _camPos.z;

        crawlersNumber = crawlers.Length;
        boxesNumber = boxes.Length;


        crawlersPosition = new float[crawlersNumber, 3];
        for (int i = 0; i < crawlersNumber; i++) {
            crawlersPosition[i, 0] = crawlers[i].transform.position.x;
            crawlersPosition[i, 1] = crawlers[i].transform.position.y;
            crawlersPosition[i, 2] = crawlers[i].transform.position.z;
        }

        boxesPosition = new float[boxesNumber, 3];
        for (int i = 0; i < boxesNumber; i++) {
            boxesPosition[i, 0] = boxes[i].transform.position.x;
            boxesPosition[i, 1] = boxes[i].transform.position.y;
            boxesPosition[i, 2] = boxes[i].transform.position.z;
        }
    }

}
