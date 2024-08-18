using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class TravelingSalesmanProblem
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] cities = { "a", "b", "c", "d", "e", "f" };
            double[,] distances = {
            {0, 5, 7.08, 16.55, 15.52, 18},
            {5, 0, 5, 11.7, 11.05, 14.32},
            {7.08, 5, 0, 14, 14.32, 18.38},
            {16.55, 11.7, 14, 0, 3, 7.62},
            {15.52, 11.05, 14.32, 3, 0, 5},
            {18, 14.32, 18.38, 7.62, 5, 0}
        };

            int start = 0; // Thành phố xuất phát là 'a'

            List<int> path;
            double shortestPathLength = FindShortestPath(start, distances, out path);

            Console.WriteLine($"Độ dài đường đi ngắn nhất là: {shortestPathLength}");
            Console.Write("Quy trình đường đi: ");
            foreach (int city in path)
            {
                Console.Write($"{cities[city]} ");
            }
        }

        public static double FindShortestPath(int start, double[,] distances, out List<int> path)
        {
            int n = distances.GetLength(0);
            int endState = (1 << n) - 1; // Bit mask to represent all cities visited

            double[,] dp = new double[n, 1 << n];
            int[,] parent = new int[n, 1 << n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 1 << n; j++)
                {
                    dp[i, j] = -1;
                    parent[i, j] = -1;
                }
            }

            double result = TSP(start, 1 << start, start, distances, dp, parent);

            int state = 1 << start;
            path = new List<int>();
            path.Add(start);
            while (state != endState)
            {
                int next = parent[start, state];
                path.Add(next);
                start = next;
                state = state | (1 << next);
            }

            return result;
        }

        public static double TSP(int i, int mask, int start, double[,] distances, double[,] dp, int[,] parent)
        {
            int n = distances.GetLength(0);
            int endState = (1 << n) - 1;

            if (mask == endState)
            {
                return distances[i, start];
            }

            if (dp[i, mask] != -1)
            {
                return dp[i, mask];
            }

            double ans = double.MaxValue;
            int index = -1;

            for (int j = 0; j < n; j++)
            {
                if ((mask & (1 << j)) == 0)
                {
                    double newAns = distances[i, j] + TSP(j, mask | (1 << j), start, distances, dp, parent);
                    if (newAns < ans)
                    {
                        ans = newAns;
                        index = j;
                    }
                }
            }

            parent[i, mask] = index;
            return dp[i, mask] = ans;
        }
    }
}
