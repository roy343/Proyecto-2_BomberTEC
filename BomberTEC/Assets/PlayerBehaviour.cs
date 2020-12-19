using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    
    public float health;
    public float MovementSpeed;
    public Tile wallTile;
    public Tile barrel;
    public GameObject expPrefab;
    public Tilemap tilemap;
    [SerializeField] private Text healthText;

    public void UpdateHealth()
    {
        healthText.text = health.ToString("0");
    }

    void Update()
    {

        UpdateHealth();
        movePlayer();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void movePlayer()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * MovementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion") ||health > 1)
        {
            health -= 1;
        }
        if (collision.CompareTag("Potion"))
        {
            health += 1;
        }
        if (collision.CompareTag("Shoe"))
        {
            MovementSpeed += 3;
        }
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCells(originCell);
        if(ExplodeCells(originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(2, 0, 0));
        }
        if(ExplodeCells(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(0, 2, 0));
        }
        if(ExplodeCells(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(-2, 0, 0));
        }
        if(ExplodeCells(originCell + new Vector3Int(0, -1, 0))) 
        {
            ExplodeCells(originCell + new Vector3Int(0, -2, 0));
        }
        



    }

    bool ExplodeCells(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if(tile == wallTile)
        {
            return false;

        }
        if(tile == barrel)
        {
            tilemap.SetTile(cell, null);
            
        }

       Vector3 pos = tilemap.GetCellCenterWorld(cell);
       Instantiate(expPrefab, pos, Quaternion.identity);
       return true;

    }
}
