using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using SnakeGame.GamePages;

namespace SnakeGame.Themes.QuitProgramButton
{
    internal class Shutdown
    {
        public static void ShutdownProg()
        {
            App.Current.Windows[0].Close();
        }
    }
}
