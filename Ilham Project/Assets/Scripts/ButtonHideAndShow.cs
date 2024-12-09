using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHideAndShow : MonoBehaviour
{
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    private Renderer _myRenderer;

    [Header("Hide and Show Menu Settings")]
    [SerializeField] private float gazeDuration = 2.0f; // Waktu yang dibutuhkan untuk Load Scene
    [SerializeField] private GameObject[] gazeObjects; // Array objek yang dapat di-hide/show

    private Coroutine gazeCoroutine; // Menyimpan coroutine aktif
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gazeItSfx;
    [SerializeField] private AudioClip ChoiceItSfx;

    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(gazeItSfx);
        SetMaterial(true);
        gazeCoroutine = StartCoroutine(StartGazeTimer()); // Memulai coroutine untuk menghitung waktu
    }

    public void OnPointerExit()
    {
        SetMaterial(false);
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine); // Menghentikan coroutine jika pointer keluar
            gazeCoroutine = null;
        }
    }

    private IEnumerator StartGazeTimer()
    {
        yield return new WaitForSeconds(gazeDuration); // Tunggu selama gazeDuration
        audioSource.PlayOneShot(ChoiceItSfx);
        HideObject();
        ShowObject();
        OnPointerExit();
    }

    public void HideObject()
    {
        gazeObjects[0].SetActive(false);
    }

    public void ShowObject()
    {
        gazeObjects[1].SetActive(true);
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
