//TODO: 
//printing
//game logic
//fitnessfunction
//random generation
//find open spots for ^ (using tuples?)
//DoAction
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPNeuralNetworkSharp
{
    public enum GameState
    {
        FailedMove,
        Terminated,
        Continue
    }
    public class Game2048
    {
        public int[,] Board { get; set; }
        public double Score { get; set; }
        private Random RNG { get; set; }
        public Game2048()
        {
            this.Board = new int[4, 4];
            this.Score = 0;
            this.RNG = new Random();
            InsertPiece();
        }
	Console.Write("something to commit");
	Console.Write("something to commit");
	Console.Write("something to commit");
        public GameState Play(int direction)
        {
            var newBoard = this.MoveBoard(direction, true);
            if (IsEqual(newBoard))
            {
                return GameState.FailedMove;
            }
            else
            {
                this.Board = newBoard;
                InsertPiece();
                bool isLegalState = false;
                for (int i = 0; i < 4; i++)
                {
                    var checkBoard = this.MoveBoard(i, false);
                    if (!IsEqual(checkBoard))
                    {
                        isLegalState = true;
                        break;
                    }
                }
                if (isLegalState)
                {
                    return GameState.Continue;
                }
                return GameState.Terminated;
            }
        }
        public List<(int, int)> FindLegalSpots()
        {
            var result = new List<(int, int)>();

            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    if (this.Board[i, j] == 0)
                    {
                        result.Add((i, j));
                    }
                }
            }
            return result;
        }
        public void InsertPiece()
        {
            var spots = this.FindLegalSpots();

            if (spots.Any())
            {
                var spot = spots[this.RNG.Next(spots.Count)];
                var value = this.RNG.Next(1, 3);
                this.Board[spot.Item1, spot.Item2] = value;
            }
        }
        public int[,] MoveBoard(int direction, bool IsPlayer)
        {
            // 0 = north
            // 1 = east
            // 2 = west
            // 3 = south


            var newBoard = new int[this.Board.GetLength(0), this.Board.GetLength(1)];
            switch (direction)
            {
                case 0:
                    for (int i = 0; i < this.Board.GetLength(0); i++)
                    {
                        for (int j = 0; j < this.Board.GetLength(1); j++)
                        {
                            for (int k = 0; k < this.Board.GetLength(0); k++)
                            {
                                if (newBoard[j, i] == 0)
                                {
                                    newBoard[j, i] = this.Board[k, i];
                                    newBoard[k, i] = 0;
                                }
                                else if (newBoard[j, i] == this.Board[k, i])
                                {
                                    newBoard[j, i] += 1;
                                    if (IsPlayer)
                                        this.Score += Math.Pow(2, newBoard[j, i]);
                                    break;
                                }
                                else if (this.Board[k, i] != 0)
                                {
                                    break;
                                }

                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < this.Board.GetLength(0); i++)
                    {
                        for (int j = 0; j < this.Board.GetLength(1); j++)
                        {
                            for (int k = 0; k < this.Board.GetLength(0); k++)
                            {
                                if (newBoard[i, j] == 0)
                                {
                                    newBoard[i, j] = this.Board[k, i];
                                    newBoard[i, k] = 0;
                                }
                                else if (newBoard[i, j] == this.Board[i, k])
                                {
                                    newBoard[i, j] += 1;
                                    if (IsPlayer)
                                        this.Score += Math.Pow(2, newBoard[j, i]);
                                    break;
                                }
                                else if (this.Board[i, k] != 0)
                                {
                                    break;
                                }

                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < this.Board.GetLength(0); i++)
                    {
                        for (int j = this.Board.GetLength(1) - 1; j >= 0; j--)
                        {
                            for (int k = this.Board.GetLength(1) - 1; k >= 0; k--)
                            {
                                if (newBoard[i, j] == 0)
                                {
                                    newBoard[i, j] = this.Board[k, i];
                                    newBoard[i, k] = 0;
                                }
                                else if (newBoard[i, j] == this.Board[i, k])
                                {
                                    newBoard[i, j] += 1;
                                    if (IsPlayer)
                                        this.Score += Math.Pow(2, newBoard[j, i]);
                                    break;
                                }
                                else if (this.Board[i, k] != 0)
                                {
                                    break;
                                }

                            }
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < this.Board.GetLength(0); i++)
                    {
                        for (int j = this.Board.GetLength(1) - 1; j >= 0; j--)
                        {
                            for (int k = this.Board.GetLength(1) - 1; k >= 0; k--)
                            {
                                if (newBoard[j, i] == 0)
                                {
                                    newBoard[j, i] = this.Board[k, i];
                                    newBoard[k, i] = 0;
                                }
                                else if (newBoard[j, i] == this.Board[k, i])
                                {
                                    newBoard[j, i] += 1;
                                    if (IsPlayer)
                                        this.Score += Math.Pow(2, newBoard[j, i]);
                                    break;
                                }
                                else if (this.Board[k, i] != 0)
                                {
                                    break;
                                }

                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("This is not a legal move");
                    break;
            }
            return newBoard;
        }
        public void PrintBoard()
        {
            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    int printValue = 0;
                    if (this.Board[i, j] != 0)
                        printValue = (int)Math.Pow(2, this.Board[i, j]);
                    Console.Write($"{printValue}\t\t");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("---------------------------------------------------");
        }
        private bool IsEqual(int[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] != this.Board[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
