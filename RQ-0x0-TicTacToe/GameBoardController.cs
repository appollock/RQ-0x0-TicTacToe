using System;
using System.Collections.Generic;
using System.Text;

namespace RQ_0x0_TicTacToe
{
    public class GameBoardController : IConsoleBoard
    {
        public char[] innerBoard;

        public char[] board;

        private ConsoleKeyInfo pressedKey;

        private bool xTurn;
        public int CurrentX { get; protected set; }
        public int CurrentY { get; protected set; }

        public GameBoardController()
        {
            SetFirstPlayer();
            InitializeComponents();
            WriteBoard();
            MoveToPosition(CurrentX, CurrentY);
            GameLoop();
        }

        private void GameLoop()
        {
            while(true)
            {
                pressedKey = Console.ReadKey(true);
                if(pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (CurrentX < 6)
                    {
                        CurrentX = 10;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                    else
                    {
                        CurrentX -= 4;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                }
                else if(pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (CurrentX > 6)
                    {
                        CurrentX = 2;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                    else
                    {
                        CurrentX += 4;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                }
                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    if (CurrentY < 3)
                    {
                        CurrentY = 5;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                    else
                    {
                        CurrentY -= 2;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    if (CurrentY > 3)
                    {
                        CurrentY = 1;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                    else
                    {
                        CurrentY += 2;
                        MoveToPosition(CurrentX, CurrentY);
                    }
                }
                else if(pressedKey.Key == ConsoleKey.Enter)
                {
                    if(HasEmptyCellAtCurrentPosition())
                    {
                        if (xTurn)
                        {
                            board[TranslateCoordinates(CurrentX, CurrentY)] = 'X';
                            innerBoard[TranslateCoordinates(CurrentX, CurrentY, true)] = 'X';
                            WriteBoard();
                            MoveToPosition(CurrentX, CurrentY);
                            xTurn = false;
                        }
                        else
                        {
                            board[TranslateCoordinates(CurrentX, CurrentY)] = 'O';
                            innerBoard[TranslateCoordinates(CurrentX, CurrentY, true)] = 'O';
                            WriteBoard();
                            MoveToPosition(CurrentX, CurrentY);
                            xTurn = true;
                        }
                    }
                }
                char player = IsGameOver();
                if(player != ' ')
                {
                    if(player == 'D')
                    {
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("It's a draw!");
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine($"{player} wins!");
                        break;
                    }
                }
            }
        }

        private void InitializeComponents()
        {
            board = "┏━━━┳━━━┳━━━┓\n┃   ┃   ┃   ┃\n┣━━━╋━━━╋━━━┫\n┃   ┃   ┃   ┃\n┣━━━╋━━━╋━━━┫\n┃   ┃   ┃   ┃\n┗━━━┻━━━┻━━━┛".ToCharArray();
            innerBoard = new char[9];
            CurrentX = 6;
            CurrentY = 3;
        }

        private int TranslateCoordinates(int x, int y, bool toInnerBoard = false)
        {
            if(toInnerBoard)
            {
                x = (int)Math.Floor(x / 4.0);
                y = (int)Math.Floor(y / 2.0);
                return y * 3 + x;
            }
            if (y == 1)
            {
                return x + 14;
            }
            else if (y == 3)
            {
                return x + 42;
            }
            else
            {
                return x + 70;
            }
        }

        private bool HasEmptyCellAtCurrentPosition()
        {
            return (board[TranslateCoordinates(CurrentX, CurrentY)] != 'X' && board[TranslateCoordinates(CurrentX, CurrentY)] != 'O');
        }

        private char IsGameOver()
        {
            
            char[] players = { 'X', 'O' };
            
            foreach (char player in players)
            {
                // diag
                if ((innerBoard[2] == player && innerBoard[4] == player && innerBoard[6] == player) ||
                    (innerBoard[0] == player && innerBoard[4] == player && innerBoard[8] == player)) return player;
                for (int i = 0; i < innerBoard.Length; i++)
                {
                    // horizontally
                    if (innerBoard[i] == player && i < innerBoard.Length - 2 && i % 3 == 0)
                    {
                        if (innerBoard[i + 1] == player && innerBoard[i + 2] == player) return player;
                    }
                    // vertically
                    if(innerBoard[i] == player && i < 3)
                    {
                        if (innerBoard[i + 3] == player && innerBoard[i + 6] == player) return player;
                    }
                }
            }
            if (HasGameFinishedByDraw()) return 'D';
            return ' ';
        }

        private bool HasGameFinishedByDraw()
        {
            bool tmp = true;
            foreach (char c in innerBoard)
            {
                if(c == default(char))
                {
                    tmp = false;
                }
            }
            return tmp;
        }

        private void SetFirstPlayer()
        {
            string command = "Hello there stranger, who shall start first(X/O)? ";
            byte @break = 0;
            while(@break == 0)
            {
                Console.Write(command);
                string input = Console.ReadLine();
                if (input.ToLower() == "x" || input.ToLower() == "o")
                {
                    xTurn = input.ToLower() == "x" ? true : false;
                    @break = 1;
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', input.Length+command.Length));
                Console.SetCursorPosition(0, 0);
            }
        }

        public void MoveToPosition(int x, int y)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(x, Console.CursorTop + y);
            Console.CursorVisible = true;
        }

        public void UpdatePosition()
        {
            throw new NotImplementedException();
        }

        public void WriteBoard()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(board);
            Console.CursorVisible = true;
        }
    }
}
