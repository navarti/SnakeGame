﻿using System;
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
    public partial class SelectGameView : UserControl
    {
        public static readonly int MIN_DIMENSION = 5;
        public static readonly int MAX_DIMENSION = 50;
        public SelectGameView()
        {
            InitializeComponent();
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxes())
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

                MainWindow window = (MainWindow)Window.GetWindow(this);
                window.Content = new GamePage(rows, cols);
                window.Rows = rows;
                window.Cols = cols;
            }
        }

        bool CheckBoxes()
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

        private void CheckPaste(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            InputRows.Text = window.Rows.ToString();
            InputCols.Text = window.Cols.ToString();
        }
    }
}
