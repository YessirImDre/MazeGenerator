using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGen
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter size of maze: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out int mazeSize);
            Console.WriteLine();

            GenerateMaze(mazeSize, 3);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void GenerateMaze(int size, int complexity)
        {
            Random random = new Random();

            string[,] maze = new string[size, size];

            (bool, string) whereEdge;

            (int, int) startRoom = (0, 0);
            (int, int) bossRoom = (size - 1, size - 1);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((i, j) == startRoom)
                    {
                        int doorsStart = random.Next(1, 4);

                        if (doorsStart == 1)
                        {
                            maze[i, j] = "[st(-SE-)]";
                        }
                        else if (doorsStart == 2)
                        {
                            maze[i, j] = "[st(-S--)]";
                        }
                        else if (doorsStart == 3)
                        {
                            maze[i, j] = "[st(--E-)]";
                        }
                    }
                    else if ((i, j) == bossRoom)
                    {
                        int doorsBoss = random.Next(1, 4);

                        if (doorsBoss == 1)
                        {
                            maze[i, j] = "[bo(N--W)]";
                        }
                        else if (doorsBoss == 2)
                        {
                            maze[i, j] = "[bo(N---)]";
                        }
                        else if (doorsBoss == 3)
                        {
                            maze[i, j] = "[bo(---W)]";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                whereEdge = (true, "NorthWest");
                            }
                            else if (j == size - 1)
                            {
                                whereEdge = (true, "NorthEast");
                            }
                            else
                            {
                                whereEdge = (true, "North");
                            }
                        }
                        else if (j == 0)
                        {
                            whereEdge = (true, "West");
                        }
                        else if (i == size - 1)
                        {
                            if (j == 0)
                            {
                                whereEdge = (true, "SouthWest");
                            }
                            else if (j == size - 1)
                            {
                                whereEdge = (true, "SouthEast");
                            }
                            else
                            {
                                whereEdge = (true, "South");
                            }
                        }
                        else if (j == size - 1)
                        {
                            whereEdge = (true, "East");
                        }
                        else
                        {
                            whereEdge = (false, "Nowhere");
                        }

                        string room = RoomToGen(random, size, whereEdge, complexity);
                        maze[i, j] = "[" + room + "]";
                    }
                }
            }



            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            if (maze[i, j + 1].Contains("W"))
                            {
                                maze[i, j].Remove(5, 1);
                                maze[i, j].Insert(5, "E");
                            }
                            else if (maze[i + 1, j].Contains("N"))
                            {
                                maze[i, j].Remove(4, 1);
                                maze[i, j].Insert(4, "S");
                            }
                        }
                        else if (j == size - 1)
                        {
                            if (maze[i, j - 1].Contains("E"))
                            {
                                maze[i, j].Remove(6, 1);
                                maze[i, j].Insert(6, "W");
                            }
                            else if (maze[i + 1, j].Contains("N"))
                            {
                                maze[i, j].Remove(4, 1);
                                maze[i, j].Insert(4, "S");
                            }
                        }
                        else
                        {
                            if (maze[i, j + 1].Contains("W"))
                            {
                                maze[i, j].Remove(5, 1);
                                maze[i, j].Insert(5, "E");
                            }
                            else if (maze[i, j - 1].Contains("E"))
                            {
                                maze[i, j].Remove(6, 1);
                                maze[i, j].Insert(6, "W");
                            }
                            else if (maze[i + 1, j].Contains("N"))
                            {
                                maze[i, j].Remove(4, 1);
                                maze[i, j].Insert(4, "S");
                            }
                        }
                    }
                    else if ((j == 0 && i !=0) && (j == 0 && i != size - 1))
                    {
                        if (maze[i - 1, j].Contains("S"))
                        {
                            maze[i, j].Remove(3, 1);
                            maze[i, j].Insert(3, "N");
                        }
                        else if (maze[i, j + 1].Contains("W"))
                        {
                            maze[i, j].Remove(5, 1);
                            maze[i, j].Insert(5, "E");
                        }
                        else if (maze[i + 1, j].Contains("N"))
                        {
                            maze[i, j].Remove(4, 1);
                            maze[i, j].Insert(4, "S");
                        }
                    }
                    else if (i == size - 1)
                    {
                        if (j == 0)
                        {
                            if (maze[i - 1, j].Contains("S"))
                            {
                                maze[i, j].Remove(3, 1);
                                maze[i, j].Insert(3, "N");
                            }
                            else if (maze[i, j + 1].Contains("W"))
                            {
                                maze[i, j].Remove(5, 1);
                                maze[i, j].Insert(5, "E");
                            }
                        }
                        else if (j == size - 1)
                        {
                            if (maze[i - 1, j].Contains("S"))
                            {
                                maze[i, j].Remove(3, 1);
                                maze[i, j].Insert(3, "N");
                            }
                            else if (maze[i, j - 1].Contains("E"))
                            {
                                maze[i, j].Remove(6, 1);
                                maze[i, j].Insert(6, "W");
                            }
                        }
                        else
                        {
                            if (maze[i - 1, j].Contains("S"))
                            {
                                maze[i, j].Remove(3, 1);
                                maze[i, j].Insert(3, "N");
                            }
                            else if (maze[i, j + 1].Contains("W"))
                            {
                                maze[i, j].Remove(5, 1);
                                maze[i, j].Insert(5, "E");
                            }
                            else if (maze[i, j - 1].Contains("E"))
                            {
                                maze[i, j].Remove(6, 1);
                                maze[i, j].Insert(6, "W");
                            }
                        }
                    }
                    else if ((j == size - 1 && i != 0) && (j == size - 1 && i != size - 1))
                    {
                        if (maze[i - 1, j].Contains("S"))
                        {
                            maze[i, j].Remove(3, 1);
                            maze[i, j].Insert(3, "N");
                        }
                        else if (maze[i, j - 1].Contains("E"))
                        {
                            maze[i, j].Remove(6, 1);
                            maze[i, j].Insert(6, "W");
                        }
                        else if (maze[i + 1, j].Contains("N"))
                        {
                            maze[i, j].Remove(4, 1);
                            maze[i, j].Insert(4, "S");
                        }
                    }
                    else
                    {
                        if (maze[i - 1, j].Contains("S"))
                        {
                            maze[i, j].Remove(3, 1);
                            maze[i, j].Insert(3, "N");
                        }
                        else if (maze[i + 1, j].Contains("N"))
                        {
                            maze[i, j].Remove(4, 1);
                            maze[i, j].Insert(4, "S");
                        }
                        else if (maze[i, j + 1].Contains("W"))
                        {
                            maze[i, j].Remove(5, 1);
                            maze[i, j].Insert(5, "E");
                        }
                        else if (maze[i, j - 1].Contains("E"))
                        {
                            maze[i, j].Remove(6, 1);
                            maze[i, j].Insert(6, "W");
                        }
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(maze[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static string RoomToGen(Random random, int size, (bool, string) edgeRoom, int complexity)
        {
            string chosenRoom = "";
            int maxDoorCount = random.Next(((size - 1) * 2), ((size * size) - 1));

            int rndDoors;

            if (complexity == 1)
            {
                rndDoors = random.Next(2, 4);
            }
            else if (complexity == 2)
            {
                rndDoors = random.Next(1, 4);
            }
            else
            {
                rndDoors = random.Next(1, 3);
            }

            int rndRoom = random.Next(1, 101);

            if (rndRoom > 40)
            {
                chosenRoom = "no";
            }
            else if (rndRoom <= 40 && rndRoom > 20)
            {
                chosenRoom = "ar";
            }
            else if (rndRoom <= 20 && rndRoom > 10)
            {
                chosenRoom = "tr";
            }
            else if (rndRoom <= 10 && rndRoom > 7)
            {
                chosenRoom = "sh";
            }
            else if (rndRoom <= 7 && rndRoom > 4)
            {
                chosenRoom = "lo";
            }
            else if (rndRoom <= 4 && rndRoom > 2)
            {
                chosenRoom = "ra";
            }
            else if (rndRoom <= 2 && rndRoom > 0)
            {
                chosenRoom = "se";
            }

            if (rndDoors == 3)
            {
                if (edgeRoom.Item1 == true)
                {
                    if (edgeRoom.Item2 == "North")
                    {
                        chosenRoom += "(-SEW)";
                    }
                    else if (edgeRoom.Item2 == "South")
                    {
                        chosenRoom += "(N-EW)";
                    }
                    else if (edgeRoom.Item2 == "East")
                    {
                        chosenRoom += "(NS-W)";
                    }
                    else if (edgeRoom.Item2 == "West")
                    {
                        chosenRoom += "(NSE-)";
                    }
                    else if (edgeRoom.Item2 == "NorthEast")
                    {
                        chosenRoom += "(-S-W)";
                    }
                    else if (edgeRoom.Item2 == "NorthWest")
                    {
                        chosenRoom += "(-SE-)";
                    }
                    else if (edgeRoom.Item2 == "SouthEast")
                    {
                        chosenRoom += "(N--W)";
                    }
                    else if (edgeRoom.Item2 == "SouthWest")
                    {
                        chosenRoom += "(N-E-)";
                    }
                }
                else
                {
                    int where = random.Next(1, 5);

                    if (where == 1)
                    {
                        chosenRoom += "(N-EW)";
                    }
                    else if (where == 2)
                    {
                        chosenRoom += "(NSE-)";
                    }
                    else if (where == 3)
                    {
                        chosenRoom += "(-SEW)";
                    }
                    else if (where == 4)
                    {
                        chosenRoom += "(NS-W)";
                    }
                }
            }
            else if (rndDoors == 2)
            {
                if (edgeRoom.Item1 == true)
                {
                    if (edgeRoom.Item2 == "North")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(-SE-)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(-S-W)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(--EW)";
                        }
                    }
                    else if (edgeRoom.Item2 == "South")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(N-E-)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(N--W)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(--EW)";
                        }
                    }
                    else if (edgeRoom.Item2 == "East")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(NS--)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(N--W)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(-S-W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "West")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(NS--)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(N-E-)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(-SE-)";
                        }
                    }
                    else if (edgeRoom.Item2 == "NorthEast")
                    {
                        chosenRoom += "(-S-W)";
                    }
                    else if (edgeRoom.Item2 == "NorthWest")
                    {
                        chosenRoom += "(-SE-)";
                    }
                    else if (edgeRoom.Item2 == "SouthEast")
                    {
                        chosenRoom += "(N--W)";
                    }
                    else if (edgeRoom.Item2 == "SouthWest")
                    {
                        chosenRoom += "(N-E-)";
                    }
                }
                else
                {
                    int where = random.Next(1, 7);

                    if (where == 1)
                    {
                        chosenRoom += "(NS--)";
                    }
                    else if (where == 2)
                    {
                        chosenRoom += "(N-E-)";
                    }
                    else if (where == 3)
                    {
                        chosenRoom += "(N--W)";
                    }
                    else if (where == 4)
                    {
                        chosenRoom += "(-SE-)";
                    }
                    else if (where == 5)
                    {
                        chosenRoom += "(-S-W)";
                    }
                    else if (where == 6)
                    {
                        chosenRoom += "(--EW)";
                    }
                }
            }
            else if (rndDoors == 1)
            {
                if (edgeRoom.Item1 == true)
                {
                    if (edgeRoom.Item2 == "North")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(-S--)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(--E-)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(---W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "South")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(N---)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(--E-)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(---W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "East")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(N---)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(-S--)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(---W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "West")
                    {
                        int which = random.Next(1, 4);

                        if (which == 1)
                        {
                            chosenRoom += "(N---)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(-S--)";
                        }
                        else if (which == 3)
                        {
                            chosenRoom += "(--E-)";
                        }
                    }
                    else if (edgeRoom.Item2 == "NorthEast")
                    {
                        int which = random.Next(1, 3);

                        if (which == 1)
                        {
                            chosenRoom += "(-S--)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(---W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "NorthWest")
                    {
                        int which = random.Next(1, 3);

                        if (which == 1)
                        {
                            chosenRoom += "(-S--)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(--E-)";
                        }
                    }
                    else if (edgeRoom.Item2 == "SouthEast")
                    {
                        int which = random.Next(1, 3);

                        if (which == 1)
                        {
                            chosenRoom += "(N---)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(---W)";
                        }
                    }
                    else if (edgeRoom.Item2 == "SouthWest")
                    {
                        int which = random.Next(1, 3);

                        if (which == 1)
                        {
                            chosenRoom += "(N---)";
                        }
                        else if (which == 2)
                        {
                            chosenRoom += "(--E-)";
                        }
                    }
                }
                else
                {
                    int where = random.Next(1, 5);

                    if (where == 1)
                    {
                        chosenRoom += "(N---)";
                    }
                    else if (where == 2)
                    {
                        chosenRoom += "(-S--)";
                    }
                    else if (where == 3)
                    {
                        chosenRoom += "(--E-)";
                    }
                    else if (where == 4)
                    {
                        chosenRoom += "(---W)";
                    }
                }
            }

            return chosenRoom;
        }
    }
}
