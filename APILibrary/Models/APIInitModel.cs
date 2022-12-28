using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary.Models
{
    public class APIInitModel
    {
        public bool AgainstComputer { get; set; }
        public APIColor Player1Color { get; set; }
        public bool Player1GoFirst { get; set; }
    }
}
