using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GameOfLife.Data
{
   public class FileStorage : IDataStorage
    {

        public void Save(string playername, Field field)
        {
                string fileName = $"{playername}.json";
            string path = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\"+fileName;  //implement directory!!
           
                    string jsonString = JsonConvert.SerializeObject(field);

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    streamWriter.WriteLine(jsonString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public Field Restore(string playername)
        {
            Field restoredField = new Field();
            string fileName = $"{playername}.json";
            string path = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\" + fileName;

            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                     restoredField = JsonConvert.DeserializeObject<Field>(jsonString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return restoredField;
        }
    }
}
