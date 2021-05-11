using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperTrap : MonoBehaviour
{

    public bool isMoving;
    public GameObject edge1;
    public GameObject edge2;
    public GameObject zapper;

    void Awake()
    {
        SetupZapper();
    }


    void Update()
    {
        if (isMoving) {
            SetupZapper();
        }
    }

    private void SetupZapper() {
        Vector2 edge1pos = edge1.transform.position;
        Vector2 edge2pos = edge2.transform.position;
        float edgeDst = Vector2.Distance(edge1pos, edge2pos);

        SpriteRenderer zapperRenderer = zapper.GetComponent<SpriteRenderer>();
        Vector2 newSize = new Vector2(edgeDst, 1);
        zapperRenderer.size = newSize;

        BoxCollider2D zapperCollider = zapper.GetComponent<BoxCollider2D>();
        zapperCollider.size = new Vector2(zapperRenderer.size.x, zapperCollider.size.y);

        Vector2 newPos = new Vector2(edge1pos.x + (edge2pos.x - edge1pos.x) / 2, edge1pos.y + (edge2pos.y - edge1pos.y) / 2);
        zapper.transform.position = newPos;

        Vector2 edgeDir = edge1.transform.position - edge2.transform.position;
        float angle = Mathf.Atan2(edgeDir.y, edgeDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        zapper.transform.rotation = rotation;
    }

    Vector2 AbsV2(Vector2 v2) {
        return new Vector2(Mathf.Abs(v2.x), Mathf.Abs(v2.y));
    }
}
