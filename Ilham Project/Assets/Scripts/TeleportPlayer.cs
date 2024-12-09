using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawnPosition; // Array posisi spawn
    [SerializeField] private int selectedLevel;               // Level yang dipilih

    private void Awake()
    {
        // Ambil level yang tersimpan di PlayerPrefs
        selectedLevel = PlayerPrefs.GetInt("Level", 0);
        Debug.Log("Selected Level: " + selectedLevel);

        // Inisialisasi array posisi spawn
        playerSpawnPosition = new Transform[4];

        // Cari objek spawn point berdasarkan nama
        playerSpawnPosition[0] = GameObject.Find("Spawn LV1")?.transform;
        playerSpawnPosition[1] = GameObject.Find("Spawn LV2")?.transform;
        playerSpawnPosition[2] = GameObject.Find("Spawn LV3")?.transform;
        playerSpawnPosition[3] = GameObject.Find("Spawn LV4")?.transform;

        // Debugging untuk memastikan semua posisi spawn ditemukan
        for (int i = 0; i < playerSpawnPosition.Length; i++)
        {
            Debug.Log("Spawn Position [" + i + "]: " +
                (playerSpawnPosition[i] != null ? playerSpawnPosition[i].position.ToString() : "NULL"));
        }
    }

    private void Start()
    {
        // Pindahkan pemain ke posisi spawn yang sesuai
        MovePlayerToSpawn();
    }

    private void MovePlayerToSpawn()
    {
        // Validasi posisi spawn
        if (selectedLevel >= 0 && selectedLevel < playerSpawnPosition.Length &&
            playerSpawnPosition[selectedLevel] != null)
        {
            transform.position = playerSpawnPosition[selectedLevel].position;
            Debug.Log("Player moved to: " + transform.position);
        }
        else
        {
            Debug.LogError("Invalid spawn position for level: " + selectedLevel);
        }
    }
}
