using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Explorer
{
    class Board
    {
        const char CIRCLE = '\u25cf';
        public int _size;

        public void InitializeBoard(int size)
        {
            _size = size;
        }

        public void Render()
        {
            for (int y = 0; y < _size; y++)
            {
                Console.ForegroundColor = ConsoleColor.Green; // 초록미로

                for (int x = 0; x < _size; x++)
                    Console.Write(CIRCLE);

                Console.WriteLine();
            }
        }
    }
}
