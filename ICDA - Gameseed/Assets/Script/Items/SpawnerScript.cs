using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject itemPrefab; // Assign your item prefab in Inspector
    public Transform[] spawnPoints; // Assign 4 or more empty GameObjects in Inspector

    void Start()
    {
        SpawnItems(4);
    }

    void SpawnItems(int count)
    {
        // Copy and shuffle the spawnPoints array
        List<Transform> shuffledPoints = new List<Transform>(spawnPoints);
        Shuffle(shuffledPoints);

        // Spawn 'count' items at unique, shuffled positions
        for (int i = 0; i < count && i < shuffledPoints.Count; i++)
        {
            Instantiate(itemPrefab, shuffledPoints[i].position, Quaternion.identity);
        }
    }

    // Fisher-Yates Shuffle
    void Shuffle(List<Transform> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            Transform temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
