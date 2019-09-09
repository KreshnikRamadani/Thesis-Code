using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class Test
    {
        public int deltaLat { get; set; }
        public int deltaLong { get; set; }



        public int location_id { get; set; }


        public int previousGene_id { get; set; }
        public double w_lat_previous { get; set; }
        public double w_long_previous { get; set; }
        public bool flag_previous = false;

        public int nextGene_id { get; set; }
        public double w_lat_next { get; set; }
        public double w_long_next { get; set; }
        public bool flag_next = false;

    }
}