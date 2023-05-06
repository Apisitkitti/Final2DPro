using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSimplex2S;

public class BackgroundGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    public Color color1;
    public Color color2;

    private Texture2D texture;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                float sample = OpenSimplex2S.Noise2(x, yCoord, xCoord);
                Color color = Color.Lerp(color1, color2, (sample + 1f) / 2f);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    private void Update()
    {
        GenerateBackground();
    }

    private void GenerateBackground()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                float sample = OpenSimplex2S.Noise2(x, yCoord, xCoord);
                Color color = Color.Lerp(color1, color2, (sample + 1f) / 2f);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
    }
}
