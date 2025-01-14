using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArea : MonoBehaviour
{
    public Material[] skyboxBank;
    public GameObject[] area;
    // Start is called before the first frame update
    void Start()
    {
        area[Random.Range(0, area.Length)].SetActive(true);
        RenderSettings.skybox = skyboxBank[Random.Range(0, skyboxBank.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
