using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLenguage : MonoBehaviour
{
    public TextMesh textMeshPro;
    public string englishText;
    public string indonesianText;
    public int lenguageID;
    public int maxCharactersPerLine = 40; // Set the maximum number of characters per line

    // Start is called before the first frame update  
    void Start()
    {
        lenguageID = PlayerPrefs.GetInt("LenguageID", 0); // Mengambil nilai dari PlayerPrefs, default 0 jika tidak ada  

        if (lenguageID == 0)
        {
            EnglishVersion();
        }
        else if (lenguageID == 1)
        {
            IndonesiaVersion();
        }
    }

    void EnglishVersion()
    {
        textMeshPro.text = FormatText(englishText);
    }

    private void IndonesiaVersion()
    {
        textMeshPro.text = FormatText(indonesianText);
    }

    private string FormatText(string text)
    {
        string[] words = text.Split(' ');
        string formattedText = "";
        int currentLineLength = 0;

        foreach (string word in words)
        {
            if (currentLineLength + word.Length > maxCharactersPerLine)
            {
                formattedText += "\n";
                currentLineLength = 0;
            }

            formattedText += word + " ";
            currentLineLength += word.Length + 1; // Include space  
        }

        return formattedText.TrimEnd(); // Remove trailing space  
    }
}
