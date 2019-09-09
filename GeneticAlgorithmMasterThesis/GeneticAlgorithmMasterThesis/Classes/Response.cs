using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class Response
    {
        public double w_latitude { get; set; }
        public double w_longitude { get; set; }
        public bool reach { get; set; }

        public Response()
        {
            w_latitude = -1;
            w_longitude = -1;
            reach = false;
        }
    }
}