using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakeGame.GamePages
{
    public static class Images
    {
        private static string folder = Settings.Default.SnakeTheme.ToString();

        public readonly static ImageSource Empty = LoadImage("Empty.png");
        public readonly static ImageSource Body = LoadImage("Body.png");
        public readonly static ImageSource Head = LoadImage("Head.png");
        public readonly static ImageSource Food = LoadImage("Food.png");
        public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");
        public readonly static ImageSource Deadhead = LoadImage("DeadHead.png");

        public static ImageSource LoadImage(string filename)
        {
            return new BitmapImage(new Uri($"SnakeImages/{folder}/{filename}", UriKind.Relative));
        }
    }
}
