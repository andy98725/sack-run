using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public string firstLevel;

    public void OnMouseUpAsButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(firstLevel);
    }
}
