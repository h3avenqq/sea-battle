using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sea_battle
{
    internal static class SeaBattle
    {
        private static string[,] myField = new string[10, 10];
        private static string[,] enemyField = new string[10, 10];
        private static string[,] enemyVisibleField = new string[10, 10];

        static SeaBattle()
        {
            FillEmpties(ref myField);
            FillEmpties(ref enemyField);
            FillEmpties(ref enemyVisibleField);
        }

        public static void Start()
        {
            Menu();
        }

        private static void Menu()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.D3)
            {
                Console.WriteLine("Enter - Начать игру\nC - Управление\nEsc - Выйти");
                key = Console.ReadKey(false);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Gameplay();
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        Console.WriteLine("Управление расстановкой кораблей:");
                        Console.WriteLine("\t↑ - Вверх");
                        Console.WriteLine("\t↓ - Вниз");
                        Console.WriteLine("\t← - Влево");
                        Console.WriteLine("\t→ - Вправо");
                        Console.WriteLine("\tR - Повернуть корабль");
                        Console.WriteLine("Вводить координаты удара нужно в формате \"A 1\"");
                        Console.ReadKey(false);
                        break;
                    case ConsoleKey.Escape:
                        System.Environment.Exit(1);
                        break;
                        
                }
                Console.Clear();
            }
        }

        private static void Gameplay()
        {
            ShipGenerator.EnemyFieldGenerator(ref enemyField);
            ShipGenerator.MyFieldGenerator(ref myField);

            FieldRenderer.Show(myField, "Your field");
            Console.ReadKey(false);

            while (!CheckShips(myField) && !CheckShips(enemyField))
            {
                FieldRenderer.Show(enemyVisibleField, "Enemy field");
                Console.WriteLine("Press any key to continue...",false);
                Console.ReadKey(false);
                PlayerAttack();
                ComputerAttack();
            }

            System.Environment.Exit(1);
        }

        private static void PlayerAttack()
        {
            while (true)
            {
                Console.Write("Ener coordinates of ship: ");
                string[] coordinateText = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                (int x, int y) coordinate = (Convert.ToChar(coordinateText[0]) - '0' - 17, Convert.ToInt32(coordinateText[coordinateText.Length - 1]) - 1);

                if (enemyField[coordinate.y, coordinate.x] == "x")
                {
                    enemyField[coordinate.y, coordinate.x] = "*";
                    enemyVisibleField[coordinate.y, coordinate.x] = "*";
                    FieldRenderer.Show(enemyVisibleField);
                    Console.WriteLine("Press any key to continue...", false);
                    Console.ReadKey(false);
                }
                else
                {
                    enemyVisibleField[coordinate.y, coordinate.x] = "0";
                    FieldRenderer.Show(enemyVisibleField);
                    Console.WriteLine("Press any key to continue...", false);
                    Console.ReadKey(false);
                    break;
                }
            }
        }

        private static void ComputerAttack()
        {
            while(true)
            {
                Random random = new Random();
                (int x, int y) coordinate = (random.Next(11), random.Next(11));
                if (myField[coordinate.y, coordinate.x] == "x")
                {
                    myField[coordinate.y, coordinate.x] = "*";
                    FieldRenderer.Show(myField);
                    Console.WriteLine("Press any key to continue...", false);
                    Console.ReadKey(false);

                }
                else
                {
                    myField[coordinate.y, coordinate.x] = "0";
                    FieldRenderer.Show(myField);
                    Console.WriteLine("Press any key to continue...", false);
                    Console.ReadKey(false);
                    break;
                }
            }
        }


        private static bool CheckShips(string[,] field)
        {
            for(int i = 0; i < field.GetLength(0); i++)
            {
                for(int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == "x")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static void FillEmpties(ref string[,] myField)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    myField[i, j] = "-";
                }
                Console.WriteLine();
            }
        }
    }
}
