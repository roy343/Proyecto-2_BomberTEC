using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


public class LevelGeneration : MonoBehaviour //Se definen las probabilidades iniciales (esto define que tan poblado estara el mapa)
{
    [Range(0, 100)]
    public int iniChance; 

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]

    public int numR;

    private int[,] terrainMap; //Matriz que representa el mapa. Los 1 y 0 en X y Y representa que el tile esta "vivo" o "activo" lo que le dice al programa que ponga un tile

    public Vector3Int tmapSize; // Tamano del mapa


    public Tilemap topMap;//Tilemaps del piso y las paredes
    public Tilemap botMap;

    public Tile topTile;
    public Tile botTile;

    int width;//Largo y ancho del mapa
    int height;

    public void doSim(int numR)//Inicializa el mapa
    {
        clearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;
        
        if (terrainMap == null)//Crea una nueva matriz para el mapa
        {
            terrainMap = new int[width, height];
            initPos();//Llama al encargado de dar 1 y 0 a la matriz
        }

        for(int i = 0; i < numR; i++)
        {
            terrainMap = genTilePos(terrainMap);// Algoritmo para generar mapas mas organicos
        }

        for(int x = 0; x < width; x++)//Loop que recorre la matriz
        {
            for (int y = 0; y < height; y++)
            {
                if(terrainMap[x,y] == 1)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);//Le da tiles al mapa
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                }
            }
        }
    }

    public int[,] genTilePos(int[,] oldMap)// Este algoritmo modifica el terrainMap para darle una apariencia mas organica
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach(var b in myB.allPositionsWithin)
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
                    if (neighb < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }
                if (oldMap[x, y] == 0)
                {
                    if (neighb > deathLimit) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        return newMap;
    }
    public void initPos()//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0; // Asigna los 1 y 0 de forma aleatoria
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//Crea el mapa con click izquierdo
        {
            doSim(numR);
        }

        if (Input.GetMouseButtonDown(1))//Lo destruye con click derecho
        {
            clearMap(true);
        }
    }

    public void clearMap(bool complete)//Funcion para borrar el mapa
    {
        topMap.ClearAllTiles();

        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}
