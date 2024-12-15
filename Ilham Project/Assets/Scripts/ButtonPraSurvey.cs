using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPraSurvey : MonoBehaviour
{
    public Material InactiveMaterial;                  // Material untuk tombol saat tidak digaze
    public Material GazedAtMaterial;                   // Material untuk tombol saat sedang digaze
    private Renderer _myRenderer;                      // Renderer tombol untuk mengubah material
    [SerializeField] private float worriedPoint;       // Poin kekhawatiran yang ditambahkan saat gaze selesai
    [SerializeField] private float gazeDuration = 2.0f; // Durasi waktu untuk menyelesaikan gaze
    [SerializeField] private PraSurvey surveyScript;   // Referensi script PraSurvey untuk memanggil metode

    private Coroutine gazeCoroutine;                   // Menyimpan coroutine aktif untuk gaze
    [SerializeField] private AudioSource audioSource;  // Audio source untuk memainkan efek suara
    [SerializeField] private AudioClip gazeItSfx;      // Efek suara saat gaze dimulai
    [SerializeField] private AudioClip ChoiceItSfx;    // Efek suara saat gaze selesai

    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();        // Ambil renderer dari game object ini
        SetMaterial(false);                            // Atur material ke inactive saat awal
    }

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(gazeItSfx);            // Mainkan suara untuk gaze dimulai
        SetMaterial(true);                             // Ubah material ke GazedAtMaterial
        gazeCoroutine = StartCoroutine(StartGazeTimer()); // Mulai coroutine untuk gaze timer
    }

    public void OnPointerExit()
    {
        SetMaterial(false);                            // Kembalikan material ke InactiveMaterial
        if (gazeCoroutine != null)                     // Jika gazeCoroutine aktif
        {
            StopCoroutine(gazeCoroutine);              // Hentikan coroutine gaze timer
            gazeCoroutine = null;                      // Reset gazeCoroutine ke null
        }
    }

    private IEnumerator StartGazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration); // Tunggu selama durasi gaze
        audioSource.PlayOneShot(ChoiceItSfx);          // Mainkan suara saat gaze selesai
        surveyScript.AddWoriedPoint(worriedPoint);     // Tambahkan poin kekhawatiran ke survey
        surveyScript.NextQuestion();                  // Panggil metode untuk pertanyaan berikutnya
        OnPointerExit();                               // Panggil OnPointerExit untuk reset
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)  // Pastikan material sudah diset
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial; // Atur material berdasarkan gaze
        }
    }
}
