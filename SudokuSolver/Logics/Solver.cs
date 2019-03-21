using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SudokuSolver.Logics
{
    public class Solver
    {
        /// <summary>
        /// Take a sudoku and solve it.
        /// </summary>
        /// <param name="sudoku"> The unsolved sudoku</param>
        /// <returns></returns>
        public int[][] Solve(int[][] sudoku)
        {
            while (true)
            {
                List<int[][]> sudokuList = SolveSudoku(sudoku);

                if (sudokuList.Count == 1) {
                    break;
                }
            }
            return sudoku;
        }

        public List<int[][]> SolveSudoku(int[][] sudoku) {
            List<int[][]> sudokuList = new List<int[][]>();
            for (int x = 0; x < sudoku.Length; x++)
            {
                for (int y = 0; y < sudoku.Length; y++)
                {
                    if (!CheckIfEmpty(sudoku, x, y))
                    {
                        List<int> exclude = CheckX(sudoku, y);
                        exclude = AddList(exclude, CheckY(sudoku, x));
                        exclude = AddList(exclude, CheckGrid(sudoku, 3, 3, x, y));
                        List<int> toAdd = RemoveNumbers(exclude, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                        foreach (var item in toAdd)
                        {
                            sudoku[y][x] = item;
                            sudokuList.Add(sudoku);
                        }
                    }
                }
            }
            return sudokuList;
        }

        public List<int> RemoveNumbers(List<int> exclude, List<int> toExcludeFrom)
        {
            foreach (var item in exclude)
            {
                try
                {
                    toExcludeFrom.RemoveAt(toExcludeFrom.FindIndex(x => x == item));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            return toExcludeFrom;
        }

        /// <summary>
        /// Check if the given position int the given sudoku is empty.
        /// </summary>
        /// <param name="sudoku"> The sudoku to check </param>
        /// <param name="x"> The x position to check </param>
        /// <param name="y"> The y position to check </param>
        /// <returns></returns>
        public bool CheckIfEmpty(int[][] sudoku, int x, int y) {
            return (sudoku[y][x] != 0);
        }
        /// <summary>
        /// Take a int list(list1) and add it to the end of another list(list0).
        /// </summary>
        /// <param name="list0"> list to add to</param>
        /// <param name="list1"> list to add </param>
        /// <returns></returns>
        public List<int> AddList(List<int> list0, List<int> list1) {
            foreach (var item in list1)
            {
                list0.Add(item);
            }
            return list0;
        }

        /// <summary>
        /// Check every Number in the x coordinate on the given y coordinate 
        /// </summary>
        /// <param name="sudoku"> The sudoku to check </param>
        /// <param name="y"> The y coordinate of the row to check</param>
        /// <returns></returns>
        public List<int> CheckX(int[][] sudoku, int y)
        {
            List<int> exclude = new List<int>();
            for (int x = 0; x < sudoku.Length; x++)
            {
                if (CheckIfEmpty(sudoku, x, y)) {
                    exclude.Add(sudoku[y][x]);
                }
            }
            return exclude;
        }

        /// <summary>
        /// Check every number in the y coordinate on the given x coordinate
        /// </summary>
        /// <param name="sudoku"> The sudoku to check </param>
        /// <param name="x"> The x coordinate of the collumn to check </param>
        /// <returns></returns>
        public List<int> CheckY(int[][] sudoku, int x)
        {
            List<int> exclude = new List<int>();
            for (int y = 0; y < sudoku.Length; y++)
            {
                if (CheckIfEmpty(sudoku, x, y))
                {
                    exclude.Add(sudoku[y][x]);
                }
            }
            return exclude;
        }

        /// <summary>
        /// Check every number of the appropriate grid of the given position of the given height and width
        /// </summary>
        /// <param name="sudoku"> The sudoku to check </param>
        /// <param name="width"> The width of the grid to check </param>
        /// <param name="height"> The height of the grid to check </param>
        /// <param name="xPos"> The x position of the current grid </param>
        /// <param name="yPos"> The y position of the current grid </param>
        /// <returns></returns>
        public List<int> CheckGrid(int[][] sudoku, int width,int height,int xPos,int yPos) {
            List<int> exclude = new List<int>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                
                    if (CheckIfEmpty(sudoku, x + xPos % width, y + yPos % height))
                    {
                        exclude.Add(sudoku[y + yPos % height][x + xPos % width]);
                    }
                }
            }
            return exclude;
        }


        public int[][] Create(int[][] sudoku)
        {
            return sudoku;
        }
    }
}