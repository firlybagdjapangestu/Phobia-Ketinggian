using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPoint : MonoBehaviour
{
    public GameObject objectToSpawn;                 // Objek yang akan di-spawn
    public GameObject surveyPrefabs;                 // Prefab survei yang akan di-spawn
    public int spawnCount = 5;                       // Jumlah objek yang akan di-spawn di awal
    public int totalCount;                           // Total objek yang sudah di-spawn

    [SerializeField] private float maxPositionX;     // Nilai maksimum posisi X untuk spawn
    [SerializeField] private float minPositionX;     // Nilai minimum posisi X untuk spawn
    [SerializeField] private float maxPositionY;     // Nilai maksimum posisi Y untuk spawn
    [SerializeField] private float minPositionY;     // Nilai minimum posisi Y untuk spawn
    [SerializeField] private float maxPositionZ;     // Nilai maksimum posisi Z untuk spawn
    [SerializeField] private float minPositionZ;     // Nilai minimum posisi Z untuk spawn

    void Start()
    {
        // Spawn beberapa objek secara acak saat game dimulai
        for (int i = 0; i < spawnCount; i++)          // Loop untuk spawn sesuai jumlah spawnCount
        {
            SpawnObject();                            // Panggil fungsi SpawnObject untuk setiap objek
        }
    }

    private void SpawnObject()
    {
        // Dapatkan posisi acak berdasarkan variabel min dan max
        float randomX = Random.Range(minPositionX, maxPositionX);    // Posisi X acak
        float randomY = Random.Range(minPositionY, maxPositionY);    // Posisi Y acak
        float randomZ = Random.Range(minPositionZ, maxPositionZ);    // Posisi Z acak

        // Tentukan posisi spawn relatif terhadap posisi GameObject ini
        Vector3 randomLocalPosition = new Vector3(randomX, randomY, randomZ);    // Buat vektor posisi lokal acak

        // Spawn objek di posisi asal GameObject ini
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);    // Spawn objek

        // Set sebagai child dari GameObject ini
        spawnedObject.transform.SetParent(gameObject.transform);     // Tetapkan parent objek yang di-spawn

        // Atur posisi lokal relatif terhadap parent
        spawnedObject.transform.localPosition = randomLocalPosition; // Set posisi lokal objek yang di-spawn
    }

    public void SpawnSurvey()
    {
        if (totalCount == spawnCount)                                // Cek apakah jumlah total spawn sudah mencapai spawnCount
        {
            GameObject surveyObject = Instantiate(surveyPrefabs, transform.position, transform.rotation);  // Spawn surveyPrefabs
            surveyObject.transform.SetParent(gameObject.transform);  // Tetapkan surveyObject sebagai child dari GameObject ini
        }
    }
}
