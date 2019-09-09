using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{

    public class Details
    {
        public int SatId { get; set; }
        public bool Result { get; set; }
        public int ResultImpact { get; set; }
        public int turn { get; set; }
     
        public int? previousLocus { get; set; }             // Locus - Position of gene
        public bool isPrevious { get; set; }

        public int? secondPreviousLocus { get; set; }
        public bool isSecondPrevious { get; set; }

        public int? nextLocus { get; set; }
        public bool isNext { get; set; }

        public int? secondNextLocus { get; set; }
        public bool isSecondNext { get; set; }

        public Offset offset { get; set; }

        //public double z_prev_w_Latitude { get; set; }
        //public double z_prev_w_Longitude { get; set; }

        //public double z_next_w_Latitude { get; set; }
        //public double z_next_w_Longitude { get; set; }

        public Details()
        {
            Result = false;
            ResultImpact = 0;
            isPrevious = false;
            isSecondPrevious = false;
            isNext = false;
            isSecondNext = false;
            turn = -1;
            offset = new Offset();
        }
    }
}