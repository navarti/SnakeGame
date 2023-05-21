using SnakeGame.GamePages;
using SnakeGame.Themes.QuitProgramButton;
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
using System.Xml.Linq;

namespace SnakeGame.GamePages
{
    public partial class GamePage : Page
    {
        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            {Direction.Up, 0},
            {Direction.Right, 90 },
            {Direction.Down, 180 },
            {Direction.Left, 270 }
        };

        private readonly Dictionary<Field.GridValue, ImageSource> gridValToImage = new()
        {
            {Field.GridValue.Empty, Images.Empty },
            {Field.GridValue.Food, Images.Food },
            {Field.GridValue.Snake, Images.Body }
        };

        Image[,] SetupGrid()
        {
            Image[,] images = new Image[field.Rows, field.Cols];
            GameGrid.Rows = field.Rows;
            GameGrid.Columns = field.Cols;
            GameGrid.Height = GameGrid.Width * (field.Rows / (double)field.Cols);

            for (int r = 0; r < field.Rows; r++)
            {
                for (int c = 0; c < field.Cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5),
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreBlock.Text = $"Score: {snake.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < field.Rows; r++)
            {
                for (int c = 0; c < field.Cols; c++)
                {
                    Field.GridValue gridVal = field.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }

        private void DrawSnakeHead(bool dead=false)
        {
            Position headPos = snake.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Col];
            image.Source = dead ? Images.Deadhead : Images.Head;
            int rotation = dirToRotation[snake.Direction];
            image.RenderTransform = new RotateTransform(rotation);
        }

        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(snake.SnakePositions());

            for(int i = 0; i < positions.Count; i++)
            {
                if (i == 0)
                {
                    DrawSnakeHead(true);
                    continue;
                }
                Position pos = positions[i];
                ImageSource source = Images.DeadBody;
                gridImages[pos.Row, pos.Col].Source = source;
                await Task.Delay(50);
            }

        }
    }
}
