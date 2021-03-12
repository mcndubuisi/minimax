/****************************************************************************** 
 * Project 1 - Tic Tac Toe
 * 
 * Develop a Tic Tac Toe intelligent agent that players as 'O'.
 * Author: Emmanuel Ndubuisi
 * Date: March 11, 2021
 * 
 * Compilation and Execution:  https://repl.it/@mcndubuisi/TicTacToe
 ******************************************************************************/

using System;
using System.Collections.Generic;

class TicTacToe 
{
  public static char[,] board = new char[3,3]{{'-', '-', '-'}, {'-', '-', '-'}, {'-', '-', '-'}};
  public static char computer = 'O';
  public static char human = 'X';

  public static void play()
  {
    int[] coordinates = bestMove();
    board[coordinates[0], coordinates[1]] = computer;
    printBoard();
    if(hasWon()) System.Environment.Exit(1);
  }

  // find the best move for agent
  public static int[] bestMove()
  {
    int bestScore = int.MinValue;
    int[] move = null;

    for (int i = 0; i < 3; i++) 
    {
      for (int j = 0; j < 3; j++) 
      {
        if (board[i,j] == '-')
        {
          board[i,j] = computer;
          int score = minimax(board, 0, false);
          board[i,j] = '-';
          if (score > bestScore)
          {
            bestScore = score;
            move = new int[] { i, j };
          }
        }
      }
    }

    return move;
  }

  // minimax algorithm that return best score per move
  public static int minimax(char[,] board, int depth, bool isMaximizing)
  {
    string result = checkWinner();
    var scores = new Dictionary<string, int>(){
      {"X", -10}, {"O", 10}, {"tie", 0}
    };
	  
    if (result != null)
    {
      return scores[result];
    }

    if (isMaximizing)
    {
      int bestScore = int.MinValue;
      for (int i = 0; i < 3; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          if (board[i,j] == '-')
          {
            board[i,j] = computer;
            int score = minimax(board, depth + 1, false);
            board[i,j] = '-';
            bestScore = Math.Max(score, bestScore);
          }
        }
      }
      return bestScore;
    }
    else 
    {
      int bestScore = int.MaxValue;
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
          // Is the spot available?
          if (board[i,j] == '-') {
            board[i,j] = human;
            int score = minimax(board, depth + 1, true);
            board[i,j] = '-';
            bestScore = Math.Min(score, bestScore);
          }
        }
      }
      return bestScore;
    }
  }

  // check if board is empty
  public static bool isEmpty(int x, int y)
  {
    if(board[x,y] == '-')
    {
      return true;
    }

    return false;
  }

  // check if there is a winner or a full board.
  public static bool hasWon()
  {
    if (checkWinner() == "tie")
    {
      Console.Write("It was a draw.\n");
    }
    else if (checkWinner() != null)
    {
      Console.Write($"Player {checkWinner()} wins!\n");
    }
    
    return checkRow() != null || checkColumn() != null || checkDiagonal() != null || isBoardFull();
  }

  // return the name of the winner
  public static string checkWinner()
  {
    if (isBoardFull()) return "tie";

    return checkRow() ?? checkColumn() ?? checkDiagonal();
  }

  // check rows for winner
  public static string checkRow()
  {
    for(int i=0; i<3; i++)
    {
      if(board[i,0] != '-' && board[i,0] == board[i,1] && board[i,1] == board[i,2])
      {
        return Char.ToString(board[i,0]);
      }
    }
    return null;
  }

  // check column for winner
  public static string checkColumn()
  {
    for(int i=0; i<3; i++)
    {
      if(board[0,i] != '-' && board[0,i] == board[1,i] && board[1,i] == board[2,i])
      {
        return Char.ToString(board[0,i]);
      }
    }
    return null;
  }

  // check diagonal for winner
  public static string checkDiagonal()
  {
    if (board[0,0] != '-' && board[0,0] == board[1,1] && board[1,1] == board[2,2])
    {
        return Char.ToString(board[0,0]);
    } 
    else if (board[0,2] != '-' && board[0,2] == board[1,1] && board[1,1] == board[2,0])
    {
        return Char.ToString(board[0,2]);
    }

    return null;
  }

  // print tic tac toe board
  public static void printBoard()
  {
    Console.WriteLine("\n {0} | {1} | {2} \n---|---|--- \n {3} | {4} | {5} \n---|---|--- \n {6} | {7} | {8} \n", 
      board[0,0], board[0,1], board[0,2], board[1,0], board[1,1], board[1,2], board[2,0], board[2,1], board[2,2]);
  }

  // check if board is full
  public static bool isBoardFull()
  {
    for(int j=0; j<3; j++)
    {
      for(int k=0; k<3; k++)
      {
        if(board[j, k] == '-')
        {
          return false;
        }
      }
    }

    return true;
  }


  public static void Main(string[] args)
  {
    Console.Write("Welcome to Tic Tac Toe.\n");
    Console.Write("Enter the (x, y) coordinate of where you want to make your move on the board.\n");

    printBoard();
    // request human to play if board isn't full
    while(isBoardFull() == false)
    {
      Console.Write("Player X's turn\n");
      Console.Write("Enter x-coordinate: ");
      int x = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter y-coordinate: ");
      int y = Convert.ToInt32(Console.ReadLine());

      // check if tile is empty
      try 
      {
        if(isEmpty(x, y))
        {
          board[x, y] = 'X';
          // stop program is there is a winner
          if (hasWon()) System.Environment.Exit(1);
          play();
        }
        else
        {
          Console.WriteLine("\nThat square is not empty. Try Again!\n");
        }
      }
      catch(Exception)
      {
        Console.WriteLine("\nInvalid move. Please try again!\n");
      }
    }
  }
}
