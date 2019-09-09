using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class BasicInfo
    {
        public int Worth { get; set; }
        public int L { get; set; }          // number of locations to photograph for this collection
        public List<int> Photographs { get; set; }
        public List<int> Participation { get; set; }      // SatelliteIDs capture its locations.

        public BasicInfo()
        {
            Photographs = new List<int>();
            Participation = new List<int>();
        }
    }
}