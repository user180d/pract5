﻿using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace pract5.Labs
{
    public class Lab1
    {
        public string input { get; set; }


        static string changeSymToNum(string scenario)
        {
            string toWorkWith = new String(scenario.Where(Char.IsDigit).ToArray());

            if (Regex.IsMatch(scenario, @"^D\d{1,2}$"))
            {
                if (int.Parse(toWorkWith) <= 10)
                {
                    return (int.Parse(toWorkWith) * 2).ToString();
                }
            }
            else if (Regex.IsMatch(scenario, @"^T\d{1,2}$"))
            {
                if (int.Parse(toWorkWith) <= 6)
                {
                    return (int.Parse(toWorkWith) * 3).ToString();
                }
            }
            return null;
        }

        static List<List<string>> solveDnTPrioritisation(List<string> scenario)
        {
            List<List<string>> toFill = new List<List<string>>();
            toFill.Add(new List<string>(scenario));
            string toSave;
            int i = 1;
            if (scenario.Count == 1)
            {
                return toFill;
            }
            if (scenario.Count == 2)
            {
                if (Regex.IsMatch(scenario[0], @"[DT]\d{1,2}"))
                {
                    toFill.Add(new List<string>(scenario));
                    toSave = changeSymToNum(scenario[0]);
                    if (toSave != null)
                    {
                        toFill[1][0] = toSave;
                    }
                    return toFill;
                }
                else { return toFill; }
            }
            else
            {
                if (Regex.IsMatch(scenario[0], @"[DT]\d{1,2}"))
                {

                    toSave = changeSymToNum(scenario[0]);
                    if (toSave != null)
                    {
                        toFill.Add(new List<string>(scenario));
                        toFill[1][0] = toSave;
                        i++;
                    }

                }
                if (Regex.IsMatch(scenario[1], @"[DT]\d{1,2}"))
                {
                    toSave = changeSymToNum(scenario[1]);
                    if (toSave != null)
                    {
                        toFill.Add(new List<string>(scenario));
                        toFill[i][1] = changeSymToNum(scenario[1]);
                        return toFill;
                    }
                }
                return toFill;
            }

        }

        static void FindCombinations(int total, List<int> currentCombination, List<int> numbers, List<List<int>> combinations, int maxNumbers)
        {
            if (total == 0 && currentCombination.Count >= 1 && currentCombination.Count <= maxNumbers)
            {
                combinations.Add(new List<int>(currentCombination));
                return;
            }
            if (total < 0 || currentCombination.Count >= maxNumbers)
            {
                return;
            }

            foreach (int number in numbers)
            {
                currentCombination.Add(number);
                FindCombinations(total - number, currentCombination, numbers, combinations, maxNumbers);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }

        public List<string> start()
        {
            try
            {
                List<string> lstr= new List<string>();
                int maxNumbers = 3;

                List<int> numbers = new List<int>(); // basic points
                List<int> doubleNumbers = new List<int>(numbers.Count); //double points
                List<int> tripleNumbers = new List<int>(numbers.Count); //triple points
                List<int> listOfPoints = new List<int>(); //points

                List<List<int>> combinations = new List<List<int>>();
                List<List<string>> additionalScenarios = new List<List<string>>();
                int n = 0;

                n = int.Parse(input);
                //The task stated that the maximum score that should be processed should be 200, but after working with the task I came to the conclusion that this is not entirely possible because the last hit should be doubled 
                if (n > 150)
                {
                    Console.WriteLine("Your score input is too high!");
                    return null;
                }
                for (int i = 1; i <= 20; i++)
                {
                    numbers.Add(i);
                }
                for (int i = 1; i <= 20; i++)
                {
                    listOfPoints.Add(i);
                }
                //double points
                foreach (int num in numbers)
                {
                    if (!listOfPoints.Contains(num * 2))
                    {
                        listOfPoints.Add(num * 2);
                    }
                    doubleNumbers.Add(num * 2);
                }

                //triple points
                foreach (int num in numbers)
                {
                    if (!listOfPoints.Contains(num * 3))
                    {
                        listOfPoints.Add(num * 3);
                    }
                    tripleNumbers.Add(num * 3);
                }

                listOfPoints.Add(25);
                listOfPoints.Add(50);

                FindCombinations(n, new List<int>(), listOfPoints, combinations, maxNumbers);

                
                    foreach (List<int> combination in combinations)
                    {
                        List<string> formattedCombination = new List<string>();
                        foreach (int num in combination)
                        {
                            if (doubleNumbers.Contains(num))
                            {
                                formattedCombination.Add($"D{num / 2}");
                            }
                            else if (tripleNumbers.Contains(num))
                            {
                                formattedCombination.Add($"T{num / 3}");
                            }
                            else if (num == 50)
                            {
                                formattedCombination.Add($"Bull");
                            }
                            else
                            {
                                formattedCombination.Add(num.ToString());
                            }
                        }

                        //Console.WriteLine(string.Join(" ", formattedCombination)); 
                        string lastVal = formattedCombination[formattedCombination.Count - 1];
                        if (Regex.IsMatch(lastVal, @"^D\d{1,2}$"))
                        {
                            additionalScenarios = solveDnTPrioritisation(formattedCombination);

                            //w.WriteLine(string.Join(" ", formattedCombination));
                            foreach (var a in additionalScenarios)
                            {
                                lstr.Add(string.Join(" ", a));
                            }
                        }
                    }
                    return lstr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
