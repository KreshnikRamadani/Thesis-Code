using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Utils
{
    public class Results
    {
        public int Fitness { get; set; }
        public int Gneration { get; set; }
        public TimeSpan SnapshotDate { get; set; }
    }
}