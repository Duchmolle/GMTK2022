using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private TileBase gridTile;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int columsCount;
    [SerializeField] private int rowsCount;

    private void Awake()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int i = 0; i < columsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), gridTile);
            }
        }
    }
}
