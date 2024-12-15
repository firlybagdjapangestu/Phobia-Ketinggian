using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraSurvey : MonoBehaviour
{
    [SerializeField] private string[] question;            // Array untuk menyimpan pertanyaan survei
    [SerializeField] private float worriedTotal;           // Total poin kekhawatiran yang dikumpulkan
    [SerializeField] private TextMesh questionDisplay;     // Komponen TextMesh untuk menampilkan pertanyaan
    [SerializeField] private int currentQuestion;          // Indeks pertanyaan saat ini
    [SerializeField] private GameObject buttonScale;       // Tombol untuk skala pilihan
    [SerializeField] private GameObject buttonPlayVR;      // Tombol untuk memulai simulasi VR
    [SerializeField] private GameObject[] scaleText;       // Array untuk teks skala penilaian

    private void OnEnable()
    {
        question = new string[5];                          // Inisialisasi array pertanyaan dengan 5 elemen
        question[0] = "Seberapa khawatir anda\ndengan ketinggian?";
        question[1] = "Seberapa sering anda\nmerasa cemas atau takut ketika\nberada ditempat tinggi";
        question[2] = "Seberapa nyaman anda untuk\nmengikuti simulasi ketinggian";
        question[3] = "Seberapa berani anda\nmelihat ketinggian";
        question[4] = "Seberapa siap anda melakukan\nsimulasi ini";
        currentQuestion = 0;                               // Set pertanyaan pertama sebagai pertanyaan aktif
        questionDisplay.text = question[currentQuestion];  // Tampilkan pertanyaan pertama

        buttonScale.SetActive(true);                       // Aktifkan tombol skala
        buttonPlayVR.SetActive(false);                     // Nonaktifkan tombol untuk memulai VR
    }

    public void AddWoriedPoint(float worriedPoint)
    {
        worriedTotal += worriedPoint;                      // Tambahkan poin kekhawatiran ke total
    }

    public void NextQuestion()
    {
        currentQuestion++;                                 // Pindah ke pertanyaan berikutnya
        if (currentQuestion < question.Length)            // Jika masih ada pertanyaan yang tersisa
        {
            questionDisplay.text = question[currentQuestion]; // Tampilkan pertanyaan berikutnya
        }
        else
        {
            ResultSurvey();                                // Panggil ResultSurvey jika semua pertanyaan terjawab
        }
    }

    public void ResultSurvey()
    {
        if (worriedTotal <= 6)                             // Jika total poin kekhawatiran kecil
        {
            questionDisplay.text = "Kami merekomendasikan LV4";
        }
        else if (worriedTotal > 6 && worriedTotal <= 12)   // Jika total poin kekhawatiran sedang
        {
            questionDisplay.text = "Kami merekomendasikan LV3";
        }
        else if (worriedTotal > 12 && worriedTotal <= 18)  // Jika total poin kekhawatiran cukup tinggi
        {
            questionDisplay.text = "Kami merekomendasikan LV2";
        }
        else if (worriedTotal > 18)                       // Jika total poin kekhawatiran sangat tinggi
        {
            questionDisplay.text = "Kami merekomendasikan LV1";
        }
        else                                               // Jika nilai tidak valid
        {
            questionDisplay.text = "Nilai tidak valid";    // Opsi untuk kasus yang tidak terduga
        }
        buttonScale.SetActive(false);                     // Nonaktifkan tombol skala
        buttonPlayVR.SetActive(true);                     // Aktifkan tombol untuk memulai simulasi VR
        scaleText[0].SetActive(false);                    // Nonaktifkan teks skala pertama
        scaleText[1].SetActive(false);                    // Nonaktifkan teks skala kedua
    }
}
