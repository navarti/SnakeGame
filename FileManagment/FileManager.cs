using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace SnakeGame.FileManagment
{
    internal class FileManager
    {
        readonly string filename_folder = "scores\\";
        readonly string extension = ".bin";

        readonly string levels = "EasyMediumHardAI";

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


        public List<Record> GetAllRecords()
        {
            string[] filenames = Directory.GetFiles(filename_folder);
            List<Record> result = new List<Record>();
            for (int i = 0; i < filenames.Length; i++)
            {
                try
                {
                    result.Add(ReadRecord(filenames[i]));
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


        [SecurityCritical]
        [HandleProcessCorruptedStateExceptions]
        Record ReadRecord(string filename)
        {
            if (!File.Exists(filename))
            {
                return new Record();
            }
            File.SetAttributes(filename, FileAttributes.Normal);

            byte[] buffer = new byte[Marshal.SizeOf(typeof(Record))];
            try
            {
                Record toReturn = new Record();
                FileStream fs = new FileStream(filename, FileMode.Open);
                fs.Read(buffer, 0, buffer.Length);
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                toReturn = (Record)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                                                        typeof(Record));
                handle.Free();
                if (fs.Position >= fs.Length)
                    fs.Close();
                File.SetAttributes(filename, FileAttributes.ReadOnly | FileAttributes.Hidden);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool WriteRecord(Record record, string filename)
        {
            try
            {
                byte[] buf = StructToByteArray(record);
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buf);
                bw.Close();
                bw = null;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private byte[] StructToByteArray(Record to_convert)
        {
            try
            {
                byte[] buffer = new byte[Marshal.SizeOf(to_convert)];
                GCHandle h = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(to_convert, h.AddrOfPinnedObject(), false);
                h.Free(); 
                return buffer; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

            if ((old.score < new_score && old.level == level) || overWrite || old.level == "")
            {
                Record new_record = new Record();
                new_record.rows = rows;
                new_record.cols = cols;
                new_record.score = new_score;
                new_record.level = level;
                new_record.dt = DateTime.Now;
                WriteRecord(new_record, filename);
                return true;
            }
            return false;
        }
    }
}
