using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.FileManagment
{
    public struct Record
    {
        public int score;
        public int rows;
        public int cols;
        public DateTime dt;
        public Record() { score = -1; rows = 0; cols = 0; dt = new DateTime(); }
    }
}
