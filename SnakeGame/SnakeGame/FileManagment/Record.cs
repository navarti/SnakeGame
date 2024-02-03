using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SnakeGame.FileManagment
{
    public struct Record
    {
        public int score;
        public int rows;
        public int cols;
        public DateTime dt;
        public char level;
        public Record() { score = -1; rows = 0; cols = 0; dt = new DateTime(); level = ' '; }

        static public Record ToStruct(byte[] ar)
        {
            byte[] data = ar;
            IntPtr ptPoit = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptPoit, data.Length);

            Record x = (Record)Marshal.PtrToStructure(ptPoit, typeof(Record));
            Marshal.FreeHGlobal(ptPoit);

            return x;
        }

        static public byte[] StructToByteArray(Record to_convert)
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

        public void Validate()
        {
            if (rows < SnakeGame.Pages.MainPageCategories.View.SelectGameView.MIN_DIMENSION ||
                cols < SnakeGame.Pages.MainPageCategories.View.SelectGameView.MIN_DIMENSION ||
                rows > SnakeGame.Pages.MainPageCategories.View.SelectGameView.MAX_DIMENSION ||
                cols > SnakeGame.Pages.MainPageCategories.View.SelectGameView.MAX_DIMENSION)
            {
                throw new Exception("Invalid dimensions");
            }

            else if (score < 0 || score > rows * cols)
            {
                throw new Exception("Invalid score");
            }
            else if (dt.Year < 2023 || dt.Year > 2100)
            {
                throw new Exception("Invalid year");
            }
            else if (!SnakeGame.Pages.MainPageCategories.View.RecordsView.levelDict.ContainsKey(level))
            {
                throw new Exception("Invalid level");
            }
        }
    }

    
}
