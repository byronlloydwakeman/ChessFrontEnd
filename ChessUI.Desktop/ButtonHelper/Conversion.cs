using ChessUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Desktop.ButtonHelper
{
    public static class Conversion
    {
        public static SquareUIModel ButtonNameToSquareModel(string buttonName)
        {
            //The name should by in the for x0y7
            //So the 1st and 3rd index should be x and y respectiveley 
            return new SquareUIModel() { XCoord = buttonName[1] - '0', YCoord = buttonName[3] - '0' };
        }
    }
}
