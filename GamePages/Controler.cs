using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using static SnakeGame.GamePages.Field;

namespace SnakeGame.GamePages
{
    internal class Controler
    {
        Snake snake;
        Stack<Position> positions_to_go;

        public Controler(Snake _snake)
        {
            snake = _snake;
            positions_to_go = new Stack<Position>();
        }

        public void ChangeDirection(Direction dir)
        {
            snake.ChangeDirection(dir);
        }

        public void Execute()
        {
            if (positions_to_go.Count == 0)
            {
                if (!AStar())
                {
                    ChangeDirectionToSurvive();
                    return;
                }
            }
            ChangeDirection(GetDirection());
        }

        private Direction GetDirection()
        {
            Position headPos = snake.HeadPosition();
            if(positions_to_go.Count == 0)
            {
                return Direction.Up;
            }
            Position to_go = positions_to_go.Pop();

            Direction[] dirs = { Direction.Left, Direction.Up, Direction.Right, Direction.Down };

            foreach (Direction dir in dirs)
                if (headPos.Translate(dir) == to_go) return dir;

            return Direction.Right;
        }


        private bool AStar()
        {
            Position from = snake.HeadPosition();
            Position to = snake.SField.FoodPos;


            List<Position> all = new List<Position>(snake.SField.EmptyPositions());
            all.Add(to);

            List<Position> open = new List<Position>();
            List<Position> closed = new List<Position>();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].IsNeighbors(from))
                {
                    all[i].Weight = 1;
                    all[i].Parent = from;
                }
                else
                    all[i].Weight = int.MaxValue / 30000;
            }

            from.Weight = int.MaxValue / 30000;
            open.Add(from);

            while (true)
            {
                int current_index = GetIndexOfLowest(open);
                if (current_index >= open.Count)
                    return false;
                Position current = open[current_index];
                open.RemoveAt(current_index);
                closed.Add(current);

                if (current == to)
                {
                    positions_to_go.Clear();
                    List<Position> result = new List<Position>();
                    while (current != from)
                    {
                        positions_to_go.Push(current);
                        current = current.Parent;
                    }
                    return true;
                }

                List<int> neighbors = new List<int>(GetIndexesOfNeighbors(current, all));
                foreach (int neighbor in neighbors)
                {
                    if (closed.Contains(all[neighbor])) continue;

                    if (current.Weight + 1 < all[neighbor].Weight || !open.Contains(all[neighbor]))
                    {
                        open.Remove(all[neighbor]);
                        all[neighbor].Weight = current.Weight + 1;
                        all[neighbor].Parent = current;
                        open.Add(all[neighbor]);
                    }
                }
            }
        }

        private int GetIndexOfLowest(List<Position> positions)
        {
            int value = int.MaxValue;
            int index = 0;
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].Weight < value)
                {
                    value = positions[i].Weight;
                    index = i;
                }
            }
            return index;
        }

        private List<int> GetIndexesOfNeighbors(Position neighbor, List<Position> positions)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].IsNeighbors(neighbor))
                    result.Add(i);
            }
            return result;
        }

        private bool ChangeDirectionToSurvive()
        {
            bool CheckHit(Position newHeadPos)
            {
                return snake.WillHit(newHeadPos) == GridValue.Food ||
                    snake.WillHit(newHeadPos) == GridValue.Empty;
            }

            Position NextHeadPos(Direction dir)
            {
                return snake.HeadPosition().Translate(dir);
            }

            if (!CheckHit(NextHeadPos(snake.Direction)))
            {
                if (CheckHit(NextHeadPos(Direction.Left)))
                {
                    ChangeDirection(Direction.Left);
                }
                else if (CheckHit(NextHeadPos(Direction.Up)))
                {
                    ChangeDirection(Direction.Up);
                }
                else if (CheckHit(NextHeadPos(Direction.Right)))
                {
                    ChangeDirection(Direction.Right);
                }
                else if (CheckHit(NextHeadPos(Direction.Down)))
                {
                    ChangeDirection(Direction.Down);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}










