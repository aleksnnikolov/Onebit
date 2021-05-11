using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingExtraCrawlers : MonoBehaviour
{
    public GameObject screen;

    int crawlersRetrieved = 0;
    int maxCrawlers = 12;

    public TMP_Text text;
    string extraText;

    public Color greenColor;
    public Color redColor;

    void Awake() {
        screen = GameObject.Find("extraCrawlersScreen");
        text = screen.GetComponentInChildren<TextMeshProUGUI>();

        int crawlersRetrieved = GameManager.extraCrawlersRetrieved;
        if (crawlersRetrieved == maxCrawlers) {
            extraText = "YOU MANAGED TO RETRIEVE ALL " + crawlersRetrieved + "/12 CRAWLERS";
            text.color = greenColor;

        } else {
            extraText = "YOU RETRIEVED\n" + crawlersRetrieved + "/12 CRAWLERS";
            text.color = redColor;
        }

        
        text.text = extraText;
    }

}
