using System;
using System.Collections.Generic;
using System.Text;

namespace RQ_0x0_TicTacToe
{
    public interface IConsoleBoard
    {
        public char[] Board { get; protected set; }

        public int CurrentPosition { get; protected set; }

        public void MoveToPosition(int index);

        public void UpdatePosition();

        public void WriteBoard();
    }
}
