using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSurvey : MonoBehaviour
{
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    private Renderer _myRenderer;
    [SerializeField] private float worriedPoint;
    [SerializeField] private float gazeDuration = 2.0f;
    [SerializeField] private SurveyScript surveyScript;

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
        audioSource.PlayOneShot(ChoiceItSfx);
        surveyScript.AddWoriedPoint(worriedPoint);
        surveyScript.NextQuestion();
        OnPointerExit();
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
