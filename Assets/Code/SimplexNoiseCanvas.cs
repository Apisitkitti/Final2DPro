using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OpenSimplex2S;

public class SimplexNoiseCanvas : MonoBehaviour
{
    //public List<Sprite> images;
    public float scale = 1.0f;
    public float speed = 1.0f;

    private Image imageComponent;
    private Texture2D texture;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        texture = new Texture2D((int)imageComponent.rectTransform.rect.width, (int)imageComponent.rectTransform.rect.height);

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
{
    while (true)
    {
        if (imageComponent != null)
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color color = new Color(Random.value, Random.value, Random.value);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();

            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.zero);

            imageComponent.sprite = sprite;
        }

        yield return null;
    }
}



    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnEnable()
    {
        StartCoroutine(Animate());
    }
}
