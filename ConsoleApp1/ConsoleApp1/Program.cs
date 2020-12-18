using System;
using System.Collections.Generic;
using System.Linq;

namespace A_Star
{
    class Program
    {
        static void initPos(int[,] terrainMap)//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (y == 0 || x == 0 || x == 19 || y == 9)
                    {
                        terrainMap[x, y] = 1;
                    }
                    else if (x == 18 || x == 1 || x == 10 || y == 1 || y == 8 || y == 5)
                    {
                        terrainMap[x, y] = 0;
                    }
                    else
                    {
                        terrainMap[x, y] = new Random().Next(2) < 1 ? 1 : 0; // Asigna los 1 y 0 de forma aleatoria
                    }
                }
            }
            Backtraking back = new Backtraking();
            if(back.init(terrainMap) == false)
            {
                initPos(terrainMap);
            }
        }
        static void print_map(int[,] terrainMap)//Recorre terrainMap y le asigna un 1 o un 0 a cada posicion de la matriz
        {
            for (int x = 0; x < 20; x++)
            {
                Console.Write("[");
                for (int y = 0; y < 10; y++)
                {
                    Console.Write(terrainMap[x,y]);
                    Console.Write(" , ");
                }
                Console.Write("]\n");
            }
        }
        private static List<Tile> GetWalkableTiles(int[,] terrainMap, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };
            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            for(int i=0; i< possibleTiles.Count; i++)
            {
                Tile pos = possibleTiles[i];
                if (pos.X < 0 || pos.Y < 0 || pos.X > 19 || pos.Y > 9 || terrainMap[pos.Y, pos.X] != 0)
                {
                    possibleTiles.Remove(pos);
                    i--;
                }
            }
            return possibleTiles;
        }
        static void a_star(int[,] terrainMap_O, int star_x, int star_y, int fin_x, int fin_y)
        {
            int[,] terrainMap = terrainMap_O;

            Tile start = new Tile();
            start.Y = star_y;
            start.X = star_x;

            var finish = new Tile();
            finish.Y = fin_y;
            finish.X = fin_x;

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option. 
                    Tile tile = checkTile;
                    List<Tile> resultado = new List<Tile>();
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
                            foreach (Tile pos in resultado)
                            {
                                Console.WriteLine("X: "+pos.X+"     Y: "+pos.Y);
                            }
                            Console.WriteLine("Map looks like :");
                            print_map(terrainMap);
                            Console.WriteLine("Done!");
                            return;
                        }
                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(terrainMap, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                            Console.WriteLine(activeTiles.Count);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }
            Console.WriteLine("No Path Found!");
        }
        static void Main(string[] args)
        {
            int[,] terrainMap;
            terrainMap = new int[20, 10];
            initPos(terrainMap);
            print_map(terrainMap);
            //a_star(terrainMap,0,0,7,7);

        }
    }
}

class Tile
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Cost { get; set; }
    public int Distance { get; set; }
    public int CostDistance => Cost + Distance;
    public Tile Parent { get; set; }

    //The distance is essentially the estimated distance, ignoring walls to our target. 
    //So how many tiles left and right, up and down, ignoring walls, to get there. 
    public void SetDistance(int targetX, int targetY)
    {
        this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}

class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public string status { get; set; }
    public Node Parent { get; set; }
    public List<Node> sons { get; set; }
    public void have_sons(int[,] terrainMap)
    {
        this.sons = new List<Node>();
        Node pos;
        this.status = "No";
        if (terrainMap[this.X + 1,this.Y] == 0)
        {
            pos = new Node();
            pos.X = this.X + 1;
            pos.Y = this.Y;
            pos.Parent = this;
            this.sons.Add(pos);
        }
        if (terrainMap[this.X - 1, this.Y] == 0)
        {
            pos = new Node();
            pos.X = this.X - 1;
            pos.Y = this.Y;
            pos.Parent = this;
            this.sons.Add(pos);
        }
        if (terrainMap[this.X, this.Y + 1] == 0)
        {
            pos = new Node();
            pos.X = this.X;
            pos.Y = this.Y + 1;
            pos.Parent = this;
            this.sons.Add(pos);
        }
        if (terrainMap[this.X, this.Y - 1] == 0)
        {
            pos = new Node();
            pos.X = this.X;
            pos.Y = this.Y - 1;
            pos.Parent = this;
            this.sons.Add(pos);
        }
    }
}

class Backtraking
{
    public int Nodes { get; set; }
    public int Visited { get; set; }
    public Node[,] matrix { get; set; }
    public bool init(int[,] terrainMap)
    {
        this.matrix = new Node[20,10];
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (terrainMap[x, y] == 0) {
                    this.matrix[x, y] = new Node();
                    this.matrix[x, y].X = x;
                    this.matrix[x, y].Y = y;
                    this.matrix[x, y].have_sons(terrainMap);
                    this.Nodes++;
                }
            }
        }
        Node current = new Node();
        Node aux = new Node();
        current.X = 1;
        current.Y = 1;
        while (true)
        {
            Console.WriteLine("Node: " + current.X + ", " + current.Y);
            if (this.matrix[current.X,current.Y].Parent == null && this.matrix[current.X, current.Y].sons.Count == 0)
            {
                this.Visited++;
                if (this.Nodes == this.Visited)
                {
                    Console.WriteLine("True");
                    return true;
                }
                else
                {
                    Console.WriteLine("Nodes: " + this.Nodes);
                    Console.WriteLine("Visited: " + this.Visited);
                    Console.WriteLine("False");
                    return false;
                }
            }
            if(this.matrix[current.X, current.Y].sons.Count == 0)
            {
                this.matrix[current.X, current.Y].status = "Yes";
                current = this.matrix[current.X, current.Y].Parent;
            }
            else
            {
                if (this.matrix[this.matrix[current.X, current.Y].sons.First().X, this.matrix[current.X, current.Y].sons.First().Y].status == "No") {
                    this.matrix[current.X, current.Y].status = "Yes";
                    aux = this.matrix[current.X, current.Y].sons.First();
                    this.matrix[aux.X, aux.Y].Parent = current;
                    this.matrix[current.X, current.Y].sons.RemoveAt(0);
                    current = aux;
                    this.Visited++;
                }
                else
                {
                    this.matrix[current.X, current.Y].sons.RemoveAt(0);
                }
            }
        }
    }
}