﻿using GameOfLife.Application;
using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    public class Setup : ISetup
    {
      //  public string PlayerName { get; set; }
        public int FieldSizeInput { get; set; }
        public Option StartOption { get; set; }
        public Message Message { get; set; }

        IApplication _application;
        
        IPlayer _player;
        
        public Setup(IApplication application, IPlayer player)
        {
            _application = application;
            _player = player;
            Message = new Message();
        }
        public void SetPlayersInput()
        {
            _application.WriteText(Message.Welcome);

            //if (string.IsNullOrEmpty(_player.Name))
            //{
               _application.WriteText(Message.AskName);
                _player.Name = GetValidatedNameInput(); ; 
            //}

            _application.WriteText(Message.AskStartOption(_player.HasSavedGame()));
             StartOption = GetValidatedOptionInput();
             
            if (StartOption != Option.RESTORE) 
            {
                _application.WriteText(Message.AskFieldSize);
                FieldSizeInput = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            var input = _application.ReadInput();

            while (string.IsNullOrEmpty(input))
            {
                _application.ShowErrorMessage(Message.BlankName);
                
                input = _application.ReadInput();
            }
            return input;
        }
        public int GetValidatedDimensionInput()
        {
            var inputIsValid = false;
            var dimensionInput = 0;

            while (!inputIsValid)
            {
               if (!int.TryParse(_application.ReadInput(), out dimensionInput))
                {
                    _application.ShowErrorMessage(Message.InvalidInput);
                }
                else if (dimensionInput < IField.MinSize || dimensionInput > IField.MaxSize) 
                {
                    _application.ShowErrorMessage(Message.OutOfRange);
                }
                else
                {
                    inputIsValid = true;
                }
            }
            return dimensionInput;
        }
        public Option GetValidatedOptionInput()
        {
            var optionIndex = 0;
            var isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(_application.ReadInput(), out optionIndex))
                    && Enum.IsDefined(typeof(Option), optionIndex)
                    || (optionIndex == (int)Option.RESTORE && _player.HasSavedGame());
                
                if (!isOptionValid)
                {
                    _application.ShowErrorMessage(Message.InvalidInput);
                }
            }
            return (Option)optionIndex;
        }
       
    }
}