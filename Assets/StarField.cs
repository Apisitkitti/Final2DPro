using UnityEngine;

public class StarField : MonoBehaviour
{
    public int width = 512;
    public int height = 512;
    public float starDensity = 0.01f;
    public float starBrightness = 2.0f;

    private Texture2D texture;

    void Start()
    {
        texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // Loop through every pixel in the texture
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Randomly place stars based on star density
if (Random.value < starDensity)
{
    // Set a random brightness value for the star
    float brightness = Random.Range(0.0f, starBrightness);
    Color color = new Color(brightness, brightness, brightness, 1.0f);

    // Set the pixel color to the star color
    texture.SetPixel(x, y, color);
}
else
{
    // Set the pixel color to black for the background
    texture.SetPixel(x, y, Color.black);
}
            }
        }

        // Apply the texture changes
        texture.Apply();

        // Create a sprite from the texture and add it to a new game object
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);
        GameObject starFieldObject = new GameObject("StarField");
        SpriteRenderer spriteRenderer = starFieldObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
