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
        public int[][] Solve(int[][] sudoku)
        {
            for (int x = 0; x < sudoku.Length; x++)
            {
                for (int y = 0; y < sudoku.Length; y++)
                {

                    List<int> exclude = CheckX(sudoku, y);
                    exclude = AddList(exclude, CheckY(sudoku,x));
                    exclude = AddList(exclude, CheckGrid(sudoku,3,3,x,y));
                }
            }
            return sudoku;
        }

        public List<int> AddList(List<int> list0, List<int> list1) {
            foreach (var item in list1)
            {
                list0.Add(item);
            }
            return list0;
        }

        public List<int> CheckX(int[][] sudoku, int y)
        {
            List<int> exclude = new List<int>(); 
            for (int x = 0; x < sudoku.Length; x++)
            {
                if (sudoku[y][x] != 0) {
                    exclude.Add(sudoku[y][x]);
                }
            }
            return exclude;
        }

        public List<int> CheckY(int[][] sudoku, int x)
        {
            List<int> exclude = new List<int>();
            for (int y = 0; y < sudoku.Length; y++)
            {
                if (sudoku[y][x] != 0)
                {
                    exclude.Add(sudoku[y][x]);
                }
            }
            return exclude;
        }

        public List<int> CheckGrid(int[][] sudoku, int width,int height,int xPos,int yPos) {
            List<int> exclude = new List<int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (sudoku[yPos][xPos] != 0)
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