using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SnakeGame.GamePages;

namespace SnakeGame.Pages.MainPageCategories.View
{
    /// <summary>
    /// Interaction logic for SelectGameView.xaml
    /// </summary>
    public partial class SelectGameView : UserControl
    {
        readonly int MIN_DIMENSION = 5;
        readonly int MAX_DIMENSION = 50;
        public SelectGameView()
        {
            InitializeComponent();
            InputRows.Text = "15";
            InputCols.Text = "15";
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            if (ParseBoxes())
            {
                ErrorTextBlock.Text = "";
                int rows = int.Parse(InputRows.Text);
                int cols = int.Parse(InputCols.Text);
                if(rows > cols)
                {
                    int temp = rows;
                    rows = cols;
                    cols = temp;
                }
                Window window = Window.GetWindow(this);
                window.Content = new GamePage(rows, cols);
                //NavigationService.GetNavigationService(this).Navigate(new GamePage(rows, cols));
            }
        }

        bool ParseBoxes()
        {
            int rows, cols;
            if (!int.TryParse(InputRows.Text, out rows) || !int.TryParse(InputCols.Text, out cols))
            {
                ErrorTextBlock.Text = "Enter the numbers!";
                return false;
            }
            if(rows < MIN_DIMENSION || cols < MIN_DIMENSION)
            {
                ErrorTextBlock.Text = $"The minimum dimension: {MIN_DIMENSION}";
                return false;
            }
            if (rows > MAX_DIMENSION || cols > MAX_DIMENSION)
            {
                ErrorTextBlock.Text = $"The maximum dimension: {MAX_DIMENSION}";
                return false;
            }
            return true;
        }

        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); 
        }
    }
}
