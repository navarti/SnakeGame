using SnakeGame.GamePages;
using SnakeGame.Properties;
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

namespace SnakeGame.Pages.MainPageCategories.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        readonly string[] snakeColor = { "Green", "Red" };
        
        public SettingsView()
        {
            InitializeComponent();
            SnakeColorTextBox.Text = Properties.Settings.Default.SnakeTheme;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ColorMode == "Dark")
            {
                Properties.Settings.Default.ColorMode = "Light";
            }
            else
            {
                Properties.Settings.Default.ColorMode = "Dark";
            }

            Properties.Settings.Default.Save();
        }

        private void ChangeSound(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Sound = !Properties.Settings.Default.Sound;
            Settings.Default.Save();
        }

        private void SnakeColorPrevious(object sender, RoutedEventArgs e)
        {
            for(int i = 0;i < snakeColor.Length-1; i++)
            {
                if (snakeColor[i] == Properties.Settings.Default.SnakeTheme)
                {
                    Properties.Settings.Default.SnakeTheme = snakeColor[i+1];
                    SnakeColorTextBox.Text = Properties.Settings.Default.SnakeTheme;
                    Settings.Default.Save(); 
                    return;
                }
            }
        }
        private void SnakeColorNext(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < snakeColor.Length; i++)
            {
                if (snakeColor[i] == Properties.Settings.Default.SnakeTheme)
                {
                    Properties.Settings.Default.SnakeTheme = snakeColor[i-1];
                    Properties.Settings.Default.Save();
                    SnakeColorTextBox.Text = Properties.Settings.Default.SnakeTheme;
                    return;
                }
            }
        }
    }
}
