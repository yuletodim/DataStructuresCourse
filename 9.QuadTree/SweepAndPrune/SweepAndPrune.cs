namespace SweepAndPrune
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SweepAndPrune
    {
        static void Main(string[] args)
        {
            var gamers = ReadInput();
            int tickCount = 0;

            var commandLine = Console.ReadLine();
            while (commandLine != string.Empty)
            {
                tickCount++;

                string[] commandArgs = commandLine.Trim().Split(' ');

                ExecuteCommand(commandArgs, gamers);

                var colidedObjects = SweepAndPruneMethod(gamers);
                foreach (var colider in colidedObjects)
                {
                    Console.WriteLine("({0}) {1} collides {2}", tickCount, colider[0].Name, colider[1].Name);
                }

                commandLine = Console.ReadLine();
            }
        }

        private static IList<Gamer[]> SweepAndPruneMethod(IList<Gamer> gamers)
        {
            InsertionSort(gamers);

            var colidedObjects = new List<Gamer[]>();

            for (int i = 0; i < gamers.Count; i++)
            {
                var currentGamer = gamers[i];
                for (int j = i + 1; j < gamers.Count; j++)
                {
                    var candidateColider = gamers[j];
                    if (currentGamer.X2 < candidateColider.X1)
                    {
                        break;
                    }

                    if (currentGamer.Intersects(candidateColider))
                    {
                        Gamer[] colidedNames = new Gamer[2] { currentGamer, candidateColider};
                        colidedObjects.Add(colidedNames);
                    }
                }
            }

            return colidedObjects;
        }

        private static void InsertionSort(IList<Gamer> gamers)
        {
            for (int i = 0; i < gamers.Count; i++)
            {
                int currentIndex = i;
                while(currentIndex > 0 && (gamers[currentIndex - 1].X1 > gamers[currentIndex].X1))
                {
                    var old = gamers[currentIndex];
                    gamers[currentIndex] = gamers[currentIndex - 1];
                    gamers[currentIndex - 1] = old;
                    currentIndex--;
                }
            }
        }

        private static void ExecuteCommand(string[] commandArgs, IList<Gamer> gamers)
        {
            var command = commandArgs[0];
            switch (command)
            {
                case "move":
                    MoveObject(commandArgs[1], int.Parse(commandArgs[2]), int.Parse(commandArgs[3]), gamers);
                    break;
                case "tick":
                    break;
                default:
                    throw new ArgumentException("Non valid command.");
            }
        }

        private static void MoveObject(string name, int newX1, int newY1, IList<Gamer> gamers)
        {
            var gamer = gamers.FirstOrDefault(g => g.Name == name);
            gamer.X1 = newX1;
            gamer.Y1 = newY1;
        }

        private static IList<Gamer> ReadInput()
        {
            var gamers = new List<Gamer>();
            var line = Console.ReadLine();
            string[] commandArgs = line.Trim().Split(' ');
            while (commandArgs[0] != "start")
            {
                var gamer = new Gamer(
                    commandArgs[1],
                    int.Parse(commandArgs[2]),
                    int.Parse(commandArgs[3]));
                gamers.Add(gamer);

                line = Console.ReadLine();
                commandArgs = line.Trim().Split(' ');
            }

            return gamers;
        }
    }
}
