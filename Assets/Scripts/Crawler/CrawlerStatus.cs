using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerStatus : MonoBehaviour
{

    Animator anim;
    CrawlerOutline outlineScript;

    public bool isAlive = true;

    private void Awake() {
        anim = GetComponent<Animator>();
        outlineScript = GetComponent<CrawlerOutline>();
    }

    public void KillCrawler() {
        isAlive = false;
        anim.SetBool("isAlive", false);
        outlineScript.DestroyCrawler();
    }

}
