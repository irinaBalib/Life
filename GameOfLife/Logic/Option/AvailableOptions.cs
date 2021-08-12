using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.Logic
{
    public class AvailableOptions : IOptions
    {
        private static List<Option> listOfAllOptions = Enum.GetValues(typeof(Option)).Cast<Option>().ToList();
        private static List<Option> listOfAvailableOptions;

        IGameStorage _storage;
        public AvailableOptions(IGameStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
        public List<Option> GetList(string playerName)
        {
            if (!SavedGameExist(playerName))
            {
                ExcludeRestoreOption();
                return listOfAvailableOptions ?? throw new ArgumentNullException(nameof(listOfAvailableOptions));
            }
            else
            {
                return listOfAllOptions ?? throw new ArgumentNullException(nameof(listOfAvailableOptions));
            }
        }
        private static void ExcludeRestoreOption()
        {
            listOfAvailableOptions = listOfAllOptions.Where(option => option != Option.Restore).ToList();
        }

        private bool SavedGameExist(string playerName)
        {
            return _storage.DataExists(playerName);
        }
    }
}
