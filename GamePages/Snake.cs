using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame.GamePages
{
    public partial class Snake
    {
        public Field SField { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }


        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();
        private readonly Random rnd = new Random();

        public Snake(Field _field)
        {
            SField = _field;
            SField.AddFood();
            Addsnake();
            Direction = Direction.Right;
        }

        private void Addsnake()
        {
            int r = SField.Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                SField.Grid[r, c] = Field.GridValue.Snake;

                snakePositions.AddFirst(new Position(r, c));
            }
        }

        public Position HeadPosition()
        {
            return snakePositions.First.Value;
        }

        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            SField.Grid[pos.Row, pos.Col] = Field.GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            SField.Grid[tail.Row, tail.Col] = Field.GridValue.Empty;
            snakePositions.RemoveLast();
        }

        public Field.GridValue WillHit(Position newHeadPosition)
        {
            if (SField.OutsideGrid(newHeadPosition))
            {
                return Field.GridValue.Outside;
            }
            if (newHeadPosition == TailPosition())
            {
                return Field.GridValue.Empty;
            }
            return SField.Grid[newHeadPosition.Row, newHeadPosition.Col];
        }

        private Direction GetLastDirection()
        {
            if (dirChanges.Count == 0)
            {
                return Direction;
            }
            return dirChanges.Last.Value;
        }

        private bool CanChangeDirection(Direction newDir)
        {
            if (dirChanges.Count == 2)
            {
                return false;
            }
            return newDir != GetLastDirection() && newDir != Direction.Opposite();
        }

        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
        }



        public void Move()
        {
            if (dirChanges.Count > 0)
            {
                Direction = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Position newHeadPosition = HeadPosition().Translate(Direction);
            Field.GridValue hit = WillHit(newHeadPosition);

            if (hit == Field.GridValue.Snake || hit == Field.GridValue.Outside)
            {
                GameOver = true;
            }
            else if (hit == Field.GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPosition);
            }
            else if (hit == Field.GridValue.Food)
            {
                AddHead(newHeadPosition);
                Score++;
                SField.AddFood();
            }
        }
    }
}
