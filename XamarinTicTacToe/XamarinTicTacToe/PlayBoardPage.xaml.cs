using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTicTacToe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayBoardPage : ContentPage
    {
        private char currentPlayer = 'X';
        private char[,] board = new char[3, 3];

        public PlayBoardPage()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == ' ' && !IsGameFinished())
            {
                board[row, col] = currentPlayer;
                button.Text = currentPlayer.ToString();
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';

                if (IsGameFinished())
                {
                    DisplayAlert("Game Over", "Player " + currentPlayer + " wins!", "OK");
                    InitializeBoard();
                }
            }
        }
        
        private bool IsGameFinished()
        {
            // Check rows for a win
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == currentPlayer && board[row, 1] == currentPlayer && board[row, 2] == currentPlayer)
                    return true;
            }

            // Check columns for a win
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == currentPlayer && board[1, col] == currentPlayer && board[2, col] == currentPlayer)
                    return true;
            }

            // Check diagonals for a win
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;

            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            // Check for a draw
            bool isDraw = true;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        isDraw = false;
                        break;
                    }
                }
                if (!isDraw)
                    break;
            }

            if (isDraw)
            {
                DisplayAlert("Game Over", "It's a draw!", "OK");
                return true;
            }

            return false;
        }

    }
}