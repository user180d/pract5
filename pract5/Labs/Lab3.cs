﻿using Microsoft.AspNetCore.Components.Forms;

namespace pract5.Labs
{
    public class Lab3
    {
        public string[] input { get; set; }
        static bool IsRunoff(int[,] matr, int i, int j, int di, int dj)
        {
            int i_new = i + di;
            int j_new = j + dj;
            if (!CellIsCorrect(matr, i_new, j_new))
                return false;
            return matr[i, j] > matr[i_new, j_new];
        }

        static bool HasRunoff(int[,] matr, int i, int j)
        {
            return IsRunoff(matr, i, j, -1, 0) ||
                 IsRunoff(matr, i, j, 1, 0) ||
                 IsRunoff(matr, i, j, 0, 1) ||
                 IsRunoff(matr, i, j, 0, -1);
        }

        static bool HasEqual(int[,] matr, int i, int j)
        {
            return IsEqual(matr, i, j, -1, 0) ||
                 IsEqual(matr, i, j, 1, 0) ||
                 IsEqual(matr, i, j, 0, 1) ||
                 IsEqual(matr, i, j, 0, -1);
        }

        static bool IsGoodEqual(int[,] matr, int i, int j, int di, int dj, List<Tuple<int, int>> badList)
        {
            return IsEqual(matr, i, j, di, dj) && !badList.Contains(new Tuple<int, int>(i + di, j + dj));
        }

        static bool HasGoodPrevEqual(int[,] matr, int i, int j, List<Tuple<int, int>> badList)
        {
            return IsGoodEqual(matr, i, j, -1, 0, badList) || IsGoodEqual(matr, i, j, 0, -1, badList);
        }

        static bool CellIsCorrect(int[,] matr, int i, int j)
        {
            return i >= 0 && i < matr.GetLength(0) && j >= 0 && j < matr.GetLength(1);
        }

        static bool IsEqual(int[,] matr, int i, int j, int di, int dj)
        {
            int i_new = i + di;
            int j_new = j + dj;
            if (!CellIsCorrect(matr, i_new, j_new))
                return false;
            return matr[i, j] == matr[i_new, j_new];
        }

        static bool HasNextEqual(int[,] matr, int i, int j)
        {
            return IsEqual(matr, i, j, 0, 1) || IsEqual(matr, i, j, 1, 0);
        }

        static int GetCellStatus(int[,] matr, int i, int j, List<Tuple<int, int>> badList)
        {
            if (HasRunoff(matr, i, j))
                return 1;
            else
            {
                if (!HasEqual(matr, i, j))
                    return -1;
                else
                {
                    if (HasGoodPrevEqual(matr, i, j, badList))
                        return 1;
                    else
                    {
                        if (HasNextEqual(matr, i, j))
                            return 0;
                        else
                            return -1;
                    }
                }
            }
        }

        public List<string> start()
        {
            List<string> list = new List<string>();
            try
            {
                string[] lines = input;
                string[] firstLine = lines[0].Split();
                int n = int.Parse(firstLine[0]);
                int m = int.Parse(firstLine[1]);
                if (n > 100 || m > 100)
                {
                    Console.WriteLine("Input sizes are too high");
                    return null;
                }

                // Initialize the matrix
                int[,] matr = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    string[] temp = lines[i + 1].Split();
                    for (int j = 0; j < m; j++)
                    {
                        matr[i, j] = int.Parse(temp[j]);
                        if (matr[i, j] > 10000)
                        {
                            Console.WriteLine("One of squares are too big");
                            return null;
                        }
                    }
                }

                List<Tuple<int, int>> badList = new List<Tuple<int, int>>();
                int counter = 0;

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        int st = GetCellStatus(matr, i, j, badList);
                        switch (st)
                        {
                            case -1:
                                counter++;
                                break;
                            case 0:
                                badList.Add(new Tuple<int, int>(i, j));
                                break;
                        }
                    }

                }
                list.Add(counter.ToString());
                return list; 
            }
            catch (Exception e)
            {
                list.Add(e.Message);
                return list;
            }
        }
    }
}
