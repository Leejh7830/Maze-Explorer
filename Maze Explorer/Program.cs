using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int WAIT_TICK = 1000 / 30;

            Board board = new Board();
            board.InitializeBoard(49);

            Console.CursorVisible = false;

            int lastTick = 0;
            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;

                //경과 시간이 1/30초보다 작다면?
                if (currentTick - lastTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion

                // 1)사용자 입력 대기

                // 2)입력과 기타 로직 처리

                // 3)렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
