using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Threading;

namespace SnakeGame.FileManagment
{
    internal class FileManager
    {
        readonly string filename_folder = "scores\\";
        readonly string extension = ".txt";

        readonly string levels = "EasyMediumHardAI";

        public FileManager()
        {
            if (!Directory.Exists(filename_folder))
            {
                DirectoryInfo di = Directory.CreateDirectory(filename_folder);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                //MessageBox.Show("New folder with records have been created. Go ahead to be the top!");
                Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                dispatcher.BeginInvoke(new Action(() => MessageBox.Show("New folder with records have been created. Go ahead to be the top!")));
            }
        }


        public List<Record> GetAllRecords()
        {
            string[] filenames = Directory.GetFiles(filename_folder);
            List<Record> result = new List<Record>();
            for (int i = 0; i < filenames.Length; i++)
            {
                try
                {
                    result.Add(ParseToRead(ReadText(filenames[i])));
                }
                catch (Exception)
                {
                    Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                    dispatcher.BeginInvoke(new Action(() => MessageBox.Show($"The file {filenames[i]} was damaged. The data was deleted")));
                }
            }
            return result;
        }

        string ParseFilenameToWrite(int rows, int cols, string level)
        {
            return filename_folder + rows + "_" + cols + "_" + level[0] + extension;
        }

        string ReadText(string filename)
        {
            string text;
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    text = sr.ReadLine();
                }
            }

            catch (Exception ex)
            {
                text = "new_file";
            }
            return text;
        }

        void WriteRecord(string text, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(text);
            }
        }

        Record ParseToRead(string text)
        {
            Record to_return = new Record();

            if(text == "new_file") return to_return;
            
            string[] data = text.Split("split");

            if(data.Length != 5)
            {
                throw new Exception();
            }
            
            if (!int.TryParse(data[0], out to_return.rows))
            {
                throw new Exception();
            }
            
            if (!int.TryParse(data[1], out to_return.cols))
            {
                throw new Exception();
            }
            
            if (!int.TryParse(data[2], out to_return.score))
            {
                throw new Exception();
            }

            to_return.level = data[3];
            if (levels.IndexOf(data[3]) == -1)
            {
                throw new Exception();
            }

            if (!DateTime.TryParse(data[4], out to_return.dt))
            {
                throw new Exception();
            }
            return to_return;
        }

        string ParseToWrite(Record to_write)
        {
            return to_write.rows + "split" + to_write.cols + "split" + to_write.score + "split" + to_write.level + "split" + to_write.dt;
        }


        //для GamePage
        public bool CheckAndWriteScore(int rows, int cols, int new_score, string level)
        {
            string filename = ParseFilenameToWrite(rows, cols, level);
            Record old = new Record();

            bool overWrite = false;
            try
            {
                old = ParseToRead(ReadText(filename));
            }
            catch (Exception ex)
            {
                overWrite = true;
                MessageBox.Show("File with current record was damaged and will be overwritten with this score");
            }

            if ((old.score < new_score && old.level == level) || overWrite || old.level == "")
            {
                Record new_record = new Record();
                new_record.rows = rows;
                new_record.cols = cols;
                new_record.score = new_score;
                new_record.level = level;
                new_record.dt = DateTime.Now;
                string to_write = ParseToWrite(new_record);
                WriteRecord(to_write, filename);
                return true;
            }
            return false;
        }
    }
}
