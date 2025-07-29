using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WallSortingGenerator : MonoBehaviour
{
    public Tilemap wallTilemap;
    public GameObject sortingPointPrefab;

    void Start()
    {
        foreach (Vector3Int pos in wallTilemap.cellBounds.allPositionsWithin)
        {
            if (wallTilemap.HasTile(pos))
            {
                Vector3 worldPos = wallTilemap.CellToWorld(pos) + new Vector3(0f, 0.5f, 0);
                Instantiate(sortingPointPrefab, worldPos, Quaternion.identity, transform);
            }
        }
    }
}
