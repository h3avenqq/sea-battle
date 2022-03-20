using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sea_battle
{
    public static class ShipGenerator
    {
        public static void MyFieldGenerator(ref string[,] myField)
        {
            for (int step = 4, h = 1; step >= 1; step--,h++)
            {
                for (int count = 1; count <= h; count++)
                {
                    List<(int x, int y)> ship = new List<(int, int)>();

                    FillShipCoordinatesX(ref ship, step);
                    
                    bool check = true;
                    
                    while (check)
                    {
                        FieldRenderer.Show(ship, myField, "testing...");
                        ConsoleKeyInfo key = Console.ReadKey(false);

                        switch (key.Key)
                        {
                            case ConsoleKey.Enter:
                                if (CheckCoordinates((ship[0].x, ship[0].y)) && CheckCoordinates((ship[ship.Count - 1].x, ship[ship.Count - 1].y)))
                                {
                                    if (GlobalCheck(ship, myField))
                                    {
                                        PlaceShip(ship, ref myField);
                                        check = false;
                                    }
                                }
                                break;

                            case ConsoleKey.RightArrow:
                                MoveShip(ship, (0,1));                               
                                break;

                            case ConsoleKey.LeftArrow:
                                MoveShip(ship, (0,-1));                               
                                break;

                            case ConsoleKey.UpArrow:
                                MoveShip(ship, (-1, 0));                               
                                break;

                            case ConsoleKey.DownArrow:
                                MoveShip(ship, (1, 0));                                
                                break;
                            case ConsoleKey.R:
                                //По сути, это транспонирование, не могу придумать навскидку, как проще сделать))
                                bool position = ship[0].y == ship[ship.Count - 1].y;
                                for (int i = 1; i < ship.Count; i++)
                                {
                                    (int x, int y) transpon = (i, i);
                                    if (position && CheckCoordinates((ship[ship.Count - 1].x - ship.Count + 1, ship[ship.Count - 1].y + ship.Count - 1)))
                                    {                                       
                                        transpon = (-i, i);
                                    }
                                    else if (!position && CheckCoordinates((ship[ship.Count - 1].x + ship.Count - 1, ship[ship.Count - 1].y - ship.Count + 1)))
                                    {                                        
                                        transpon = (i, -i);
                                    }
                                    ship[i] = (ship[i].x + transpon.x, ship[i].y + transpon.y);
                                    //Не могу сказать, что стало прямо так уж лучше, скорее, просто мое видение
                                }
                                break;
                        }
                        FieldRenderer.Show(ship, myField, "testing...");
                    }
                }                
            }            
        }

        public static void MoveShip(List<(int x, int y)> movingShip, (int dX, int dY) direction)
        {
            if (CheckCoordinates((movingShip[0].x+ direction.dX, movingShip[0].y + direction.dY)) && 
                CheckCoordinates((movingShip[movingShip.Count - 1].x+ direction.dX, movingShip[movingShip.Count - 1].y + direction.dY)))
            {
                for (int i = 0; i < movingShip.Count; i++)
                {
                    movingShip[i] = (movingShip[i].x+ direction.dX, movingShip[i].y + direction.dY);
                }
            }
        }
        

        public static void EnemyFieldGenerator(ref string[,] enemyField)
        {
            Random rnd = new Random();
            for (int step = 4, h = 1; step >= 1; step--, h++)
            {
                for (int count = 1; count <= h; count++)
                {
                    while (true)
                    {
                        List<(int x, int y)> ship = new List<(int x, int y)> { (rnd.Next(11), rnd.Next(11)) };
                        int direction = rnd.Next(2);
                        if (direction == 0)
                        {
                            FillShipCoordinatesX(ref ship, step, ship[0].x, ship[0].y);
                            if (CheckCoordinates(ship[0]) && CheckCoordinates(ship[ship.Count - 1]) && GlobalCheck(ship, enemyField))
                            {
                                PlaceShip(ship, ref enemyField);
                                break;
                            }
                        }
                        else
                        {
                            FillShipCoordinatesY(ref ship, step, ship[0].x, ship[0].y);
                            if (CheckCoordinates(ship[0]) && CheckCoordinates(ship[ship.Count - 1]) && GlobalCheck(ship, enemyField))
                            {
                                PlaceShip(ship, ref enemyField);
                                break;
                            }
                        }
                    }
                }
            }
            FieldRenderer.Show(enemyField);
        }

        private static bool CheckCoordinates((int,int) ship)
        {
            return ship.Item1 >= 0 && ship.Item2 >= 0 && ship.Item1 < 10 && ship.Item2 < 10;
        }

        private static bool CheckNeighbours((int, int) ship, string[,] f)
        {
            for (int y = ship.Item2 - 1; y <= ship.Item2 + 1; y++)
            {
                for(int x = ship.Item1 - 1; x <= ship.Item1 + 1; x++)
                {
                    if (y >= 0 && x >= 0 && x < f.GetLength(0) && y < f.GetLength(0))
                    {
                        if (f[x, y] == "x")
                            return false;
                    }
                }
            }
            return true;
        }

        private static bool GlobalCheck(List<(int,int)> ship, string[,] f)
        {
            foreach (var s in ship)
            {
                if (!CheckNeighbours(s, f))
                {
                    return false;
                }
            }
            return true;
        }

        private static void FillShipCoordinatesX(ref List<(int,int)> ship, int k, int x = 0, int y = 0)
        {
            for (int i = x; i < k+x; i++)
            {
                ship.Add((i, y));
            }
        }

        private static void FillShipCoordinatesY(ref List<(int, int)> ship, int k, int x = 0, int y = 0)
        {
            for (int i = y; i < k + y; i++)
            {
                ship.Add((x, i));
            }
        }
        private static void PlaceShip(List<(int x, int y)> ship, ref string[,] field)
        {
            for (int i = 0; i < ship.Count; i++)
            {
                field[ship[i].x, ship[i].y] = "x";
            }
        }
    }
}
