using System;
using System.Collections.Generic;
using System.Text;

namespace RQ_0x0_TicTacToe
{
    public interface IConsoleBoard
    {
        public void MoveToPosition(int x, int y);

        public void UpdatePosition();

        public void WriteBoard();
    }
}
