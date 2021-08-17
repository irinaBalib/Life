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
            playerInput.PlayerName = _validator.ValidateName();

            listOfAvailableOptions = _options.GetList(playerInput.PlayerName);

            _application.WriteText(AskStartOption());
            playerInput.StartOption = _validator.ValidateOption(listOfAvailableOptions);
            if (playerInput.StartOption == Option.Multiple)
            {
                playerInput.FieldSize = NumericData.MultiFieldSize;
            }

            if (playerInput.StartOption != Option.Restore && playerInput.StartOption != Option.Multiple) 
            {
                _application.WriteText(TextMessages.AskFieldSize);
                playerInput.FieldSize = _validator.ValidateDimension();
            }
            return playerInput;
        }
        private string AskStartOption()
        {
            string output = "Please choose game field set up: ";

            foreach (var option in listOfAvailableOptions)
            {
               output += (int)option + " - " + option + "  ";
            }

            //if (_storage.DataExists(playerName))  
            //{
            //    output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set, { (int)Option.Restore} - restore saved game): ";
            //}
            //else
            //{
            //    output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set): ";
            //}
            return output;
        }

    }
}
