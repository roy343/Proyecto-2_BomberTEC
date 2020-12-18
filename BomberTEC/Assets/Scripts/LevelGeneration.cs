using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

/// <summary>
/// Clase de generacion aleatoria de niveles
/// </summary>
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
    /// <summary>
    /// Dosim inicializa el mapa, se encarga de hacer la simulacion y asignar los tiles al mapa
    /// </summary>
    /// <param name="numR"></param>
    public void doSim(int numR)
    {
        clearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;
        
        if (terrainMap == null)//Crea una nueva matriz para el mapa
        {
            terrainMap = new int[width, height];
            initPos();//Llama al encargado de dar 1 y 0 a la matriz
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
    /// <summary>
    /// Le da a la matriz terrainMap 1 y 0 en sus posiciones
    /// </summary>
    public void initPos()//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(y == 0 || x == 0 || x == 19 || y == 9)
                {
                    terrainMap[x, y] = 1;
                }
                else if (x == 18 || x == 1 || x == 10 || y == 1 || y == 8 || y == 5)
                {
                    terrainMap[x, y] = 0;
                }
                else
                {
                    terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0; // Asigna los 1 y 0 de forma aleatoria
                }
            }
        }
    }
    /// <summary>
    /// El mouse controla la generacion de niveles
    /// </summary>
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
    /// <summary>
    /// Borra el mapa
    /// </summary>
    /// <param name="complete"></param>
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
