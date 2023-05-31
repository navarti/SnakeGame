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

        static readonly Dictionary<int, Tuple<string, int>> userSpeedLevels = new Dictionary<int, Tuple<string, int>>
        {
            {0, new Tuple<string, int>("Easy", 150)},
            {1, new Tuple<string, int>("Medium", 100)},
            {2, new Tuple<string, int>("Hard", 50)}
        };

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
            snake = new Snake(field);
            controler = new Controler(snake);
            gridImages = SetupGrid();
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            await GameLoop();
            await ShowGameOver();
            field = new Field(field.Rows, field.Cols);
            snake = new Snake(field);
            controler = new Controler(snake);
        }

        private async Task GameLoop()
        {
            while (!snake.GameOver)
            {
                if(gameRunning == false)
                {
                    break;
                }
                await Task.Delay(GetDelay());
                if (mode == Mode.AI)

                    controler.Execute();
                snake.Move();
                Draw();
            }
        }

        int GetDelay()
        {
            if (mode == Mode.User)
            {
                return userSpeedLevels[(int)SpeedSlider.Value].Item2;
            }
            else
            {
                return -((int)SpeedSlider.Value - (MAX_AI_SPEED + MIN_AI_SPEED));
            }
        }
    }

}



