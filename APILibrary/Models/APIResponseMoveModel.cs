using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary.Models
{
    public class APIResponseMoveModel
    {
        public bool IsAllowed { get; set; }
        public bool HasWon { get; set; }
        public bool HasDrawn { get; set; }
    }
}
