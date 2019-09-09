using GeneticAlgorithmMasterThesis.Classes;
using GeneticAlgorithmMasterThesis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.DataSet
{
    public class Satellite
    {
        public int Id { get; set; }             // This is an OrdinalNumber
        public int latitude { get; set; }       // Latitude of the satellite at turn 0 in arcseconds (− 324000 ≤ φ ≤ 324000 , which corresponds to − 90° ≤ φ ≤ 90 ),
        public int longitude { get; set; }      // Longitude of the satellite at turn 0 in arcseconds (− 648000 ≤ λ ≤ 647999 , which corresponds to − 180° ≤ λ ≤ 179°59′59′′),
        public Offset cameraOffsets { get; set; }
        public int velocity { get; set; }       // Velocity of the satellite at turn 0 in arcseconds per turn (100 ≤ |v| ≤ 500) ,
        public int W { get; set; }              // Maximum orientation change in each dimension in arcseconds per turn (0 ≤ w ≤ 200) ,
        public int D { get; set; }              // Maximum orientation value in each dimension in arcseconds (0 ≤ d ≤ 10000 ) .
        
        public Satellite()
        {
            cameraOffsets = new Offset();
        }


        /*
         * In degrees :
            abs( ((posInit + vitesse * temps - 90) %% 360) - 180 ) - 90
         * In arcseconds :
            abs( ((posInit + vitesse * temps - 324000) %% 1296000) - 648000 ) - 324000
         */
        public int CalculateLatitudePerTurn(int time)
        {
            return Math.Abs((Functions.Modulo(latitude + velocity * time - 324000, 1296000)) - 648000) - 324000;
        }


        /*
            * Longitude if satellite never go up
            *
            * In degrees :
               (posInit + vitesse * temps - 180) %% 360 - 180
            * In arcseconds :
               (posInit + vitesse * temps - 648000) %% 1296000 - 648000
        */
        public int CalculateLongitudePerTurn(int time)
        {
            int l = Functions.Modulo(longitude + Constants.EarthVelocity * time - 648000, 1296000) - 648000;
            // switch side because of latitude move
            if (SideT(time))
            {
                return l < 0 ? l + 648000 : l - 648000;
            }
            else
            {
                return l;
            }
        }

        private bool SideT(int t)
        {
            return Functions.Modulo((latitude + (t * velocity) + 324000) / 648000, 2) == 1;
        }

        //public bool canReach(Offset cameraOffsets_t)
        //{
        //    if (cameraOffsets_t.deltaLat > D || cameraOffsets_t.deltaLong > D)
        //        return false;

        //    double w_latitude = Math.Abs(cameraOffsets_t.deltaLat - cameraOffsets.deltaLat) / ((double)cameraOffsets_t.turn - (double)cameraOffsets.turn);
        //    double w_longitude = Math.Abs(cameraOffsets_t.deltaLong - cameraOffsets.deltaLong) / ((double)cameraOffsets_t.turn - (double)cameraOffsets.turn);
        //    return w_latitude < W && w_longitude < W;
        //}

        public Response canReach(Offset cameraOffsets_t)
        {
            Response response = new Response();
            if (cameraOffsets_t.deltaLat > D || cameraOffsets_t.deltaLong > D)
            {
                response.reach = false;
                return response;
            }

            double w_latitude = Math.Abs(cameraOffsets_t.deltaLat - cameraOffsets.deltaLat) / ((double)cameraOffsets_t.turn - (double)cameraOffsets.turn);
            double w_longitude = Math.Abs(cameraOffsets_t.deltaLong - cameraOffsets.deltaLong) / ((double)cameraOffsets_t.turn - (double)cameraOffsets.turn);
            // return w_latitude <= W && w_longitude <= W;

            response.w_latitude = w_latitude;
            response.w_longitude = w_longitude;
            response.reach = w_latitude <= (double)W && w_longitude <= (double)W;

            return response;
        }
    }
}