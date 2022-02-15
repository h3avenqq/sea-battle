using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sea_battle
{
    internal static class FieldRenderer
    {
        public static void Show(string[,] field, string message = "")
        {
            Console.Clear();
            Console.WriteLine($"\n{message}");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void Show(List<(int, int)> ship, string[,] field, string message = "")
        {
            Console.Clear();
            Console.WriteLine($"\n{message}");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0;j < 10; j++)
                {
                    string temp = "-";
                    foreach(var kj in ship)
                    {
                        if (kj.Item1 == i && kj.Item2 == j)
                        {
                            temp = "x";
                            break;
                        }
                    }

                    if (temp == "x")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(temp);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(field[i,j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
