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
    public static class ImageLoader
    {

        public static ImageSource Empty
        {
            get { return LoadImage("Empty.png"); }
        }
        public static ImageSource Body
        {
            get { return LoadImage("Body.png"); }
        }
        public static ImageSource Head
        {
            get { return LoadImage("Head.png"); }
        }
        public static ImageSource Food
        {
            get { return LoadImage("Food.png"); }
        }
        public static ImageSource DeadBody
        {
            get { return LoadImage("DeadBody.png"); }
        }
        public static ImageSource Deadhead 
        {
            get { return LoadImage("DeadHead.png"); }
        }
        

        public static ImageSource LoadImage(string filename)
        {
            string folder = Settings.Default.SnakeTheme.ToString();
            return new BitmapImage(new Uri($"SnakeImages/{folder}/{filename}", UriKind.Relative));
        }
    }
}
