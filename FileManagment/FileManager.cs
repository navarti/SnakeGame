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
        readonly string filename_folder = "scores//";
        readonly string extension = ".txt";

        public string[] GetRecordFiles()
        {
            return Directory.GetFiles(filename_folder);
        }



        string GetFileName(int rows, int cols)
        {
            return filename_folder + rows + "_" + cols + extension;
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

        public void WriteText(string text, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(text);
            }
        }

        Tuple<int, int> ParseToRead(string text)
        {
            if(text == "") return Tuple.Create(0, 0);
            string[] data = text.Split();
            int score = 0;
            int date = 0;
            if (!int.TryParse(data[0], out score) || !int.TryParse(data[1], out date))
            {
                //
            }
            return Tuple.Create(score, date);
        }

        string ParseToWrite(int score, int date)
        {
            return score + " " + date;
        }

        public Tuple<int, int> GetScore(int rows, int cols)
        {
            string filename = GetFileName(rows, cols);
            return ParseToRead(ReadText(filename));
        }

        public bool CheckAndWriteScore(int rows, int cols, int new_score)
        {
            string filename = GetFileName(rows, cols);
            int old_score = ParseToRead(ReadText(filename)).Item1;

            if(old_score < new_score)
            {
                DateTime dateTime = DateTime.Now;
                int date = dateTime.Day;
                string to_write = ParseToWrite(new_score, date);
                return true;
            }

            return false;
        }
    }
}
