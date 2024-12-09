using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPoint : MonoBehaviour
{
    public GameObject objectToSpawn; // Objek yang akan di-spawn
    public GameObject surveyPrefabs;
    public int spawnCount = 5;      // Jumlah objek yang akan di-spawn
    public int totalCount;

    [SerializeField] private float maxPositionX;
    [SerializeField] private float minPositionX;
    [SerializeField] private float maxPositionY;
    [SerializeField] private float minPositionY;
    [SerializeField] private float maxPositionZ;
    [SerializeField] private float minPositionZ;

    void Start()
    {
        // Spawn beberapa objek secara acak saat game dimulai
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Dapatkan posisi acak berdasarkan variabel min dan max
        float randomX = Random.Range(minPositionX, maxPositionX);
        float randomY = Random.Range(minPositionY, maxPositionY);
        float randomZ = Random.Range(minPositionZ, maxPositionZ);

        // Tentukan posisi spawn relatif terhadap posisi GameObject ini
        Vector3 randomLocalPosition = new Vector3(randomX, randomY, randomZ);

        // Spawn objek di posisi asal GameObject ini
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        // Set sebagai child dari GameObject ini
        spawnedObject.transform.SetParent(gameObject.transform);

        // Atur posisi lokal relatif terhadap parent
        spawnedObject.transform.localPosition = randomLocalPosition;
    }

    public void SpawnSurvey()
    {
        if(totalCount == spawnCount)
        {
            GameObject surveyObject = Instantiate(surveyPrefabs,transform.position, transform.rotation);
            surveyObject.transform.SetParent(gameObject.transform);
        }
    }
}
