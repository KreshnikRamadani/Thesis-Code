using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.DataSet
{
    public class Collection
    {
        public int Id { get; set; }             // This is an OrdinalNumber       
        public int V { get; set; }              // Value (worth) of the collection (0 ≤ V ≤ 10000) ,
        public int L { get; set; }              // Number of locations to photograph for this collection (0 < L ≤ 100) ,
        public int R { get; set; }              // Number of time ranges during which images for this collection need to be taken (0 < R ≤ 100) .
        public List<TimeRange> TimeRanges { get; set; }
        public List<Location> Locations { get; set; }

        // Preprocessing
        public double avgLatitude { get; set; }
        public double avgLongitude { get; set; }
        public int quadrant { get; set; }
      //  public int section { get; set; }

        public Collection()
        {
            TimeRanges = new List<TimeRange>();
            Locations = new List<Location>();
        }

        public bool isInTimeRange(int t)
        {
            foreach (var timeRange in TimeRanges)
            {
                if (t >= timeRange.from && t <= timeRange.to)
                    return true;
            }
            return false;
        }
    }
}
