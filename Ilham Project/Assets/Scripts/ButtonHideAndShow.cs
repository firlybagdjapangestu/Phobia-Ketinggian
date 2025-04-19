using System.Collections;               // Import library untuk menggunakan IEnumerator dan Coroutine
using UnityEngine;                     // Import library utama Unity
using UnityEngine.SceneManagement;     // Import library untuk manajemen scene (tidak digunakan dalam script ini)

public class ButtonHideAndShow : MonoBehaviour
{
    public Material InactiveMaterial;              // Material saat objek tidak sedang di-gaze
    public Material GazedAtMaterial;              // Material saat objek sedang di-gaze
    private Renderer _myRenderer;                 // Renderer untuk mengatur material objek

    [Header("Hide and Show Menu Settings")]
    [SerializeField] private float gazeDuration = 2.0f;          // Waktu yang dibutuhkan untuk mengaktifkan aksi setelah gaze
    [SerializeField] private GameObject[] gazeObjects;          // Array objek yang bisa di-hide dan show

    private Coroutine gazeCoroutine;              // Menyimpan coroutine aktif untuk gaze timer
    [SerializeField] private AudioSource audioSource;          // AudioSource untuk memainkan efek suara
    [SerializeField] private AudioClip gazeItSfx;              // Efek suara saat gaze dimulai
    [SerializeField] private AudioClip ChoiceItSfx;            // Efek suara saat pilihan diaktifkan

    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();               // Mendapatkan komponen Renderer dari GameObject
        SetMaterial(false);                                   // Mengatur material awal sebagai InactiveMaterial
    }

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(gazeItSfx);                   // Memainkan efek suara saat pointer masuk
        SetMaterial(true);                                    // Mengatur material menjadi GazedAtMaterial
        gazeCoroutine = StartCoroutine(StartGazeTimer());     // Memulai coroutine untuk menghitung waktu gaze
    }

    public void OnPointerExit()
    {
        SetMaterial(false);                                   // Mengatur material kembali ke InactiveMaterial
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);                    // Menghentikan coroutine jika pointer keluar
            gazeCoroutine = null;                            // Reset nilai coroutine
        }
    }

    private IEnumerator StartGazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration);       // Menunggu selama gazeDuration
        audioSource.PlayOneShot(ChoiceItSfx);                // Memainkan efek suara saat pilihan diaktifkan
        HideObject();                                        // Memanggil fungsi untuk menyembunyikan objek
        ShowObject();                                        // Memanggil fungsi untuk menampilkan objek lain
        OnPointerExit();                                     // Simulasikan pointer keluar setelah aksi selesai
    }

    public void HideObject()
    {
        gazeObjects[0].SetActive(false);                    // Menyembunyikan objek pertama dalam array
    }

    public void ShowObject()
    {
        gazeObjects[1].SetActive(true);                     // Menampilkan objek kedua dalam array
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)     // Memastikan kedua material sudah diatur
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial; // Mengatur material berdasarkan status gaze
        }
    }




}