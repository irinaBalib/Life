using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameOfLife.SaveGame
{
     public class GameFile : IGameStorage
    {
        IApplication _application;
        public GameFile(IApplication application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }
        public void Save(string playername, List<IField> fields)
        {
            string filePath = $"{GetDirectoryPath()}{playername}.json";

            var fieldDTOs = ConvertFieldToDTO(fields);

            string jsonString = JsonConvert.SerializeObject(fieldDTOs); // TODO: remove future cells / DTO .cs

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                {
                    streamWriter.WriteLine(jsonString);
                }
            }
            catch (Exception e)
            {
                _application.WriteText(e.Message);
            }
        }
        
        public List<IField> Restore(string playername)
        {
           string filePath = $"{GetDirectoryPath()}{playername}.json";
            List<FieldDTO> fieldDTOs = new List<FieldDTO>();
                try
                {
                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        string jsonString = streamReader.ReadToEnd();
                        fieldDTOs = JsonConvert.DeserializeObject<List<FieldDTO>>(jsonString);  
                    }
                }
                catch (Exception e)
                {
                    _application.WriteText(e.Message);
                }

            List<IField> restoredFields = ConvertDtoToField(fieldDTOs);
        return restoredFields;
        }

       

        public bool DataExists(string playername)
        {
           string filePath = $"{GetDirectoryPath()}{playername}.json";
            return File.Exists(filePath);
        }
        private string GetDirectoryPath()
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\SavedGames\\";

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            return path;
        }

        private List<FieldDTO> ConvertFieldToDTO (List<IField> fields)
        {
            List<FieldDTO> fieldDTOs = new List<FieldDTO>();
            Parallel.ForEach(fields, field =>
            {
                fieldDTOs.Add(new FieldDTO { Cells = field.Cells, Generation = field.Generation, Dimension = field.Dimension });
            });
            return fieldDTOs;
        }
        private List<IField> ConvertDtoToField(List<FieldDTO> fieldDTOs)
        {
            List<IField> restoredFields = new List<IField>();
            Parallel.ForEach(fieldDTOs, fieldDTO =>
            {
                restoredFields.Add(new SquareField { Cells = fieldDTO.Cells, Generation = fieldDTO.Generation, Dimension = fieldDTO.Dimension, FutureCells = new bool[fieldDTO.Dimension, fieldDTO.Dimension] });
            });
            return restoredFields;
        }
    }
}
