using SnakeGame.GamePages;
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
using SnakeGame.Pages;
using System.Configuration;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public MainWindow()
        {
            Rows = 15;
            Cols = 15;
            InitializeComponent();
            Content = new MainPage();
        }
    }
}
