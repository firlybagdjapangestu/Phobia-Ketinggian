using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraSurvey : MonoBehaviour
{
    [SerializeField] private string[] question;            // Array to store survey questions  
    [SerializeField] private float worriedTotal;           // Total worry points accumulated  
    [SerializeField] private TextMesh questionDisplay;     // TextMesh component to display questions  
    [SerializeField] private int currentQuestion;          // Current question index  
    [SerializeField] private GameObject buttonScale;       // Button for scale options  
    [SerializeField] private GameObject buttonPlayVR;      // Button to start VR simulation  
    [SerializeField] private GameObject[] scaleText;       // Array for scale rating text  
    [SerializeField] private int lenguageID;               // Language ID being used  

    private void OnEnable()
    {
        lenguageID = PlayerPrefs.GetInt("LenguageID", 0);   // Retrieve language ID from PlayerPrefs, default to 0 if not set  
        question = new string[5];                          // Initialize question array with 5 elements  

        if (lenguageID == 1) // Bahasa Indonesia  
        {
            question[0] = "Seberapa khawatir anda\ndengan ketinggian?";
            question[1] = "Seberapa sering anda\nmerasa cemas atau takut ketika\nberada ditempat tinggi";
            question[2] = "Seberapa nyaman anda untuk\nmengikuti simulasi ketinggian";
            question[3] = "Seberapa berani anda\nmelihat ketinggian";
            question[4] = "Seberapa siap anda melakukan\nsimulasi ini";
        }
        else if (lenguageID == 0) // English  
        {
            question[0] = "How worried are you\nabout heights?";
            question[1] = "How often do you\nfeel anxious or afraid\nwhen in high places?";
            question[2] = "How comfortable are you\nwith participating in a \nheight simulation?";
            question[3] = "How brave are you\nwhen looking at heights?";
            question[4] = "How ready are you to\nperform this simulation?";
        }

        currentQuestion = 0;                               // Set the first question as the active question  
        questionDisplay.text = question[currentQuestion];  // Display the first question  

        buttonScale.SetActive(true);                       // Enable scale button  
        buttonPlayVR.SetActive(false);                     // Disable VR start button  
    }

    public void AddWoriedPoint(float worriedPoint)
    {
        worriedTotal += worriedPoint;                      // Add worry points to the total  
    }

    public void NextQuestion()
    {
        currentQuestion++;                                 // Move to the next question  
        if (currentQuestion < question.Length)            // If there are remaining questions  
        {
            questionDisplay.text = question[currentQuestion]; // Display the next question  
        }
        else
        {
            ResultSurvey();                                // Call ResultSurvey if all questions are answered  
        }
    }

    public void ResultSurvey()
    {
        if (worriedTotal <= 6) // If worry points are low  
        {
            questionDisplay.text = lenguageID == 0 ? "We recommend LV4" : "Kami merekomendasikan LV4";
        }
        else if (worriedTotal > 6 && worriedTotal <= 12) // If worry points are moderate  
        {
            questionDisplay.text = lenguageID == 0 ? "We recommend LV3" : "Kami merekomendasikan LV3";
        }
        else if (worriedTotal > 12 && worriedTotal <= 18) // If worry points are fairly high  
        {
            questionDisplay.text = lenguageID == 0 ? "We recommend LV2" : "Kami merekomendasikan LV2";
        }
        else if (worriedTotal > 18) // If worry points are very high  
        {
            questionDisplay.text = lenguageID == 0 ? "We recommend LV1" : "Kami merekomendasikan LV1";
        }
        else // If the value is invalid  
        {
            questionDisplay.text = lenguageID == 0 ? "Invalid value" : "Nilai tidak valid";
        }
        buttonScale.SetActive(false); // Disable scale button  
        buttonPlayVR.SetActive(true); // Enable VR start button  
        scaleText[0].SetActive(false); // Disable the first scale text  
        scaleText[1].SetActive(false); // Disable the second scale text  
    }
}
