using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBlock : MonoBehaviour
{

    public bool horizontal;
    public GameObject support1;
    public GameObject support2;
    public GameObject blockBase;
    public GameObject overlay;

    void Start()
    {
        SetupCrawlerBlock();
    }



    void SetupCrawlerBlock() {
        Vector2 support1Pos = support1.transform.position;
        Vector2 support2Pos = support2.transform.position;
        float supportDst = Vector2.Distance(support1Pos, support2Pos);

        SpriteRenderer baseRenderer = blockBase.GetComponent<SpriteRenderer>();
        SpriteRenderer overlayRenderer = overlay.GetComponent<SpriteRenderer>();
        Vector2 newSize;
        newSize = new Vector2(0.5f, supportDst);

        baseRenderer.size = newSize;
        overlayRenderer.size = newSize;

        BoxCollider2D baseCollider = blockBase.GetComponent<BoxCollider2D>();
        baseCollider.size = new Vector2(baseRenderer.size.x / 2, baseRenderer.size.y);
        
        Vector2 newPos = new Vector2(support1Pos.x + (support2Pos.x - support1Pos.x) / 2, support1Pos.y + (support2Pos.y - support1Pos.y) / 2);
        blockBase.transform.position = newPos;
        overlay.transform.position = newPos;
    }

}
