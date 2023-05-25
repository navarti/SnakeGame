using System;
using System.Collections.Generic;
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

namespace SnakeGame.GamePages
{
    public partial class GamePage : Page
    {
        enum Mode { User, AI }
        Mode mode = Mode.User;

        enum Level { Easy=150, Medium=100, Hard=50}
        Level level = Level.Medium;

        const int MAX_AI_SPEED = 10;
        const int MIN_AI_SPEED = 150;

        private Image[,] gridImages;
        Field field;
        Snake snake;
        Controler controler;
        private bool gameRunning;

        public GamePage(int rows = 15, int cols = 15)
        {
            InitializeComponent();
            field = new Field(rows, cols);
        }

        public void ChangeDimension(int rows, int cols)
        {
            field = new Field(rows, cols); 
            gridImages = SetupGrid();
            mode = Mode.User;
        }

        private async Task RunGame()
        {
            field = new Field(field.Rows, field.Cols);
            snake = new Snake(field);
            controler = new Controler(snake); 
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();
        }

        private async Task GameLoop()
        {
            while (!snake.GameOver)
            {
                await Task.Delay(GetDelay());
                if (mode == Mode.AI)

                    controler.Execute();
                snake.Move();
                Draw();
            }
        }

        int GetDelay()
        {
            if(mode == Mode.User)
            {
                return (int)level;
            }
            else
            {
              
                return (int)AISlider.Value;
            }


        }
    }
}