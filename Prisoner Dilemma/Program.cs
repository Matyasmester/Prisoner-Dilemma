using System;

namespace Prisoner_Dilemma 
{
    internal class Program
    {
        private const int nPrisoners = 100;
        private const int MaxTries = 50;

        private static Random rng = new();

        private static List<int> boxes = new List<int>();

        static void Main()
        {
            int GameRounds = 10000;

            int won = 0;
            int lost = 0;

            FillListTo(boxes, nPrisoners);

            for(int i = 0; i < GameRounds; i++)
            {
                Shuffle(boxes);
                bool isGameWon = PlayGameRound();
                if (isGameWon) won++;
                else { lost++; }
            }

            double successRate = won / Convert.ToDouble(GameRounds);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Average chance of success: ~33%\n");

            Console.WriteLine(nPrisoners + " prisoners, " + GameRounds + " rounds\n");
            Console.WriteLine(won + " games won, " + lost + " games lost, success ratio is " + successRate + "%");
            Console.Read();
        }

        private static bool PlayGameRound()
        {
            int failed = 0;
            int succeeded = 0;

            for (int i = 1; i < nPrisoners; i++)
            {
                // How many boxes a prisoner has opened 
                int tries = 0;

                // Each prisoner goes to the box that has their number on it
                // and sees what's inside it
                int next = boxes[i];

                // The prisoner is guaranteed to be on the loop that ends with
                // their number, so this should never be an infinite loop
                while (true)
                {
                    int currentSlip = next;
                    tries++;

                    // If the slip has their number on it, they're free
                    if (currentSlip == i)
                    {
                        if (tries <= MaxTries) succeeded++;
                        if (tries > MaxTries) failed++;
                        break;
                    }

                    // They go next to the box that the current slip points to
                    next = boxes[currentSlip];
                }
            }

            // No matter how many succeeded, if even one failed, the game is lost
            return failed == 0;
        }

        private static void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void FillListTo(List<int> list, int max) 
        {
            for(int i = 0; i < max; i++)
            {
                list.Add(i);
            }
        }
    }
}