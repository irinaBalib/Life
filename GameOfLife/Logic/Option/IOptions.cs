using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Logic
{
    public interface IOptions
    {
        List<Option> GetList(string playerName);
    }
}