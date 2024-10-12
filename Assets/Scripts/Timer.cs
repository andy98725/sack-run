using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float startTime;

    public UnityEngine.UI.Text text;


    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (showTotal)
            text.text = totalTime.ToString("F3");
        else if (!Game.isLoading)
            text.text = (Time.time - startTime).ToString("F3");
    }

    static float totalTime = 0;

    public void storeTime()
    {
        totalTime += Time.time - startTime;
    }
    public bool showTotal = false;
}
