using GeneticAlgorithmMasterThesis.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class Gene
    {
        public int satId { get; set; }
        public int satLatitude { get; set; }
        public int satLongitude { get; set; }
        public int Turn { get; set; }

        public Offset CameraPosition { get; set; }

        public int CollectionID { get; set; }       // USAGE: During development process. 
        public int LocationID { get; set; }
        //public int latitude { get; set; }          // Definitely this should delete after development process
        //public int longitude { get; set; }         // Definitely this should delete after development process

        public Gene()
        {
            CameraPosition = new Offset();
        }


    }
}