using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public Material InactiveMaterial;                  // Material saat objek tidak digaze
    public Material GazedAtMaterial;                   // Material saat objek digaze
    private const float gazeDuration = 2f;             // Waktu gaze sebelum objek dihancurkan

    private Renderer _myRenderer;                      // Renderer objek untuk mengubah material
    private Coroutine gazeCoroutine;                   // Coroutine untuk mengelola waktu gaze
    private SpawningPoint spawningPoint;               // Referensi ke parent SpawningPoint

    [SerializeField] private AudioSource audioSource;  // Audio source untuk efek suara
    [SerializeField] private AudioClip gazeItSfx;      // Efek suara saat gaze dimulai
    [SerializeField] private AudioClip ChoiceItSfx;    // Efek suara saat gaze selesai

    public void Start()
    {
        spawningPoint = GetComponentInParent<SpawningPoint>(); // Ambil referensi parent SpawningPoint
        _myRenderer = GetComponent<Renderer>();               // Ambil renderer dari objek ini
        SetMaterial(false);                                   // Set material ke inactive di awal
    }

    public void OnPointerEnter()
    {
        SetMaterial(true);                                    // Ubah material ke GazedAtMaterial
        audioSource.PlayOneShot(gazeItSfx);                  // Mainkan efek suara gaze dimulai

        // Mulai coroutine untuk menghitung waktu gaze
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);                    // Hentikan coroutine sebelumnya jika ada
        }
        gazeCoroutine = StartCoroutine(GazeTimer());
    }

    public void OnPointerExit()
    {
        SetMaterial(false);                                  // Kembalikan material ke InactiveMaterial

        // Hentikan timer jika pointer keluar
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);
            gazeCoroutine = null;
        }
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)  // Pastikan material sudah di-set
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial; // Ubah material sesuai status gaze
        }
    }

    private IEnumerator GazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration);       // Tunggu selama gazeDuration

        // Setelah gaze selesai, lakukan tindakan berikut
        audioSource.PlayOneShot(ChoiceItSfx);               // Mainkan suara pilihan
        yield return new WaitForSeconds(0.5f);              // Tunggu sedikit sebelum aksi selanjutnya
        spawningPoint.totalCount++;                         // Tambahkan jumlah total di spawningPoint
        spawningPoint.SpawnSurvey();                        // Panggil metode SpawnSurvey di spawningPoint
        gameObject.SetActive(false);                        // Nonaktifkan objek ini
    }
}
