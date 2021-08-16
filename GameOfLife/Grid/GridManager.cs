using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.Grid
{
    public class GridManager : IGridManager
    {
        private List<IField> listOfFields;
        IApplication _application;
        IFieldManager _fieldManager;
        public GridManager(IApplication application, IFieldManager fieldManager)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _fieldManager = fieldManager ?? throw new ArgumentNullException(nameof(fieldManager));
        }

        public void SetGridContent(Option option, int fieldSize, string playerName)
        {
            listOfFields = new List<IField>();
            int fieldCount;
            if (option == Option.Multiple)
            {
                fieldCount = NumericData.MultiFieldGrid;
            }
            else
            {
                fieldCount = NumericData.SingleFieldGrid;
            }

            for (int i = 0; i < fieldCount; i++)
            {
                IField field = _fieldManager.GetField(option, fieldSize, playerName);
                listOfFields.Add(field);
            }
        }
        public int CountAliveCells()
        {
            int liveCellCount = 0;

            foreach (IField field in listOfFields)
            {
               liveCellCount += _fieldManager.CountAliveCells(field);
            }
            return liveCellCount;
        }

        public void SaveGridData(string playerName)
        {
            foreach (IField field in listOfFields)
            {
                _fieldManager.SaveField(playerName, field);
            }
        }

        public int GetGeneration()
        {
            return listOfFields.FirstOrDefault().Generation;
        }

        public void LoopGridData()  //  separate with and without printing
        {
            foreach (IField field in listOfFields)
            {
                _fieldManager.PrintCurrentSetFuture(field);

                _application.EmptyLine();
            }
        }
        public void UpdateGridData()
        {
            foreach (IField field in listOfFields)
            {
                _fieldManager.UpdateFieldData(field);
                field.Generation++;
            }
        }

        
    }
    }
