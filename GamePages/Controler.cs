using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using static SnakeGame.GamePages.Field;
using static System.Formats.Asn1.AsnWriter;

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

            PriorityQueue<Position, int> open = new PriorityQueue<Position, int>();
            List<Position> closed = new List<Position>();
            from.Weight = 0;
            open.Enqueue(from, Heuristic(from, to) + from.Weight);


            Position current = from;
            while (open.Count != 0 && !closed.Exists(x=>x==to))
            {
                current = open.Dequeue();
                closed.Add(current);

                List<int> neighbors = new List<int>(GetIndexesOfNeighbors(current, all));
                foreach (int neighbor in neighbors)
                {
                    if (closed.Contains(all[neighbor])) continue;

                    bool isFound = false;
                    foreach (var oldPos in open.UnorderedItems)
                    {
                        if(oldPos.Element == all[neighbor]) isFound = true;
                    }

                    if (!isFound)
                    {
                        all[neighbor].Weight = current.Weight + 1;
                        all[neighbor].Parent = current;
                        open.Enqueue(all[neighbor], all[neighbor].Weight + Heuristic(all[neighbor], to));
                    }
                }
            }

            if(!closed.Exists(x=> x == to))
            {
                return false;
            }

            Position temp = closed[closed.IndexOf(current)];
            if (temp == null) return false;
            do
            {
                positions_to_go.Push(temp);
                temp = temp.Parent;
            } while (temp != from && temp != null);
            
            return true;
        }

        private int Heuristic(Position start, Position goal)
        {
            var dx = goal.Row - start.Row;
            var dy = goal.Col - start.Col;
            return Math.Abs(dx) + Math.Abs(dy);
        }

        /*
        vector<Node*> graph:: Astar(int x1, int y1, int x2, int y2)
        {
            Node* start = findnode(x1, y1), *end = findnode(x2, y2);
            priorityQueue q;
            vector<Node*> prev(g.size()* g[0].size(), nullptr);
            vector<int> gscore(g.size()* g[0].size(), g.size() * g[0].size());
            gscore[start->getX() * g[0].size() + start->getY()] = 0;
            start->setp(heuristic(start, end));
            q.push(start);
            while (!q.isEmpty())
            {
                Node* current = q.pop();
                if (current == end)
                {
                    break;
                }
                vector<Node*> & adjac = current->get_adj();
                for (int i = 0; i < adjac.size(); i++)
                {
                    Node* neighbor = adjac[i];
                    int ind = neighbor->getX() * g[0].size() + neighbor->getY();
                    int tentativeGScore = gscore[current->getX() * g[0].size() + current->getY()] + 1;
                    if (tentativeGScore < gscore[ind])
                    {
                        prev[ind] = current;
                        gscore[ind] = tentativeGScore;
                        neighbor->setp(tentativeGScore + heuristic(neighbor, end));
                        if (!q.isin(neighbor))
                        {
                            q.push(neighbor);
                        }
                    }
                }
            }
            if (prev[end->getX() * g[0].size() + end->getY()] == nullptr)
            {
                return std::vector<Node*>();
            }
            std::vector<Node*> path;
            Node* current = end;
            while (current != start)
            {
                path.insert(path.begin(), current);
                current = prev[current->getX() * g[0].size() + current->getY()];
            }
            path.insert(path.begin(), start);
            return path;
        }
        */

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










