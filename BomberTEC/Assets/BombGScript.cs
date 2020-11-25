using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombGScript : MonoBehaviour
{
    public Tilemap tilemap;

    public GameObject Bomb;

    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(position);
            Vector3 center = tilemap.GetCellCenterWorld(cell);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
