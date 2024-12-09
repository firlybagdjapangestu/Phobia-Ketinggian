using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyScript : MonoBehaviour
{
    [SerializeField] private string[] question;
    [SerializeField] private float worriedTotal;
    [SerializeField] private TextMesh questionDisplay;
    [SerializeField] private int currentQuestion;
    [SerializeField] private GameObject buttonScale;
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private GameObject[] scaleText;


    private void Start()
    {
        question[0] = "Setelah menjalankan simulasi\nseberapa takut anda sekarang\nterhadap ketinggian ?";
        question[1] = "Seberapa deg-degan anda\nsekarang ?";
        question[2] = "Seberapa pusing anda\nsekarang ?";
        question[3] = "Apakah anda\nmerasa tidak nyaman ?";
        question[4] = "Seberapa membantu simulasi ini\nuntuk anda ?";
        currentQuestion = 0;
        questionDisplay.text = question[currentQuestion];
        buttonScale.SetActive(true);
        
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
        if (worriedTotal <= 10)
        {
            int level = PlayerPrefs.GetInt("Level");
            if (level < 3) {
                buttonNext.SetActive(true);
                buttonExit.SetActive(false);
                questionDisplay.text = "Anda bisa untuk melanjutkan\nke level berikutnya";
            }
            else
            {
                buttonExit.SetActive(true);
                buttonNext.SetActive(false);
                questionDisplay.text = "Selamat dan semoga cepat pulih";
            }
            

        }
        else if (worriedTotal > 10 && worriedTotal <= 20)
        {
            questionDisplay.text = "Ulangi lagi level ini";
        }
        else if (worriedTotal > 20 && worriedTotal <= 30) // Fixed range logic
        {
            questionDisplay.text = "Anda harus kembali\nke level sebelumnya";
        }
        else
        {
            questionDisplay.text = "Nilai tidak valid"; // Optional for unexpected values
        }
        
        buttonScale.SetActive(false);
        scaleText[0].SetActive(false);
        scaleText[1].SetActive(false);
    }
}
