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
using SnakeGame.FileManagment;


namespace SnakeGame.Pages.MainPageCategories.View
{
    /// <summary>
    /// Interaction logic for RecordsView.xaml
    /// </summary>
    public partial class RecordsView : UserControl
    {
        static readonly int GeneralNum = 5;

        public static readonly Dictionary<char, string> levelDict = new Dictionary<char, string>
        {
            {'E', "Easy" },
            {'M', "Medium" },
            {'H', "Hard"},
            {'A', "AI" }
        };
        FileManager fm = new FileManager();
        List<Record> all_records;
        int counter = 0;
        
        public RecordsView()
        {
            InitializeComponent();
            FillRecords();
        }

        void FillRecords()
        {
            try
            {
                all_records = fm.GetAllRecords();
            }
            catch (Exception)
            {
                FillRecords();
            }

            TextBlock[] scoreBlocks = { ScoreTB1, ScoreTB2, ScoreTB3, ScoreTB4, ScoreTB5 };
            TextBlock[] levelsBlocks = { LevelTB1, LevelTB2, LevelTB3, LevelTB4, LevelTB5 };
            TextBlock[] dimensionBlocks = { DimensionTB1, DimensionTB2, DimensionTB3, DimensionTB4, DimensionTB5 };
            TextBlock[] dateBlocks = { DateTB1, DateTB2, DateTB3, DateTB4, DateTB5 };

            for (int i = 0; i < GeneralNum; i++) 
            {
                scoreBlocks[i].Text = "";
                dimensionBlocks[i].Text = "";
                levelsBlocks[i].Text = "";
                dateBlocks[i].Text = "";
            }

            int block_counter = 0;
            for(int i=counter; i<all_records.Count && block_counter < GeneralNum;i++)
            {
                if (CheckDimension(i))
                {
                    scoreBlocks[block_counter].Text = all_records[i].score.ToString();
                    levelsBlocks[block_counter].Text = levelDict[all_records[i].level];
                    dimensionBlocks[block_counter].Text = all_records[i].rows.ToString() + 'x' + all_records[i].cols.ToString();
                    dateBlocks[block_counter].Text = all_records[i].dt.ToString();
                    block_counter++;
                }
            }
        }

        bool CheckDimension(int i)
        {
            int current_rows=0, current_cols=0;

            int.TryParse(InputRows.Text.ToString(), out current_rows);
            int.TryParse(InputCols.Text.ToString(), out current_cols);

            if (current_cols == 0 && current_rows == 0) return true;
            if(current_rows == 0 && all_records[i].cols == current_cols) return true;
            if(current_cols == 0 && all_records[i].rows == current_rows) return true;
            if (current_rows == all_records[i].rows && all_records[i].cols == current_cols) return true;
            return false;
        }

        private void DimensionPrevious(object sender, RoutedEventArgs e)
        {
            if(counter + GeneralNum < all_records.Count - 1)
            {
                counter += GeneralNum;
                FillRecords();
            }
        }
        private void DimensionNext(object sender, RoutedEventArgs e)
        {
            if (counter - GeneralNum >= 0)
            {
                counter -= GeneralNum;
                FillRecords();
            }
        }

        private void CheckInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textChangedEventHandler(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length - 1 > View.SelectGameView.MAX_DIMENSION.ToString().Length)
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToString().Substring(0, ((TextBox)sender).Text.Length-1);
            }
            FillRecords();
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
    }
}
