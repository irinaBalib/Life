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
       
        public TextMessages Message { get; set; }
        IApplication _application;
        IValidator _validator;
        IGameStorage _storage;
        
        public PlayerInputCapture(IApplication application, IValidator validator, IGameStorage storage)
        {
            _application = application;
            _validator = validator;
            _storage = storage;
            Message = new TextMessages();
        }
        public PlayerInput GetPlayersInput()
        {
            _application.WriteText(TextMessages.Welcome);

            PlayerInput playerInput = new PlayerInput();

            _application.WriteText(TextMessages.AskName);
                playerInput.PlayerName = _validator.ValidateName(); ; 
           
            _application.WriteText(AskStartOption(playerInput.PlayerName));
            playerInput.StartOption = _validator.ValidateOption(playerInput.PlayerName);
             
            if (playerInput.StartOption != Option.Restore) 
            {
                _application.WriteText(Message.AskFieldSize);
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
