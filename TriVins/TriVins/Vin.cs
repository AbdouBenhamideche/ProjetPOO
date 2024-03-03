using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriVins
{
    internal class Vin
    {
        public double alcohol { get; set; }
        public double sulphates { get; set; }
        public double citricacid { get; set; }
        public double volatileacidity { get; set; }
        public int quality { get; set; }

  
        public Vin() { }
    }
}
