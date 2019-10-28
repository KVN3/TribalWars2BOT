using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Library.Models
{
    public struct Village
    {
        public string name;
        public Coordinates coordinates;

        public Village(string name, Coordinates coordinates)
        {
            this.name = name;
            this.coordinates = coordinates;
        }



        
    }
}
