using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject follow;

    const float lerp = 0.98f; // % per second

    void Start()
    {
        transform.localPosition += positionAdjustment();
    }

    Vector3 positionAdjustment()
    {
        return follow.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += positionAdjustment() * (1 - (float)Math.Pow(1 - lerp, Time.deltaTime));
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (transform.position.y < Move.deathY + 2)
            transform.position = new Vector3(transform.position.x, Move.deathY + 2, -10);
    }
}
