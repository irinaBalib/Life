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

        public void Save(string playername, IField field)
        {
            string filePath = $"{GetDirectoryPath()}{playername}.json"; 

            string jsonString = JsonConvert.SerializeObject(field);

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                {
                    streamWriter.WriteLine(jsonString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public SquareField Restore(string playername)
        {
           string filePath = $"{GetDirectoryPath()}{playername}.json";
            SquareField restoredField = new SquareField();

                try
                {
                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        string jsonString = streamReader.ReadToEnd();
                        restoredField = JsonConvert.DeserializeObject<SquareField>(jsonString);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            return restoredField;
        }

        private string GetDirectoryPath()
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\SavedGames\\";

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            return path;
        }

        public bool DataExists(string playername)
        {
           string filePath = $"{GetDirectoryPath()}{playername}.json";
            return File.Exists(filePath);
        }
    }
}
