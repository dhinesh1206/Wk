using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour {

    public Sprite[] spritestochange;
    public float fps = 24;
    public GameObject objectsToChnageSprite;

    GameObject gb;

    public enum SpriteChangeType
    {
        SpriteRenderer,
        Mask,
        Image
    }
    public SpriteChangeType type;

    public void Start()
    {
        if(objectsToChnageSprite)
        {
            gb = objectsToChnageSprite;
        } else
        {
            gb = gameObject;
        }

        StartCoroutine(SpriteChange());
    }

    IEnumerator SpriteChange()
    {
        for (int i = 0; i < spritestochange.Length; i++)
        {
            if(type == SpriteChangeType.SpriteRenderer)
            {
                gb.GetComponent<SpriteRenderer>().sprite = spritestochange[i];
               
            }
            else if(type == SpriteChangeType.Mask)
            {
                gb.GetComponent<SpriteMask>().sprite = spritestochange[i];
            }
            else if (type == SpriteChangeType.Image)
            {
                gb.GetComponent<Image>().sprite = spritestochange[i];
            }

            yield return new WaitForSeconds(1 / fps);
        }
        yield return null;
        StartCoroutine(SpriteChange());
    }
}
