using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


public class LevelGeneration : MonoBehaviour //Probabilidades de aparicion de bloques
{
    [Range(0, 100)]
    public int iniChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    private int[,] terrainMap;//Matriz que representa el mapa

    public Vector3Int tmapSize;//Tamaño del mapa

    //Vector3 PlayerPosition;

    public Tilemap topMap;//El tilemap superior (donde estan los bloques)
    public Tilemap botMap;//El tilemap inferior (el piso)

    public Tile topTile;// Tiles superiores
    public Tile botTile; // Tiles inferiores

    int width; // Alto y ancho del mapa
    int height;

    public void doSim(int numR)//Funcion para generar el mapa
    {
        clearMap(false);

        width = tmapSize.x;// Define el largo y el ancho
        height = tmapSize.y;
        
        if (terrainMap == null)// Si el mapa esta vacio crea uno nuevo
        {
            terrainMap = new int[width, height];// Crea el mapa encargado de
            initPos();
        }

        for(int i = 0; i < numR; i++)// Aplica el algoritmo de generacion al mapa
        {
            terrainMap = genTilePos(terrainMap);
            Debug.Log(terrainMap[18, 1]);
        }

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(terrainMap[x,y] == 1)// Recorre el mapa posicion por posicion asignandole los tiles a cada una
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                }
            }
        }
        Debug.Log(terrainMap[18, 1]);
    }

    public int[,] genTilePos(int[,] oldMap)// Algoritmo para la generacion organica de niveles
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }
                if(oldMap[x,y] == 1)
                {
                    if (x == 18 & y == 1) newMap[x, y] = 0;
                    if (neighb < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }
                if (oldMap[x, y] == 0)
                {
                    if (neighb > deathLimit & x != 18 & y != 1) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        Debug.Log(newMap[18, 1]);
        return newMap;
    }
    public void initPos()// Esto se encarga de darle a cada posicion en la matriz un 1 o un 0. Dependiendo de lo que tenga le asigna un tile o no
    {
        //PlayerPosition = new Vector3(3f, 5f, 0.0f);
        //Vector3Int cell = botMap.WorldToCell(PlayerPosition);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;//Genera una posibilidad aleatoria de darle a la posicion en la matriz un 1 o un 0
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// Genera el mapa con un click
        {
            doSim(numR);
        }

        if (Input.GetMouseButtonDown(1)) // Lo elimina con click derecho
        {
            clearMap(true);
        }
    }

    public void clearMap(bool complete) // Funcion para borrar el mapa
    {
        topMap.ClearAllTiles();

        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}
