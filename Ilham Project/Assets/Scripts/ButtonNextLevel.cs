using System.Collections;               // Import library untuk IEnumerator dan Coroutine
using System.Collections.Generic;       // Import library untuk koleksi generik
using UnityEngine;                      // Import library utama Unity
using UnityEngine.SceneManagement;      // Import library untuk manajemen scene

public class ButtonNextLevel : MonoBehaviour
{
    public Material InactiveMaterial;              // Material saat objek tidak sedang di-gaze
    public Material GazedAtMaterial;              // Material saat objek sedang di-gaze
    private Renderer _myRenderer;                 // Renderer untuk mengatur material objek
    [SerializeField] private float gazeDuration = 2.0f;          // Waktu yang dibutuhkan untuk mengaktifkan aksi setelah gaze

    [SerializeField] private string nameSceneToLoad;            // Nama scene yang akan diload
    [SerializeField] private int level;                         // Level saat ini yang disimpan di PlayerPrefs
    [SerializeField] private int nextLevel;                     // Jumlah level berikutnya untuk ditambahkan
    private Coroutine gazeCoroutine;              // Menyimpan coroutine aktif untuk gaze timer
    [SerializeField] private AudioSource audioSource;          // AudioSource untuk memainkan efek suara
    [SerializeField] private AudioClip gazeItSfx;              // Efek suara saat gaze dimulai
    [SerializeField] private AudioClip ChoiceItSfx;            // Efek suara saat pilihan diaktifkan

    public void Start()
    {
        level = PlayerPrefs.GetInt("Level");                  // Mendapatkan nilai level dari PlayerPrefs
        _myRenderer = GetComponent<Renderer>();               // Mendapatkan komponen Renderer dari GameObject
        SetMaterial(false);                                   // Mengatur material awal sebagai InactiveMaterial
    }

    public void OnPointerEnter()
    {
        PlayerPrefs.SetInt("Level", level + nextLevel);       // Menyimpan nilai level baru di PlayerPrefs
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
        PlayerPrefs.SetInt("Level", level + nextLevel);       // Menyimpan nilai level baru di PlayerPrefs lagi untuk memastikan
        LoadScene(nameSceneToLoad);                          // Memanggil fungsi untuk load scene
        OnPointerExit();                                     // Simulasikan pointer keluar setelah aksi selesai
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadSceneAsync(nameScene);              // Melakukan load scene secara asynchronous
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)     // Memastikan kedua material sudah diatur
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial; // Mengatur material berdasarkan status gaze
        }
    }
}
