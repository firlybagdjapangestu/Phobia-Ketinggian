using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadLevel : MonoBehaviour
{
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    private Renderer _myRenderer;
    [SerializeField] private float gazeDuration = 2.0f;

    [SerializeField] private string nameSceneToLoad;
    public int level;
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
        PlayerPrefs.SetInt("Level", level);
        audioSource.PlayOneShot(gazeItSfx);
        SetMaterial(true);
        gazeCoroutine = StartCoroutine(StartGazeTimer());
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
        PlayerPrefs.SetInt("Level", level);
        LoadScene(nameSceneToLoad);
        OnPointerExit();
    }

    public void LoadScene(string nameScene)
    {
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadSceneAsync(nameScene);
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
