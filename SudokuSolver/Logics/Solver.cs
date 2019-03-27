using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SudokuSolver.Logics
{
    public class Solver
    {
        /// <summary>
        /// Take a sudoku and solve it.
        /// </summary>
        /// <param name="sudoku"> The unsolved sudoku</param>
        /// <returns></returns>
        public int[][] Solve(int[][] sudoku) {
            uint[][] uSudoku = (uint[][])(object)sudoku;
            List<uint[][]> sudokuList = new List<uint[][]> { uSudoku };
            for (int x = 0; x < uSudoku.Length; x++)
            {
                for (int y = 0; y < uSudoku[x].Length; y++)
                {
                    if (!CheckIfEmpty(uSudoku, x,y)) {
                        
                        List<uint[][]> editedSudokus = new List<uint[][]>();
                        foreach (var sudokuItem in sudokuList)
                        {
                            editedSudokus.AddRange(SolveSudoku(sudokuItem, x, y));
                            //Debug.WriteLine(x.ToString() + "-----" + y.ToString());
                        }
                        sudokuList = editedSudokus;
                    }
                }
            }
            

            sudoku = (int[][])(object)sudokuList.Last();
            return sudoku;
        }


        public uint[][] ConvertIntSudoku(int[][] arrayToClone)
        {
            uint[][] newArray = new uint[arrayToClone.Length][];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = (uint[])arrayToClone[i].Clone();
            }
            return newArray;
        }

        public int[][] ConvertuintSudoku(uint[][] arrayToClone)
        {
            int[][] newArray = new int[arrayToClone.Length][];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = (int[])arrayToClone[i].Clone();
            }
            return newArray;
        }

        public List<uint[][]> SolveSudoku(uint[][] sudoku,int x,int y) {
            List<uint[][]> sudokuList = new List<uint[][]>();
            
            if (!CheckIfEmpty(sudoku, x, y))
            {
                List<uint> exclude = CheckX(sudoku, y);
                exclude = AddList(exclude, CheckY(sudoku, x));
                exclude.AddRange(CheckGrid(sudoku, 3, 3, x, y));
                List<uint> toAdd = RemoveNumbers(exclude, new List<uint> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                foreach (var item in toAdd)
                {
                    sudokuList.Add(DeepClone(sudoku));
                }
                for (int i = 0; i < toAdd.Count; i++)
                {
                    sudokuList[i][y][x] = toAdd[i];
                }
            }
            return sudokuList;
        }

        public List<uint> RemoveNumbers(List<uint> exclude, List<uint> toExcludeFrom)
        {
            foreach (var item in exclude)
            {
                if (toExcludeFrom.FindIndex(x => x == item) > -1) {
                    toExcludeFrom.RemoveAt(toExcludeFrom.FindIndex(x => x == item));
                }
            }
            return toExcludeFrom;
        }

        /// <summary>
        /// Check if the given position uint the given sudoku is empty.
        /// </summary>
        /// <param name="sudoku"> The sudoku to check </param>
        /// <param name="x"> The x position to check </param>
        /// <param name="y"> The y position to check </param>
        /// <returns></returns>
        public bool CheckIfEmpty(uint[][] sudoku, int x, int y) {
            return (sudoku[y][x] != 0);
        }

        /// <summary>
        /// Take a uint list(list1) and add it to the end of another list(list0).
        /// </summary>
        /// <param name="list0"> list to add to</param>
        /// <param name="list1"> list to add </param>
        /// <returns></returns>
        public List<uint> AddList(List<uint> list0, List<uint> list1) {
            foreach (var item in list1)
            {
                list0.Add(item);
            }
            return list0;
        }

        /// <summary>
        /// Take a uint list(list1) and add it to the end of another list(list0).
        /// </summary>
        /// <param name="list0"> list to add to</param>
        /// <param name="list1"> list to add </param>
        /// <returns></returns>
        public List<uint[][]> AddList(List<uint[][]> list0, List<uint[][]> list1)
        {
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
        public List<uint> CheckX(uint[][] sudoku, int y)
        {
            List<uint> exclude = new List<uint>();
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
        public List<uint> CheckY(uint[][] sudoku, int x)
        {
            List<uint> exclude = new List<uint>();
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
        public List<uint> CheckGrid(uint[][] sudoku, int width,int height,int xPos,int yPos) {
            List<uint> exclude = new List<uint>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                
                    if (CheckIfEmpty(sudoku, xPos - xPos % width + x, yPos - yPos % height + y))
                    {
                        exclude.Add(sudoku[yPos - yPos % height + y][xPos - xPos % width + x]);
                    }
                }
            }
            return exclude;
        }

        public uint[][] DeepClone(uint[][] arrayToClone)
        {
            uint[][] newArray = new uint[arrayToClone.Length][];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = (uint[])arrayToClone[i].Clone();
            }
            return newArray;
        }

        public int[][] Create(int[][] sudoku)
        {
            return sudoku;
        }
    }
}