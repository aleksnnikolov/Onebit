using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageExtraCrawlers : MonoBehaviour
{
    public GameObject screen;

    public int extraCrawlersOnLevel;
    int extraCrawlers = 0;

    public TMP_Text text;
    string extraText;

    public Color redColor;
    public Color greenColor;

    void Awake() {
        screen = GameObject.Find("extraCrawlersScreen");
        text = screen.GetComponentInChildren<TextMeshProUGUI>();
        extraText = "EXTRA CRAWLERS RETRIEVED\n" + extraCrawlers + "/" + extraCrawlersOnLevel;
        text.color = redColor;
        text.text = extraText;
    }

    public void SendExtraCrawlers() {
        GameManager.extraCrawlersRetrieved += extraCrawlers;
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
            extraCrawlers++;

        extraText = "EXTRA CRAWLERS RETRIEVED\n" + extraCrawlers + "/" + extraCrawlersOnLevel;
        text.text = extraText;
        if (extraCrawlers >= extraCrawlersOnLevel)
            text.color = greenColor;
        else
            text.color = redColor;
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
            extraCrawlers--;

        extraText = "EXTRA CRAWLERS RETRIEVED\n" + extraCrawlers + "/" + extraCrawlersOnLevel;
        text.text = extraText;
        if (extraCrawlers >= extraCrawlersOnLevel)
            text.color = greenColor;
        else
            text.color = redColor;
    }
}
