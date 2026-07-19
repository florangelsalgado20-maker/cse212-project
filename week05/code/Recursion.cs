using System;
using System.Collections.Generic;

namespace CSE212
{
    public class Recursion
    {
        /// <summary>
        /// Calcula la suma de los cuadrados de los números desde n hasta 1 de forma recursiva.
        /// </summary>
        public static int SumSquaresRecursive(int n)
        {
            if (n <= 0)
            {
                return 0;
            }
            return (n * n) + SumSquaresRecursive(n - 1);
        }

        /// <summary>
        /// Genera todas las permutaciones posibles de un tamaño específico a partir de un conjunto de letras.
        /// </summary>
        public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
        {
            if (word.Length == size)
            {
                results.Add(word);
                return;
            }

            for (int i = 0; i < letters.Length; i++)
            {
                string currentLetter = letters[i].ToString();
                string remainingLetters = letters.Remove(i, 1);
                PermutationsChoose(results, remainingLetters, size, word + currentLetter);
            }
        }

        /// <summary>
        /// Calcula el número de formas de subir una escalera de 's' escalones, dando 1, 2 o 3 pasos a la vez.
        /// Utiliza memoización para optimizar el rendimiento de O(3^n) a O(n).
        /// </summary>
        public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
        {
            if (remember == null)
            {
                remember = new Dictionary<int, decimal>();
            }

            if (s == 0) return 1;
            if (s == 1) return 1;
            if (s == 2) return 2;
            if (s == 3) return 4;

            if (remember.ContainsKey(s))
            {
                return remember[s];
            }

            decimal ways = CountWaysToClimb(s - 1, remember) + 
                           CountWaysToClimb(s - 2, remember) + 
                           CountWaysToClimb(s - 3, remember);

            remember[s] = ways;
            return ways;
        }

        /// <summary>
        /// Genera todas las combinaciones binarias posibles reemplazando los comodines ('*') con '0' y '1'.
        /// </summary>
        public static void WildcardBinary(string pattern, List<string> results)
        {
            if (!pattern.Contains('*'))
            {
                results.Add(pattern);
                return;
            }

            int wildcardIndex = pattern.IndexOf('*');
            
            string patternWithZero = pattern.Substring(0, wildcardIndex) + "0" + pattern.Substring(wildcardIndex + 1);
            string patternWithOne = pattern.Substring(0, wildcardIndex) + "1" + pattern.Substring(wildcardIndex + 1);

            WildcardBinary(patternWithZero, results);
            WildcardBinary(patternWithOne, results);
        }

        /// <summary>
        /// Resuelve un laberinto utilizando un algoritmo de búsqueda en profundidad (DFS) con retroceso.
        /// </summary>
        public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<(int, int)>? currPath = null)
        {
            if (currPath == null)
            {
                currPath = new List<(int, int)>();
            }

            if (maze.IsEnd(x, y))
            {
                currPath.Add((x, y));
                results.Add(currPath.AsString());
                currPath.RemoveAt(currPath.Count - 1);
                return;
            }

            currPath.Add((x, y));

            if (maze.IsValidMove(currPath, x + 1, y)) SolveMaze(results, maze, x + 1, y, currPath);
            if (maze.IsValidMove(currPath, x - 1, y)) SolveMaze(results, maze, x - 1, y, currPath);
            if (maze.IsValidMove(currPath, x, y + 1)) SolveMaze(results, maze, x, y + 1, currPath);
            if (maze.IsValidMove(currPath, x, y - 1)) SolveMaze(results, maze, x, y - 1, currPath);

            currPath.RemoveAt(currPath.Count - 1);
        }
    }
}
