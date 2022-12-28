using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Library.Models
{
    public class SquareUIModel
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public override bool Equals(object obj)
        {
            SquareUIModel square2 = (SquareUIModel)obj;
            if(XCoord == square2.XCoord && YCoord == square2.YCoord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
