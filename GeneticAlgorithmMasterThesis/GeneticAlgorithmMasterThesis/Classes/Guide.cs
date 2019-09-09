using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class Guide
    {
        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }
        public int Worth { get; set; }
        public int L { get; set; }          // number of locations to photograph for this collection
        public List<int> Photographs { get; set; }

        public Guide()
        {
            Photographs = new List<int>();
        }
    }
}