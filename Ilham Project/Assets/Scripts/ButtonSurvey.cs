using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSurvey : MonoBehaviour
{
    public Material InactiveMaterial;                   // Material yang digunakan saat tidak sedang dilihat
    public Material GazedAtMaterial;                   // Material yang digunakan saat sedang dilihat
    private Renderer _myRenderer;                      // Renderer untuk mengubah material objek
    [SerializeField] private float worriedPoint;       // Poin kekhawatiran yang ditambahkan ke survei
    [SerializeField] private float gazeDuration = 2.0f; // Durasi waktu untuk menahan pandangan
    [SerializeField] private SurveyScript surveyScript; // Referensi ke script SurveyScript

    private Coroutine gazeCoroutine;                   // Menyimpan coroutine aktif untuk mengatur waktu pandangan

    [SerializeField] private AudioSource audioSource;  // AudioSource untuk memainkan efek suara
    [SerializeField] private AudioClip gazeItSfx;      // Efek suara ketika pandangan dimulai
    [SerializeField] private AudioClip ChoiceItSfx;    // Efek suara ketika pilihan dikonfirmasi
    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();        // Inisialisasi renderer
        SetMaterial(false);                            // Atur material ke InactiveMaterial saat memulai
    }

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(gazeItSfx);            // Mainkan efek suara untuk memulai pandangan
        SetMaterial(true);                             // Ubah material ke GazedAtMaterial
        gazeCoroutine = StartCoroutine(StartGazeTimer()); // Mulai coroutine untuk menghitung durasi pandangan
    }

    public void OnPointerExit()
    {
        SetMaterial(false);                            // Ubah kembali material ke InactiveMaterial
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);             // Hentikan coroutine jika pointer keluar
            gazeCoroutine = null;                     // Reset referensi coroutine
        }
    }

    private IEnumerator StartGazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration); // Tunggu selama durasi pandangan
        audioSource.PlayOneShot(ChoiceItSfx);          // Mainkan efek suara untuk konfirmasi pilihan
        surveyScript.AddWoriedPoint(worriedPoint);     // Tambahkan poin kekhawatiran ke survei
        surveyScript.NextQuestion();                  // Pindah ke pertanyaan berikutnya
        OnPointerExit();                               // Panggil OnPointerExit untuk reset status
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial; // Atur material berdasarkan status
        }
    }
}
