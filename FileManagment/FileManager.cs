using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.FileManagment
{
    internal class FileManager
    {
        readonly string filename_folder = "scores\\";
        readonly string extension = ".txt";

        public FileManager()
        {
            if (!Directory.Exists(filename_folder))
            {
                DirectoryInfo di = Directory.CreateDirectory(filename_folder);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }


        public Record[] GetAllRecords()
        {
            string[] filenames = Directory.GetFiles(filename_folder);
            Record[] result = new Record[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
            {
                result[i] = ParseToRead(ReadText(filenames[i]));
                Tuple<int, int> dimension = ParseFilename(filenames[i]);
                result[i].rows = dimension.Item1;
                result[i].cols = dimension.Item2;
            }
            return result;
        }

        Tuple<int, int> ParseFilename(string filename)
        {
            filename = filename.Substring(filename_folder.Length);
            filename = filename.Substring(0, filename.IndexOf('.'));
            string[] data = filename.Split('_');

            int rows=0, cols=0;
            
            if (!int.TryParse(data[0], out rows) || !int.TryParse(data[1], out cols))
            {
                //
            }

            return Tuple.Create(rows, cols);
        }

        string GetFileName(int rows, int cols)
        {
            return filename_folder + rows + "_" + cols + extension;
        }

        string ReadText(string filename)
        {
            //GetAllRecords();
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    text = sr.ReadLine();
                }
            }

            catch (Exception ex)
            {
                //throw new Exception();
            }
            return text;
        }

        void WriteText(string text, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(text);
            }
        }

        Record ParseToRead(string text)
        {
            Record to_return = new Record();
            if(text == "") return to_return;
            int index_to_split = text.IndexOf(' ');
            //string[] data = text.Split(' ', 2);
            string[] data = { text.Substring(0, index_to_split), text.Substring(index_to_split+1)};
            
            if (!int.TryParse(data[0], out to_return.score))
            {
                //
            }
             
            if(DateTime.TryParse(data[1], out to_return.dt))
            {
                //
            }
            return to_return;
        }

        string ParseToWrite(Record to_write)
        {
            return to_write.score + " " + to_write.dt;
        }


        ////для GamePage
        //public Record GetScore(int rows, int cols)
        //{
        //    string filename = GetFileName(rows, cols);
        //    Record to_return = ParseToRead(ReadText(filename));
        //    to_return.rows = rows;
        //    to_return.cols = cols;
        //    return to_return;
        //}

        //для GamePage
        public bool CheckAndWriteScore(int rows, int cols, int new_score)
        {
            GetAllRecords();
            string filename = GetFileName(rows, cols);
            int old_score = ParseToRead(ReadText(filename)).score;

            if(old_score < new_score)
            {
                Record new_record = new Record();
                new_record.score = new_score;
                new_record.dt = DateTime.Now;
                string to_write = ParseToWrite(new_record);
                WriteText(to_write, filename);
                return true;
            }

            return false;
        }
    }
}
