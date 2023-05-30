using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Controls;

namespace SnakeGame.FileManagment
{
    internal class FileManager
    {
        readonly string filename_folder = "scores\\";
        readonly string extension = ".bin";
        readonly char separator= '_';
        List<string> files_ignore = new List<string>();

        public FileManager()
        {
            if (!Directory.Exists(filename_folder))
            {
                DirectoryInfo di = Directory.CreateDirectory(filename_folder);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                dispatcher.BeginInvoke(new Action(() => MessageBox.Show("New folder with records have been created. Go ahead to be the top!")));
            }
        }

        bool CheckFilename(string filename)
        {
            if (!filename.EndsWith(extension)) return false;
            
            filename = filename.Substring(filename_folder.Length, filename.Length - (filename_folder.Length + extension.Length));
            string[] parts = filename.Split(separator);

            int rows, cols;
            if (!int.TryParse(parts[0], out rows)) return false;
            if (!int.TryParse(parts[1], out cols)) return false;

            if (rows < Pages.MainPageCategories.View.SelectGameView.MIN_DIMENSION ||
                cols < Pages.MainPageCategories.View.SelectGameView.MIN_DIMENSION ||
                rows > Pages.MainPageCategories.View.SelectGameView.MAX_DIMENSION ||
                cols > Pages.MainPageCategories.View.SelectGameView.MAX_DIMENSION)
            {
                return false;
            }

            if (parts[2].Length != 1) return false;
            if (!Pages.MainPageCategories.View.RecordsView.levelDict.ContainsKey(parts[2][0])) return false;

            return true;
        } 

        // для Records
        public List<Record> GetAllRecords()
        {
            string[] filenames = Directory.GetFiles(filename_folder);
            List<Record> result = new List<Record>();
            for (int i = 0; i < filenames.Length; i++)
            {
                
                if (files_ignore.Contains(filenames[i])) continue;
                if (!CheckFilename(filenames[i]))
                {
                    if (Task.Run(() => MessageBox.Show($"The file {filenames[i]} is unrecognised. Should it be removed?",
                                                                "", MessageBoxButton.YesNo, MessageBoxImage.Question)).Result == MessageBoxResult.Yes)
                    {
                        File.SetAttributes(filenames[i], FileAttributes.Normal);
                        File.Delete(filenames[i]);
                    }
                    else
                    {
                        files_ignore.Add(filenames[i]);
                    }
                    throw new Exception("Unrecognised file");
                }
                try
                {
                    result.Add(ReadRecord(filenames[i]));
                }
                catch (Exception)
                {
                    File.SetAttributes(filenames[i], FileAttributes.Normal);
                    File.Delete(filenames[i]);
                    Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                    dispatcher.BeginInvoke(new Action(() => MessageBox.Show($"The file {filenames[i]} was damaged. The data was deleted")));
                    throw new Exception("Damaged file");
                }
            }
            return result;
        }

        string ParseFilenameToWrite(int rows, int cols, string level)
        {
            return filename_folder + rows + separator + cols + separator + level[0] + extension;
        }


        Record ReadRecord(string filename)
        {
            if (!File.Exists(filename))
            {
                return new Record();
            }
            File.SetAttributes(filename, FileAttributes.Normal);

            byte[] buffer = new byte[Marshal.SizeOf(typeof(Record))];
            
            FileStream fs = new FileStream(filename, FileMode.Open);
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            File.SetAttributes(filename, FileAttributes.ReadOnly | FileAttributes.Hidden);
            Record to_return = Record.ToStruct(buffer);
            to_return.Validate();
            return to_return;
        }

        bool WriteRecord(Record record, string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.SetAttributes(filename, FileAttributes.Normal);
                }
                byte[] buf = Record.StructToByteArray(record);
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buf);
                bw.Close();
                bw = null;
                File.SetAttributes(filename, FileAttributes.ReadOnly | FileAttributes.Hidden);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        

        //для GamePage
        public bool CheckAndWriteScore(int rows, int cols, int new_score, string level)
        {
            string filename = ParseFilenameToWrite(rows, cols, level);
            Record old = new Record();

            bool overWrite = false;
            try
            {
                old = ReadRecord(filename);
            }
            catch (Exception ex)
            {
                overWrite = true;
                MessageBox.Show("File with current record was damaged and will be overwritten with this score");
            }

            if ((old.score < new_score && old.level == level[0]) || overWrite || old.level == ' ')
            {
                Record new_record = new Record();
                new_record.rows = rows;
                new_record.cols = cols;
                new_record.score = new_score;
                new_record.level = level[0];
                new_record.dt = DateTime.Now;
                WriteRecord(new_record, filename);
                return true;
            }
            return false;
        }
    }
}
