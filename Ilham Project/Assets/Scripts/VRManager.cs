using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour
{
    // Tidak diperlukan sementara untuk menghindari bug
    /*[SerializeField] private Transform player; 
    [SerializeField] private GameObject obstaclePrefabs;
    [SerializeField] private Transform[] playerSpawnPosition;
    [SerializeField] private Transform[] ObstacleSpawnPosition;*/
    [SerializeField] private int selectedLevel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] backSound;
    [SerializeField] private AudioClip intructionSfx;

    private void Awake()
    {
        selectedLevel = PlayerPrefs.GetInt("Level", -1); // Default ke -1 jika belum di-set
        /*if (selectedLevel < 0 || selectedLevel >= playerSpawnPosition.Length)
        {
            Debug.LogError("Invalid selectedLevel! Check PlayerPrefs or array size.");
            selectedLevel = 0; // Atur ke nilai default
        }
        playerSpawnPosition[0] = GameObject.Find("Spawn LV1").transform;
        playerSpawnPosition[1] = GameObject.Find("Spawn LV2").transform;
        playerSpawnPosition[2] = GameObject.Find("Spawn LV3").transform;
        playerSpawnPosition[3] = GameObject.Find("Spawn LV4").transform;*/
        //GameObject playerObject = GameObject.Find("Player");
        audioSource.PlayOneShot(intructionSfx);
        audioSource.clip = backSound[selectedLevel];
        audioSource.Play();
    }
    private void Start()
    {
        /*if (player == null)
        {
            Debug.LogError("Player reference is missing!");
            return;
        }

        player.position = playerSpawnPosition[selectedLevel].position;
        GameObject obstacle = Instantiate(obstaclePrefabs,
            ObstacleSpawnPosition[selectedLevel].position,
            ObstacleSpawnPosition[selectedLevel].rotation);*/
    }

}
