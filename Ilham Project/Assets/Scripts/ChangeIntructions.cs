using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIntructions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteDisplay;
    [SerializeField] private Sprite[] spritesIntructur;
    [SerializeField] private int lenguageID; // 0 = English, 1 = Bahasa Indonesia
    // Start is called before the first frame update
    void Start()
    {
        lenguageID = PlayerPrefs.GetInt("LenguageID", 0); // Retrieve language ID from PlayerPrefs, default to 0 if not set
        if (lenguageID == 1) // Bahasa Indonesia
        {
            IndonesiaVersion();
        }
        else if (lenguageID == 0) // English
        {
            EnglishVersion();
        }
    }


    void EnglishVersion()
    {
        spriteDisplay[0].sprite = spritesIntructur[4];
        spriteDisplay[1].sprite = spritesIntructur[5];
        spriteDisplay[2].sprite = spritesIntructur[6];
        spriteDisplay[3].sprite = spritesIntructur[7];
    }

    void IndonesiaVersion()
    {
        spriteDisplay[0].sprite = spritesIntructur[0];
        spriteDisplay[1].sprite = spritesIntructur[1];
        spriteDisplay[2].sprite = spritesIntructur[2];
        spriteDisplay[3].sprite = spritesIntructur[3];
    }

}
