using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.Logic;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife.Input
{
    public class PlayerInputCapture : IPlayerInputCapture
    {
        private List<Option> listOfAvailableOptions;
        IApplication _application;
        IValidator _validator;
        IOptions _options;

        public PlayerInputCapture(IApplication application, IValidator validator, IOptions options)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        public PlayerInput GetPlayersInput()
        {
            _application.WriteText(TextMessages.Welcome);

            PlayerInput playerInput = new PlayerInput();

            _application.WriteText(TextMessages.AskName);
            playerInput.PlayerName = _validator.GetValidatedName();

            listOfAvailableOptions = _options.GetList(playerInput.PlayerName);

            _application.WriteText(AskStartOption());
            playerInput.StartOption = _validator.GetValidatedOption(listOfAvailableOptions);
            if (playerInput.StartOption == Option.Multiple)
            {
                playerInput.FieldSize = NumericData.MultiFieldSize;
            }

            if (playerInput.StartOption != Option.Restore && playerInput.StartOption != Option.Multiple) 
            {
                _application.WriteText(TextMessages.AskFieldSize);
                playerInput.FieldSize = _validator.GetValidatedDimension();
            }
            return playerInput;
        }

        public List<int> GetPlayersFieldSelection()
        {
            List<int> indexes = new List<int>();
            _application.WriteText(TextMessages.AskFields);

            for (int i = 1; i <= NumericData.PrintedFieldCount; i++)
            {
                int input = _validator.GetValidatedIndex(indexes, i);
                indexes.Add(input);
            }
            return indexes;
        }
        private string AskStartOption()
        {
            string output = "Please choose game field set up: ";

            foreach (var option in listOfAvailableOptions)
            {
               output += (int)option + " - " + option + "  ";
            }

            return output;
        }

    }
}
