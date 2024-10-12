using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSack : MonoBehaviour
{

    public Sprite sack1, sack2, sack3, sack4, sack5, sack6, sack7, sack8, sack9, sack0;

    public static Sprite currentSack;
    // Start is called before the first frame update
    void Start()
    {
        if (currentSack == null) currentSack = sack4;

        setSprite(currentSack);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            setSprite(sack1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            setSprite(sack2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            setSprite(sack3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            setSprite(sack4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            setSprite(sack5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            setSprite(sack6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            setSprite(sack7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            setSprite(sack8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            setSprite(sack9);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            setSprite(sack0);
    }

    private void setSprite(Sprite i)
    {
        if (i == null) return; currentSack = i;

        GetComponent<SpriteRenderer>().sprite = i;
        // Refresh mesh
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
