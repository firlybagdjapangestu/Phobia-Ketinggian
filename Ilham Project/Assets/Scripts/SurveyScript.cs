using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyScript : MonoBehaviour
{
    [SerializeField] private string[] question;                    // Pertanyaan-pertanyaan dalam survei  
    [SerializeField] private float worriedTotal;                   // Total poin kekhawatiran pengguna  
    [SerializeField] private TextMesh questionDisplay;             // Objek untuk menampilkan teks pertanyaan  
    [SerializeField] private int currentQuestion;                  // Indeks pertanyaan yang sedang ditampilkan  
    [SerializeField] private GameObject buttonScale;               // Tombol untuk memilih skala kekhawatiran  
    [SerializeField] private GameObject buttonNext;                // Tombol untuk melanjutkan ke level berikutnya  
    [SerializeField] private GameObject buttonExit;                // Tombol untuk keluar dari simulasi  
    [SerializeField] private GameObject[] scaleText;               // Teks skala yang ditampilkan kepada pengguna  
    [SerializeField] private int level;                            // Level pengguna yang disimpan melalui PlayerPrefs  
    [SerializeField] private int lenguageID;                       // ID bahasa yang digunakan  

    private void Start()
    {
        lenguageID = PlayerPrefs.GetInt("LenguageID", 0);         // Ambil ID bahasa dari PlayerPrefs, default 0 jika tidak ada  

        if (lenguageID == 0)
        {
            question[0] = "After running the simulation\nhow afraid are you now\nof heights?";  // Pertanyaan pertama  
            question[1] = "How nervous are you\nright now?";                                    // Pertanyaan kedua  
            question[2] = "How dizzy are you\nright now?";                                      // Pertanyaan ketiga  
            question[3] = "Do you feel\nuncomfortable?";                                        // Pertanyaan keempat  
            question[4] = "How helpful was this simulation\nfor you?";                         // Pertanyaan kelima  
        }
        else if (lenguageID == 1)
        {
            question[0] = "Setelah menjalankan simulasi\nseberapa takut anda sekarang\nterhadap ketinggian ?";  // Pertanyaan pertama  
            question[1] = "Seberapa deg-degan anda\nsekarang ?";                                               // Pertanyaan kedua  
            question[2] = "Seberapa pusing anda\nsekarang ?";                                                  // Pertanyaan ketiga  
            question[3] = "Apakah anda\nmerasa tidak nyaman ?";                                                // Pertanyaan keempat  
            question[4] = "Seberapa membantu simulasi ini\nuntuk anda ?";                                      // Pertanyaan kelima  
        }

        currentQuestion = 0;                                                                              // Set pertanyaan pertama  
        questionDisplay.text = question[currentQuestion];                                                 // Tampilkan pertanyaan pertama  
        buttonScale.SetActive(true);                                                                      // Aktifkan tombol skala kekhawatiran  
        level = PlayerPrefs.GetInt("Level");                                                              // Ambil level pengguna dari PlayerPrefs  
    }

    public void AddWoriedPoint(float worriedPoint)                    // Tambahkan poin kekhawatiran dari pilihan pengguna  
    {
        worriedTotal += worriedPoint;                                 // Tambahkan poin kekhawatiran ke total  
    }

    public void NextQuestion()                                        // Pindah ke pertanyaan berikutnya  
    {
        currentQuestion++;                                            // Tingkatkan indeks pertanyaan  
        if (currentQuestion < question.Length)                        // Cek apakah masih ada pertanyaan  
        {
            questionDisplay.text = question[currentQuestion];         // Tampilkan pertanyaan berikutnya  
        }
        else
        {
            ResultSurvey();                                           // Tampilkan hasil survei jika semua pertanyaan selesai  
        }
    }

    public void ResultSurvey()                                        // Logika untuk menampilkan hasil survei  
    {
        if (worriedTotal <= 15)                                       // Jika total kekhawatiran kecil (<= 15)  
        {
            if (level < 3)                                            // Jika level pengguna di bawah 3  
            {
                buttonNext.SetActive(true);                           // Aktifkan tombol "Next"  
                buttonExit.SetActive(false);                          // Nonaktifkan tombol "Exit"  
                questionDisplay.text = lenguageID == 0 ?
                    "You can proceed\nto the next level\n" :
                    "Anda bisa untuk melanjutkan\nke level berikutnya\n";
            }
            else
            {
                buttonExit.SetActive(true);                           // Aktifkan tombol "Exit"  
                buttonNext.SetActive(false);                          // Nonaktifkan tombol "Next"  
                questionDisplay.text = lenguageID == 0 ?
                    "Congratulations and get well soon\n" :
                    "Selamat dan semoga cepat pulih\n";
            }
        }
        else if (worriedTotal > 15 && worriedTotal <= 20)             // Jika kekhawatiran sedang (> 15 dan <= 20)  
        {
            questionDisplay.text = lenguageID == 0 ?
                "Repeat this level again\n" :
                "Ulangi lagi level ini\n";
        }
        else if (worriedTotal > 20 && worriedTotal <= 30)             // Jika kekhawatiran tinggi (> 20 dan <= 30)  
        {
            questionDisplay.text = lenguageID == 0 ?
                "You need to go back\nto the previous level\n" :
                "Anda harus kembali\nke level sebelumnya\n";
        }
        else                                                          // Jika nilai tidak valid  
        {
            questionDisplay.text = lenguageID == 0 ?
                "Invalid value" :
                "Nilai tidak valid";
        }

        buttonScale.SetActive(false);                                 // Nonaktifkan tombol skala  
        scaleText[0].SetActive(false);                                // Nonaktifkan teks skala pertama  
        scaleText[1].SetActive(false);                                // Nonaktifkan teks skala kedua  
    }
}
