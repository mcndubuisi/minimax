// todo: a whole lot lmao

using System;

class MainClass 
{
  public static char[,] board = new char[3,3]{{'-', '-', '-'}, {'-', '-', '-'}, {'-', '-', '-'}};
  public static char computer = 'X';

  public static void play()
  {
    Random rand = new Random();
    int random_x = rand.Next(0, 3);
    int random_y = rand.Next(0, 3);

    if(isEmpty(random_x, random_y))
    {
      random_x = rand.Next(0, 3);
      random_y = rand.Next(0, 3);
      board[random_x,random_y] = computer;
      printBoard();
    }
    else
    {
      play();
    }
  }

  public static bool isEmpty(int x, int y)
  {
    if(board[x,y] == '-')
    {
      return true;
    }

    return false;
  }

  public static bool checkForWin()
  {
    return checkRow() || checkColumn() || checkDiagonal();
  }

  public static bool checkRow()
  {
    for(int i=0; i<3; i++)
    {
      if(board[i,0] != '-' && board[i,0] == board[i,1] && board[i,1] == board[i,2])
      {
        return true;
      }
    }
    return false;
  }

  public static bool checkColumn()
  {
    for(int i=0; i<3; i++)
    {
      if(board[0,i] != '-' && board[0,i] == board[1,i] && board[1,i] == board[2,i])
      {
        return true;
      }
    }
    return false;
  }

  public static bool checkDiagonal()
  {
    return ((board[0,0] != '-' && board[0,0] == board[1,1] && board[1,1] == board[2,2]) 
            || (board[0,2] != '-' && board[0,2] == board[1,1] && board[1,1] == board[2,0]));
  }

  public static void printBoard()
  {
    Console.WriteLine(" {0} | {1} | {2} \n---|---|--- \n {3} | {4} | {5} \n---|---|--- \n {6} | {7} | {8} \n", 
      board[0,0], board[0,1], board[0,2], board[1,0], board[1,1], board[1,2], board[2,0], board[2,1], board[2,2]);
  }

  public static bool boardEmpty()
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
    printBoard();
    while(checkForWin() != true && boardEmpty() != true)
    {
      Console.Write("Enter x: ");
      int x = int.Parse(Console.ReadLine());
      Console.Write("Enter y: ");
      int y = int.Parse(Console.ReadLine());

      if(isEmpty(x, y))
      {
        board[x,y] = 'O';
        play();
      }
      else
      {
        Console.WriteLine("That square is not empty");
      }
    }
  }
}
