using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    private const float gazeDuration = 2f;  // Waktu gaze sebelum dihancurkan (2 detik)

    private Renderer _myRenderer;
    private Vector3 _startingPosition;

    private Coroutine gazeCoroutine;  // Coroutine untuk mengelola waktu gaze
    private SpawningPoint spawningPoint;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gazeItSfx;
    [SerializeField] private AudioClip ChoiceItSfx;

    public void Start()
    {
        spawningPoint = GetComponentInParent<SpawningPoint>();
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }

    public void OnPointerEnter()
    {
        SetMaterial(true);
        audioSource.PlayOneShot(gazeItSfx);
        // Mulai coroutine untuk menghitung waktu gaze
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);  // Jika ada coroutine yang sedang berjalan, berhenti
        }
        gazeCoroutine = StartCoroutine(GazeTimer());
    }

    public void OnPointerExit()
    {
        SetMaterial(false);

        // Jika pointer keluar, reset timer
        if (gazeCoroutine != null)
        {
            StopCoroutine(gazeCoroutine);
            gazeCoroutine = null;
        }
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }

    private IEnumerator GazeTimer()
    {
        // Menunggu selama 'gazeDuration' detik
        yield return new WaitForSeconds(gazeDuration);

        // Setelah 2 detik, hancurkan objek
        audioSource.PlayOneShot(ChoiceItSfx);
        yield return new WaitForSeconds(0.5f);
        spawningPoint.totalCount++;
        spawningPoint.SpawnSurvey();
        gameObject.SetActive(false);
    }
}
