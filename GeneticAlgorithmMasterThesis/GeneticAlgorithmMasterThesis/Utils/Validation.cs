using GeneticAlgorithmMasterThesis.Algorithm;
using GeneticAlgorithmMasterThesis.Classes;
using GeneticAlgorithmMasterThesis.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Utils
{
    public static class Validation
    {

        public static bool checkIfLocationIsInTimeRange(Individ _individ, Inputs _inputs)
        {
            for (int i = 0; i < _individ.Chromosome.Length; i++)
            {
                foreach (var item in _individ.Chromosome[i])
                {
                    var collection = _inputs.collections.Single(x => x.Id == item.CollectionID);
                    if (!collection.isInTimeRange(item.CameraPosition.turn))
                        return false;
                }
            }
            return true;
        }


        /*
         * Characteristic of Method:
         * It recalculates from scratch all the positions of camera per turn between taking two consecutive pictures!
         */
        public static bool checkCameraOffsetForConsecutivePictures(Individ _individ, Inputs _inputs)
        {   
            double w_latitude = 0;
            double w_longitude = 0;
            for (int i = 0; i < _individ.Chromosome.Length; i++)
            {
                var satelliteContent = _individ.Chromosome[i].OrderBy(x => x.Turn).ToList();
                Offset CameraPosition_t0 = new Offset();
                Satellite satellite = _inputs.satellites[i];

                // foreach (var item in satelliteContent)
                for (int j = 0; j < satelliteContent.Count; j++)
                {
                    var item = satelliteContent[j];
                    if (item.CameraPosition.deltaLat > satellite.D || item.CameraPosition.deltaLong > satellite.D)
                        return false;

                    w_latitude = Math.Abs(item.CameraPosition.deltaLat - CameraPosition_t0.deltaLat) / ((double)item.CameraPosition.turn - (double)CameraPosition_t0.turn);
                    w_longitude = Math.Abs(item.CameraPosition.deltaLong - CameraPosition_t0.deltaLong) / ((double)item.CameraPosition.turn - (double)CameraPosition_t0.turn);

                    if (w_latitude > (double)satellite.W && w_longitude > (double)satellite.W)
                        return false;

                    CameraPosition_t0 = new Offset();
                    CameraPosition_t0.deltaLat = item.CameraPosition.deltaLat;
                    CameraPosition_t0.deltaLong = item.CameraPosition.deltaLong;
                    CameraPosition_t0.turn = item.CameraPosition.turn;
                }
            }
            return true;
        }

        /* BACK UP Method
         * 
         * public static bool checkCameraOffsetForConsecutivePictures(Individ _individ, Inputs _inputs)
           {
            double w_latitude = 0;
            double w_longitude = 0;
            for (int i = 0; i < _individ.Chromosome.Length; i++)
            {
                var satelliteContent = _individ.Chromosome[i].OrderBy(x => x.CameraPosition.turn).ToList();
                Offset CameraPosition_t0 = new Offset();
                Satellite satellite = _inputs.satellites[i];

                // foreach (var item in satelliteContent)
                for (int j = 0; j < satelliteContent.Count; j++)
                {

                    var item = satelliteContent[j];

                    if (item.CameraPosition.deltaLat > satellite.D || item.CameraPosition.deltaLong > satellite.D)
                        return false;

                    w_latitude = Math.Abs(item.CameraPosition.deltaLat - CameraPosition_t0.deltaLat) / ((double)item.CameraPosition.turn - (double)CameraPosition_t0.turn);
                    w_longitude = Math.Abs(item.CameraPosition.deltaLong - CameraPosition_t0.deltaLong) / ((double)item.CameraPosition.turn - (double)CameraPosition_t0.turn);

                    if (!(w_latitude <= (double)satellite.W && w_longitude <= (double)satellite.W))
                        return false;

                    CameraPosition_t0 = new Offset();
                    CameraPosition_t0.deltaLat = item.CameraPosition.deltaLat;
                    CameraPosition_t0.deltaLong = item.CameraPosition.deltaLong;
                    CameraPosition_t0.turn = item.CameraPosition.turn;
                }
            }
            return true;
        }
         
     */


        //public static bool _checkCameraOffsetForConsecutivePictures(Individ _individ, Inputs _inputs)
        //{
        //    double w_latitude = 0;
        //    double w_longitude = 0;
        //    for (int i = 0; i < _individ.Chromosome.Length; i++)
        //    {
        //        var satelliteContent = _individ._Chromosome[i].OrderBy(x => x.Key).ToList();
        //        Offset CameraPosition_t0 = new Offset();
        //        Satellite satellite = _inputs.satellites[i];
        //        foreach (var item in satelliteContent)
        //        {
        //            if (item.Value.CameraPosition.deltaLat > satellite.D || item.Value.CameraPosition.deltaLong > satellite.D)
        //                return false;

        //            w_latitude = Math.Abs(item.Value.CameraPosition.deltaLat - CameraPosition_t0.deltaLat) / ((double)item.Value.CameraPosition.turn - (double)CameraPosition_t0.turn);
        //            w_longitude = Math.Abs(item.Value.CameraPosition.deltaLong - CameraPosition_t0.deltaLong) / ((double)item.Value.CameraPosition.turn - (double)CameraPosition_t0.turn);

        //            if (!(w_latitude < satellite.W && w_longitude < satellite.W))
        //                return false;

        //            CameraPosition_t0 = new Offset();
        //            CameraPosition_t0.deltaLat = item.Value.CameraPosition.deltaLat;
        //            CameraPosition_t0.deltaLong = item.Value.CameraPosition.deltaLong;
        //            CameraPosition_t0.turn = item.Value.CameraPosition.turn;
        //        }
        //    }
        //    return true;
        //}



        //public static bool checkCameraOffsetForConsecutivePictures(Individ _individ, Inputs _inputs)
        //{
        //    for (int i = 0; i < _individ.Chromosome.Length; i++)
        //    {
        //        foreach (var gene in _individ.Chromosome[i])
        //        {
        //            Satellite satellite = _inputs.satellites[i];
        //            if (gene.CameraPosition.w_lat > satellite.W || gene.CameraPosition.w_long > satellite.W)
        //                return false;
        //        }
        //    }
        //    return true;
        //}


        public static bool checkIfOneLocationIsCapturedOnce(Individ _individ)
        {
            List<int> locationsCaptured = new List<int>();
            for (int i = 0; i < _individ.Chromosome.Length; i++)
            {
                foreach (var location in _individ.Chromosome[i])
                {
                    if (locationsCaptured.Contains(location.LocationID))
                        return false;

                    locationsCaptured.Add(location.LocationID);
                }
            }
            return true;
        }

        public static bool checkIfAtMostOneLocationIsCapturedPerTurn(Individ _individ)
        {
            for (int i = 0; i < _individ.Chromosome.Length; i++)
            {
                var turnsList = _individ.Chromosome[i].Select(x => x.CameraPosition.turn).ToList();
                var uniqueTurns = turnsList.Distinct().ToList();
                if (turnsList.Count != uniqueTurns.Count)
                    return false;
            }
            return true;
        }

    }
}