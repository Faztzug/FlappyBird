using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private float[] randomChance;
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        int spritesLength = sprites.Length;

        for (int i = 0; i < spritesLength; i++)
        {
            float rng = Random.Range(0f, 100f);
            if(rng < randomChance[i])
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.sprite = sprites[i];
                }
                
                break;
            }
        }
    }

    
}
