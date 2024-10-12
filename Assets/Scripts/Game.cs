
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{


    public SpriteRenderer sack;
    public string nextLevel;
    public static bool isLoading = false;

    public Timer timer;

    void Start()
    {
        isLoading = false;
        AudioListener.volume = 0.2f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.F5))
            Screen.fullScreen = !Screen.fullScreen;
    }

    public void Win()
    {
        if (isLoading) return;
        isLoading = true;

        if (timer != null) timer.storeTime();
        StartCoroutine(FadeOutAndLoad(nextLevel));
    }

    public void Die()
    {
        if (isLoading) return;
        isLoading = true;

        string curLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        StartCoroutine(FadeOutAndLoad(curLevel));
    }

    private IEnumerator FadeOutAndLoad(string scene)
    {
        float fadeDuration = 1f; // Adjust this value to change fade duration
        float elapsedTime = 0f;
        Color originalColor = sack.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sack.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Restart the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}