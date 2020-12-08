using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombGScript : MonoBehaviour
{
    public Tilemap tilemap;

    public GameObject Bomb;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("The bomb has beem planted");
            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(position);
            Vector3 center = tilemap.GetCellCenterWorld(cell);

            Instantiate(Bomb, center, Quaternion.identity);
        }
    }
}
