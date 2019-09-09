using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class Offset
    {
        public int deltaLat { get; set; }
        public int deltaLong { get; set; }
        public int turn { get; set; }
        //public double w_lat { get; set; }
        //public double w_long { get; set; }

        public Offset()
        {
            deltaLat = 0;
            deltaLong = 0;
            turn = 0;
            //w_lat = -1;
            //w_long = -1;
        }
    }
}