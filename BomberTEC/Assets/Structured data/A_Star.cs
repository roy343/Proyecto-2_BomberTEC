using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EStar
{
    class A_star
    {
        public static void initPos(int[,] terrainMap)//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    terrainMap[x, y] = new System.Random().Next(2) < 1 ? 1 : 0; // Asigna los 1 y 0 de forma aleatoria
                }
            }
        }
        public static void print_map(int[,] terrainMap)//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
        {
            for (int x = 0; x < 8; x++)
            {
                Debug.Log("[");
                for (int y = 0; y < 8; y++)
                {
                    Debug.Log(terrainMap[x, y]);
                    Debug.Log(" , ");
                }
                Debug.Log("]\n");
            }
        }
        public static List<TileData> GetWalkableTiles(int[,] terrainMap, TileData currentTile, TileData targetTile)
        {
            var possibleTiles = new List<TileData>()
            {
                new TileData { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileData { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new TileData { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileData { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };
            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            for (int i = 0; i < possibleTiles.Count; i++)
            {
                TileData pos = possibleTiles[i];
                if (pos.X < 0 || pos.Y < 0 || pos.X > 7 || pos.Y > 7 || terrainMap[pos.Y, pos.X] != 0)
                {
                    possibleTiles.Remove(pos);
                    i--;
                }
            }
            return possibleTiles;
        }
        public List<TileData> Do_Astar(int[,] terrainMap_O, int star_x, int star_y, int fin_x, int fin_y)
        {
            int[,] terrainMap = terrainMap_O;

            TileData start = new TileData();
            start.Y = star_y;
            start.X = star_x;

            var finish = new TileData();
            finish.Y = fin_y;
            finish.X = fin_x;

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<TileData>();
            activeTiles.Add(start);
            var visitedTiles = new List<TileData>();
            List<TileData> resultado = new List<TileData>();
            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option. 
                    TileData tile = checkTile;
                    Console.WriteLine("Steps...");
                    while (true)
                    {
                        resultado.Add(tile);
                        if (terrainMap[tile.Y, tile.X] == 0)
                        {
                            terrainMap[tile.Y, tile.X] = 7;
                        }
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            resultado.Reverse();
                            foreach (TileData pos in resultado)
                            {
                                //transform.position += new Vector3(pos.X,pos.Y, 0) * Time.deltaTime * 5;
                                Debug.Log("X: " + pos.X + "     Y: " + pos.Y);
                            }
                            Debug.Log("Map looks like :");
                            print_map(terrainMap);
                            Debug.Log("Done!");
                        }
                    }
                }
            }return resultado;
        }
    }
}
class TileData{
    public int X { get; set; }
    public int Y { get; set; }
    public int Cost { get; set; }
    public int Distance { get; set; }
    public int CostDistance => Cost + Distance;
    public TileData Parent { get; set; }
    //The distance is essentially the estimated distance, ignoring walls to our target. 
    //So how many tiles left and right, up and down, ignoring walls, to get there. 
    public void SetDistance(int targetX, int targetY){
        this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}