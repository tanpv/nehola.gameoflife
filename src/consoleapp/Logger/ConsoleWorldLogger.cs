﻿using nehola.gameoflife.Entities;
using System;

namespace nehola.gameoflife.Logger
{
    public class ConsoleWorldLogger : IWorldLogger
    {
        public void PrintGeneration(int generation)
        {
            Console.WriteLine(String.Format("Generation #{0}", generation));
        }

        public void PrintSeparator()
        {
            Console.WriteLine("----------------------------------------------------------");
        }

        public void PrintForCell(Cell cell)
        {
            Console.Write(cell.IsAlive ? "#" : " ");
        }

        public void PrintForRow(Int32 row)
        {
            Console.WriteLine();
        }
    }
}