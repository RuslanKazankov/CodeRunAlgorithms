using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Problems
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Distance { get; set; }
        public Player(int distance)
        {
            Distance = distance;
        }

        public Player(Player player)
        {
            X = player.X;
            Y = player.Y;
        }

        public void GoDown()
        {
            Y++;
        }

        public void GoRight()
        {
            X++;
        }
    }

    public class ShortDistance2 : ProblemBase
    {
        public override void Solve()
        {
            string[] nmInput = Console.ReadLine()!.Split(' ');
            int n = int.Parse(nmInput[0]);
            int m = int.Parse(nmInput[1]);

            if (n == 1 && m == 1)
            {
                Console.WriteLine(Console.ReadLine());
                return;
            }

            int[,] arr = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] mInput = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < m; j++)
                {
                    arr[i, j] = int.Parse(mInput[j]);
                }
            }

            List<Player> players = new List<Player>() { new Player(arr[0, 0]) };

            Player? finishPlayer;

            while (true)
            {
                List<Player> newPlayers = new List<Player>();
                foreach (Player player in players)
                {
                    Player pDown = new Player(player);
                    if (pDown.Y != n - 1)
                    {
                        pDown.GoDown();
                        pDown.Distance = player.Distance + arr[pDown.Y, pDown.X];

                        Player? checkPlayerDown = newPlayers.Where(p => p.X == pDown.X && p.Y == pDown.Y).FirstOrDefault();
                        if (checkPlayerDown == null)
                        {
                            newPlayers.Add(pDown);
                        }
                        else if (checkPlayerDown.Distance > pDown.Distance)
                        {
                            newPlayers.Remove(checkPlayerDown);
                            newPlayers.Add(pDown);
                        }

                    }

                    Player pRight = new Player(player);
                    if (pRight.X != m - 1)
                    {
                        pRight.GoRight();
                        pRight.Distance = player.Distance + arr[pRight.Y, pRight.X];

                        Player? checkPlayerRight = newPlayers.Where(p => p.X == pRight.X && p.Y == pRight.Y).FirstOrDefault();

                        if (checkPlayerRight == null)
                        {
                            newPlayers.Add(pRight);
                        }
                        else if (checkPlayerRight.Distance > pRight.Distance)
                        {
                            newPlayers.Remove(checkPlayerRight);
                            newPlayers.Add(pRight);
                        }
                    }
                }
                finishPlayer = newPlayers.Where(p => p.X == m - 1 && p.Y == n - 1).FirstOrDefault();
                if (finishPlayer != null)
                {
                    break;
                }
                players = newPlayers;
            }
            Console.WriteLine(finishPlayer.Distance);
        }
    }
}
