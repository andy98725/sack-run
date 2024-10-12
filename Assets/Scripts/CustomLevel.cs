using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.WebGLFileBrowser;

public class CustomLevel : MonoBehaviour
{

    public static Sprite level, bad, good;

    public static Vector2 startingLoc = Vector2.zero;

    public bool isButton;
    public string customName;
    void Start()
    {
        if (!isButton) LoadLayout();
    }

    public void OnMouseUpAsButton()
    {
        if (isButton) GetCustomLevel();

    }
    private void GetCustomLevel()
    {

        WebGLFileBrowser.FilesWereOpenedEvent += (File[] opened) =>
        {
            if (opened.Length == 0) return;
            if (!opened[0].IsImage()) return;

            Sprite baseSpr = LoadPNG(opened[0]);
            if (baseSpr == null) return;

            ParseSprite(baseSpr);

            UnityEngine.SceneManagement.SceneManager.LoadScene(customName);
        };
        WebGLFileBrowser.OpenFilePanelWithFilters(".png,.jpg");
    }


    private Sprite LoadPNG(File file)
    {
        Texture2D t = new Texture2D(2, 2);
        if (!t.LoadImage(file.data)) return null;

        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0), 100);
    }

    static Color levelColor = new Color(0.1f, 0.5f, 0.1f);
    static Color badColor = Color.red;
    static Color goodColor = Color.yellow;
    static Color startingColor = new Color(0.48f, 0.27f, 0.11f);
    private void ParseSprite(Sprite sprite)
    {
        // TODO set level, bad, good, startingLoc

        Texture2D baseTex = sprite.texture;
        Color[] pixels = baseTex.GetPixels();
        int width = baseTex.width;
        int height = baseTex.height;


        Texture2D levelTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D goodTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Texture2D badTex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Color[] transparentPixels = new Color[width * height];
        for (int i = 0; i < transparentPixels.Length; i++)
        {
            transparentPixels[i] = Color.clear;
        }

        levelTex.SetPixels(transparentPixels);
        goodTex.SetPixels(transparentPixels);
        badTex.SetPixels(transparentPixels);

        level = Sprite.Create(levelTex, new Rect(0, 0, width, height), new Vector2(0, 0), 100);
        good = Sprite.Create(goodTex, new Rect(0, 0, width, height), new Vector2(0, 0), 100);
        bad = Sprite.Create(badTex, new Rect(0, 0, width, height), new Vector2(0, 0), 100);
        float startingThreshold = 0.1f;


        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                Color pixel = pixels[y * width + x];
                if (ColorDistance(pixel, levelColor) < 0.2f)
                {
                    levelTex.SetPixel(x, y, pixel);
                }
                else if (ColorDistance(pixel, badColor) < 0.2f)
                {
                    badTex.SetPixel(x, y, pixel);
                }
                else if (ColorDistance(pixel, goodColor) < 0.2f)
                {
                    goodTex.SetPixel(x, y, pixel);
                }
                else if (ColorDistance(pixel, startingColor) < startingThreshold)
                {
                    startingLoc = new Vector2(x, y) / 100;
                    startingThreshold = ColorDistance(pixel, startingColor);
                }
            }

        levelTex.Apply();
        goodTex.Apply();
        badTex.Apply();
    }

    private float ColorDistance(Color a, Color b)
    {
        return Mathf.Sqrt((a.r - b.r) * (a.r - b.r) + (a.g - b.g) * (a.g - b.g) + (a.b - b.b) * (a.b - b.b));
    }
    private void LoadLayout()
    {
        Load(transform.Find("Ground"), level);
        Load(transform.Find("Good"), good);
        Load(transform.Find("Bad"), bad);

        FindObjectOfType<Move>().transform.position = startingLoc;
    }
    private void Load(Transform target, Sprite sprite)
    {
        if (target == null) return;
        if (sprite == null)
        {
            Destroy(target.gameObject);
            return;
        }

        target.GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(target.GetComponent<PolygonCollider2D>());
        target.gameObject.AddComponent<PolygonCollider2D>();

    }
}
