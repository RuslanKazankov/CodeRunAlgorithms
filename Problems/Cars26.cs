using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Problems
{
    public class Cars26 : ProblemBase
    {
        public override void Solve()
        {
            string[] inputParams = Console.ReadLine()!.Trim().Split(' ');
            int n = int.Parse(inputParams[0]);
            int k = int.Parse(inputParams[1]);
            int p = int.Parse(inputParams[2]);

            Dictionary<int, List<int>> NumPos = new Dictionary<int, List<int>>(n);
            int[] cars = new int[p];

            for (int i = 0; i < p; i++)
            {
                cars[i] = int.Parse(Console.ReadLine()!);

                if (!NumPos.ContainsKey(cars[i]))
                {
                    NumPos.Add(cars[i], new List<int>());
                }
                NumPos[cars[i]].Add(i);
            }

            int operations = 0;

            List<int> currentCars = new List<int>(k);
            for (int i = 0; i < p; i++)
            {
                if (currentCars.Count < k) // первые k машин
                {
                    if (!currentCars.Contains(cars[i]))
                    {
                        AddBinary(currentCars, cars[i], NumPos);
                        operations++;
                    }
                    else
                    {
                        currentCars.Remove(cars[i]);
                        AddBinary(currentCars, cars[i], NumPos);
                    }
                    NumPos[cars[i]].RemoveAt(0);
                    continue;
                }

                if (currentCars.Contains(cars[i])) // машина уже содержится
                {
                    currentCars.Remove(cars[i]);
                    AddBinary(currentCars, cars[i], NumPos);
                    NumPos[cars[i]].RemoveAt(0);
                    continue;
                }

                // новая машина
                currentCars.Remove(currentCars.Last());
                if (NumPos[cars[i]].Count > 0)
                {
                    NumPos[cars[i]].RemoveAt(0);
                }

                AddBinary(currentCars, cars[i], NumPos);
                operations++;
            }

            Console.WriteLine(operations);
        }

        private static void AddBinary(List<int> list, int value, Dictionary<int, List<int>> dic)
        {
            int mid = 0;
            int left = 0;
            int right = list.Count - 1;
            int trueValue = dic[value].Count > 1 ? dic[value][1] : int.MaxValue;
            while (left <= right)
            {
                mid = (left + right) / 2;
                int trueListMid = dic[list[mid]].Count > 0 ? dic[list[mid]][0] : int.MaxValue;
                if (trueValue < trueListMid)
                {
                    right = mid - 1;
                }
                else if(trueValue > trueListMid)
                {
                    left = mid + 1;
                }
                else
                {
                    list.Insert(mid, value);
                    return;
                }
            }

            list.Insert(left, value);
        }
    }
}
