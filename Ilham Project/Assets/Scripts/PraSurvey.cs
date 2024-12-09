using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraSurvey : MonoBehaviour
{
    [SerializeField] private string[] question;
    [SerializeField] private float worriedTotal;
    [SerializeField] private TextMesh questionDisplay;
    [SerializeField] private int currentQuestion;
    [SerializeField] private GameObject buttonScale;
    [SerializeField] private GameObject buttonPlayVR;
    [SerializeField] private GameObject[] scaleText;

    private void OnEnable()
    {
        question = new string[5];
        question[0] = "Seberapa khawatir anda\ndengan ketinggian?";
        question[1] = "Seberapa sering anda\nmerasa cemas atau takut ketika\nberada ditempat tinggi";
        question[2] = "Seberapa nyaman anda untuk\nmengikuti simulasi ketinggian";
        question[3] = "Seberapa berani anda\nmelihat ketinggian";
        question[4] = "Seberapa siap anda melakukan\nsimulasi ini";
        currentQuestion = 0;
        questionDisplay.text = question[currentQuestion];
        
        buttonScale.SetActive(true);
        buttonPlayVR.SetActive(false);
    }

    public void AddWoriedPoint(float worriedPoint)
    {
        worriedTotal += worriedPoint;
    }

    public void NextQuestion()
    {
        currentQuestion++;
        if (currentQuestion < question.Length)
        {
            questionDisplay.text = question[currentQuestion];
        }
        else
        {
            ResultSurvey(); // Call ResultSurvey when all questions are answered
        }
    }

    public void ResultSurvey()
    {
        if (worriedTotal <= 6)
        {
            questionDisplay.text = "Kami merekomendasikan LV4";
        }
        else if (worriedTotal > 6 && worriedTotal <= 12)
        {
            questionDisplay.text = "Kami merekomendasikan LV3";
        }
        else if (worriedTotal > 12 && worriedTotal <= 18)
        {
            questionDisplay.text = "Kami merekomendasikan LV2";
        }
        else if (worriedTotal > 18)
        {
            questionDisplay.text = "Kami merekomendasikan LV1";
        }
        else
        {
            questionDisplay.text = "Nilai tidak valid"; // Optional, untuk kasus tidak terduga
        }
        buttonScale.SetActive(false);
        buttonPlayVR.SetActive(true);
        scaleText[0].SetActive(false);
        scaleText[1].SetActive(false);
    }
    
}
