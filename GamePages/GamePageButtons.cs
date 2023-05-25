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
            if (gameRunning) return;
            if (mode == Mode.User)
            {
                mode = Mode.AI;
                ChangeSlider(MAX_AI_SPEED, MIN_AI_SPEED, "AI Speed", false);
            }
            else
            {
                ChangeSlider(0, userSpeedLevels.Count-1, "Level", true);
                mode = Mode.User;
            }
        }

        void ChangeSlider(int min, int max, string text, bool user)
        {
            SpeedSlider.Minimum = min;
            SpeedSlider.Maximum = max;
            SpeedTextBlock.Text = text;   
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


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.Content = window.Main;
            window.KeyDown -= Page_KeyDown;
            window.PreviewKeyDown -= Page_PreviewKeyDown;
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
            AICheckBox.IsChecked = false;
            SpeedSlider.TickFrequency = 1;
            SpeedSlider.IsSnapToTickEnabled = true;
            ChangeSlider(0, userSpeedLevels.Count - 1, "Level", true);
            SpeedSlider.Value = userSpeedLevels.Count/2;
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            Shutdown.ShutdownProg();
        }
    }
}
