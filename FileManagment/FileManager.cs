using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            }
        }


        public Record[] GetAllRecords()
        {
            string[] filenames = Directory.GetFiles(filename_folder);
            Record[] result = new Record[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
            {
                result[i] = ParseToRead(ReadText(filenames[i]));
            }
            return result;
        }

        string ParseFilenameToWrite(int rows, int cols, string level)
        {
            return filename_folder + rows + "_" + cols + "_" + level[0] + extension;
        }

        string ReadText(string filename)
        {
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

            if(text == "") return to_return;
            
            string[] data = text.Split("split");

            if(data.Length != 5)
            {
                //
            }
            
            if (!int.TryParse(data[0], out to_return.rows))
            {
                //
            }
            
            if (!int.TryParse(data[1], out to_return.cols))
            {
                //
            }
            
            if (!int.TryParse(data[2], out to_return.score))
            {
                //
            }

            to_return.level = data[3];
            if (levels.IndexOf(data[3]) == -1)
            {
                //
            }

            if (DateTime.TryParse(data[4], out to_return.dt))
            {
                //
            }
            return to_return;
        }

        string ParseToWrite(Record to_write)
        {
            return to_write.rows + "split" + to_write.cols + "split" + to_write.score + "split" + to_write.level + "split" + to_write.dt;
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
        public bool CheckAndWriteScore(int rows, int cols, int new_score, string level)
        {
            string filename = ParseFilenameToWrite(rows, cols, level);
            Record old = ParseToRead(ReadText(filename));

            if((old.score < new_score && old.level == level) || old.level == "")
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
