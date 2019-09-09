using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Utils
{
    public static class Functions
    {
        public static Random randomGenerator;
        static Functions()
        {
            randomGenerator = new Random(DateTime.Now.Millisecond);
        }

        public static int Modulo(int a, int b)
        {
            int result = a % b;
            return result >= 0 ? result : result + b;
        }

        public static double Distance(int x1, int y1, int x2, int y2)
        {
            // return Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y2 - y1), 2));
        }


        //public static Triple UpdateSatelliteLocation(int _lat, int _long, int _v)
        //{
        //    Triple _triple = new Triple();
        //    //  t + 1            
        //    if ((-90 * 60 * 60) <= (_lat + _v) && (_lat + _v) <= (90 * 60 * 60))    // if − 90° ≤ φt + vt ≤ 90°
        //    {
        //        _triple.latitude = _lat + _v;               //  φt + vt
        //        _triple.longitude = _long - 15;             //  λt − 15′′
        //        //   velocity vt doesn't change             //  vt
        //        _triple.velocity = _v;
        //    }
        //    else if ((_lat + _v) > (90 * 60 * 60))    //  if φt + vt > 90°  (satellite flew over the North Pole)
        //    {
        //        _triple.latitude = (180 * 60 * 60) - (_lat + _v);           //  180° − (φt + vt)
        //        _triple.longitude = (-180 * 60 * 60) + (_long - 15);        //  −180° + (λt − 15′′)
        //        _triple.velocity = _v * (-1);                               //  − vt
        //    }
        //    else if ((_lat + _v) < (90 * 60 * 60 * (-1)))
        //    {
        //        _triple.latitude = (-180 * 60 * 60) - (_lat + _v);          //  − 180° − (φt + vt)
        //        _triple.longitude = (-180 * 60 * 60) + (_long - 15);        //  − 180° + (λt − 15′′)
        //        _triple.velocity = _v * (-1);                               //  − vt
        //    }
        //    return _triple;
        //}



        public static int CoordinateQadrant(int latitude, int longitude)
        {
            if (latitude >= 0 && longitude >= 0)        // North-East
                return Constants.NE;

            else if (latitude >= 0 && longitude < 0)    // North-West
                return Constants.NW;

            else if (latitude < 0 && longitude < 0)     // South-West
                return Constants.SW;

            else if (latitude < 0 && longitude >= 0)    // South-East
                return Constants.SE;

            return -1;
        }
        
        //// Divide quadrant in section
        //public static int assignSection(int latitude)
        //{
        //    if (Math.Abs(latitude) <= Constants.sectionSize_NS)
        //        return 1;
        //    else if (Math.Abs(latitude) >= Constants.sectionSize_NS && Math.Abs(latitude) <= 2 * Constants.sectionSize_NS)
        //        return 2;
        //    else if (Math.Abs(latitude) >= 2 * Constants.sectionSize_NS && Math.Abs(latitude) <= 3 * Constants.sectionSize_NS)
        //        return 3;
        //    else
        //        return 4;
        //}
    }
}