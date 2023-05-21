using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GamePages
{
    public class Field
    {
        public enum GridValue
        {
            Empty,
            Snake,
            Food,
            Outside
        }
        Random rnd = new Random();

        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; set; }

        public Position FoodPos { get; private set; }

        public Field(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[Rows, Cols];
        }

        public IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        public void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
            {
                return;
            }
            Position pos = empty[rnd.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
            FoodPos = pos;
        }

        public bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

    }

}
