using SnakeGame.GamePages;
using SnakeGame.Themes.QuitProgramButton;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SnakeGame.GamePages
{
    public partial class GamePage : Page
    {
        private void ModeButton(object sender, RoutedEventArgs e)
        {
            if (mode == Mode.User)
                mode = Mode.AI;
            else
                mode = Mode.User;
        }
        private async void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private async Task ShowCountDown()
        {
            for(int i = 3; i > 0; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(0);
            FileManagment.FileManager fm = new FileManagment.FileManager();

            OverlayText.Text = fm.CheckAndWriteScore(field.Rows, field.Cols, snake.Score) ?
                "Congratulations! New record!\nPress any key to break it again" : 
                "Game Over\n Press any key to restart";
            Overlay.Visibility = Visibility.Visible;
        }


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new Pages.MainPage();
            window = null;
            //NavigationService.Navigate(new MainPage());
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (mode == Mode.User)
            {
                switch (e.Key)
                {
                    case Key.A:
                        controler.ChangeDirection(Direction.Left);
                        break;
                    case Key.D:
                        controler.ChangeDirection(Direction.Right);
                        break;
                    case Key.W:
                        controler.ChangeDirection(Direction.Up);
                        break;
                    case Key.S:
                        controler.ChangeDirection(Direction.Down);
                        break;
                    default:
                        break;
                }
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Page_KeyDown;
            window.PreviewKeyDown += Page_PreviewKeyDown; 
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            Shutdown.ShutdownProg();
        }


    }
}
