using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife.Input
{
    public class PlayerInputCapture : IPlayerInputCapture
    {
        IApplication _application;
        IValidator _validator;
        IGameStorage _storage;
        
        public PlayerInputCapture(IApplication application, IValidator validator, IGameStorage storage)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
        public PlayerInput GetPlayersInput()
        {
            _application.WriteText(TextMessages.Welcome);

            PlayerInput playerInput = new PlayerInput();

            _application.WriteText(TextMessages.AskName);
                playerInput.PlayerName = _validator.ValidateName(); 
           
            _application.WriteText(AskStartOption(playerInput.PlayerName));
            playerInput.StartOption = _validator.ValidateOption(playerInput.PlayerName);
             
            if (playerInput.StartOption != Option.Restore) 
            {
                _application.WriteText(TextMessages.AskFieldSize);
                playerInput.FieldSize = _validator.ValidateDimension();
            }
            return playerInput;
        }
        private string AskStartOption(string playerName)
        {
            string output = "Please choose game field set up ";

            if (_storage.DataExists(playerName))  // TODO: make class with list of available options, check storage there
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set, { (int)Option.Restore} - restore saved game): ";
            }
            else
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set): ";
            }
            return output;
        }

    }
}
