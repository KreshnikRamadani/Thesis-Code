using GeneticAlgorithmMasterThesis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.DataSet
{
    public class Inputs
    {
        // Public Members
        public int duration { get; set; }                       // The duration of the simulation in turns.
        public int numberOfSatelites { get; set; }              // Number of satellites
        public int numberOfCollections { get; set; }
        public List<Satellite> satellites { get; set; }
        public List<Collection> collections { get; set; }
        public List<Location> locations { get; set; }

        // Test
        public List<int> AvailableCollectionsValues { get; set; }
        public List<Preprocessing> PreprocessingData { get; set; }


        public Inputs()
        {
            satellites = new List<Satellite>();
            collections = new List<Collection>();
            locations = new List<Location>();
            PreprocessingData = new List<Preprocessing>();
            AvailableCollectionsValues = new List<int>();
        }


        public void calculatePreprocessing()
        {
            // For each satellite
            foreach (var satellite in satellites)
            {
                for (int t = 0; t < duration; t++)
                {
                    Preprocessing preprocessing = new Preprocessing();
                    preprocessing.SatID = satellite.Id;
                    preprocessing.turn = t;
                    preprocessing.latitude = satellite.CalculateLatitudePerTurn(t);   // Update Latitude
                    preprocessing.longitude = satellite.CalculateLongitudePerTurn(t);  // Update Longitude
                    preprocessing.quadrant = Functions.CoordinateQadrant(preprocessing.latitude, preprocessing.longitude);
                    PreprocessingData.Add(preprocessing);
                }
            }
        }
    }
}