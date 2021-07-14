using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife.Data
{
   public class FileStorage : IDataStorage
    {

        public void Save(string playername)
        {
            string path = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\{playername}.dat"; //directory
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    //writer.Write(Height);
                    //writer.Write(Width);
                    //writer.Write(Generation);
                    //foreach (Cell c in Cells)
                    //{
                    //    writer.Write(c.Id);
                    //    writer.Write(c.IsAlive);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void Restore(string playername)
        {

        }
    }
}
