using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var DTO = ConvertFieldToDTO(fields);

            string jsonString = JsonConvert.SerializeObject(DTO); 

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
            GameDTO gameDTO = new GameDTO();
                try
                {
                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        string jsonString = streamReader.ReadToEnd();
                        gameDTO = JsonConvert.DeserializeObject<GameDTO>(jsonString);  
                    }
                }
                catch (Exception e)
                {
                    _application.WriteText(e.Message);
                }

            List<IField> restoredFields = ConvertDtoToField(gameDTO);
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
        private GameDTO ConvertFieldToDTO (List<IField> fields)
        {
            List<FieldDTO> fieldDTOs = new List<FieldDTO>(); 

            foreach (IField field in fields)
            {
                fieldDTOs.Add(new FieldDTO { Cells = field.Cells });
            }

            GameDTO gameDTO = new GameDTO();
            gameDTO.FieldDTOs = fieldDTOs;
            gameDTO.Dimension = fields.FirstOrDefault().Dimension;
            gameDTO.Generation = fields.FirstOrDefault().Generation;
            
            return gameDTO;
        }
        private List<IField> ConvertDtoToField(GameDTO gameDTO)
        {
            List<IField> restoredFields = new List<IField>();
            foreach (var fieldDTO in gameDTO.FieldDTOs)
            {
                restoredFields.Add(new SquareField { Cells = fieldDTO.Cells, Generation = gameDTO.Generation, Dimension = gameDTO.Dimension, FutureCells = new bool[gameDTO.Dimension, gameDTO.Dimension] });
            }

            return restoredFields;
        }
    }
}
