using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Assign your item prefab in Inspector
    public Transform[] spawnPoints; // Assign 4 or more empty GameObjects in Inspector
    public PlayerBar bar;
    public SanityItem SaneItem;
    public bool firsttime = true;
    public bool rechargepill = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(bar.currentSanity<30 && firsttime && rechargepill)
        {
            
            firsttime = false;
            rechargepill = false;
            SpawnItems(2);
        }
        else if (bar.currentSanity<30 && rechargepill)
        {
            rechargepill = false;
            SpawnItems(1);
            
        }
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

    public void recharging()
    {
        rechargepill = true;
    }
}
