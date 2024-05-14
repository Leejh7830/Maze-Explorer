using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Explorer
{
    class Board
    {
        const char CIRCLE = '\u25cf'; // (●) 검은 원을 나타냅니다
        public TileType[,] _tile; // 2D 타일정보
        public int _size; // 미로의 크기
        Player _player;

        public int DestY { get; private set; }
        public int DestX { get; private set; }

        public enum TileType
        {
            Base = 0,
            Empty = 1,
            Wall = 2,
        }

        public enum Direction
        {
            DOWN = 0,
            RIGHT = 1,
        }

        // 미로 초기설정
        public void InitializeBoard(int size, Player player)
        {
            // size는 항상 홀수
            if (size % 2 == 0)
                return;

            _size = size;
            _tile = new TileType[_size, _size];
            _player = player;

            DestY = _size - 2;
            DestX = _size - 2;

            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {
            Random rand = new Random();

            #region Fill Empty
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    // 짝수는 벽으로 생성 (가장자리는 항상 벽)
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Base;
                }
            }
            #endregion
            
            #region SideWinder Algorithm
            for (int y = 0; y < _size; y++)
            {
                int rightCount = 1;

                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    
                    if (x == _size - 2 && y == _size - 2)
                        continue;
                    
                    
                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    Direction dir = (Direction)rand.Next(0, 2);
                    switch (dir)
                    {
                        case Direction.DOWN: // 아래
                            int randomIndex = rand.Next(0, rightCount);
                            _tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                            rightCount = 1;
                            break;
                        case Direction.RIGHT: // 우측
                            _tile[y, x + 1] = TileType.Empty;
                            rightCount += 1;
                            break;
                    }
                }
            }
            #endregion
        }

        // 렌더링, 그리는 역할
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    // 플레이어 표시
                    if (y == _player.PosY && x == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    // 목적지 표시
                    else if (y == DestY && x == DestX)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        // 타일유형에 따라 색상구분, 빈 공간은 초록 / 벽은 빨강
        private ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Base:
                    return ConsoleColor.DarkGreen;
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
