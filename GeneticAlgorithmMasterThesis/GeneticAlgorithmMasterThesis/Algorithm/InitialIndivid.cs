using Force.DeepCloner;
using GeneticAlgorithmMasterThesis.Classes;
using GeneticAlgorithmMasterThesis.DataSet;
using GeneticAlgorithmMasterThesis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Algorithm
{
    public class InitialIndivid
    {
        // public async Task<Individ> GetRandomIndivid(Inputs _inputs)
        public Individ GetRandomIndivid(Inputs _inputs)
        {
            Individ individ = new Individ();
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            individ.InitializeChromosome(_inputs);
            List<Collection> collections = _inputs.collections.DeepClone();        // All collections
            List<Collection> quadrantCollections = new List<Collection>();     // Collections filtered by quadrant
            List<Collection> collections_t = new List<Collection>();
            List<int> satellites = _inputs.satellites.Select(x => x.Id).ToList();
            Offset OrientationVector_t = new Offset();
            int satLatitude = 0;
            int satLongitude = 0;
            int currentSatQuadrant = 0;
            int quadrant = 0;

            int randomIndex = 0;
            int satCounter = _inputs.numberOfSatelites;
            while (satCounter > 0)
            {
                randomIndex = randomGenerator.Next(0, satellites.Count);
                int satId = satellites[randomIndex];
                Satellite satellite = _inputs.satellites[satId];

                for (int turn = 0; turn < _inputs.duration; turn++)
                {
                    // 1.   Satellite is moving - Update satellite location
                    satLatitude = satellite.CalculateLatitudePerTurn(turn);
                    satLongitude = satellite.CalculateLongitudePerTurn(turn);

                    // 2.   The satellites won’t be required to take an image of any location above latitude 85° (north or south).
                    if (Math.Abs(satLatitude) > Constants.EightyFiveDegree)
                        continue;

                    // 3.   Consider satellite covering prerequisite - max orientation value of the satellites
                    OrientationRange Lat_bounds = new OrientationRange();
                    Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
                    Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

                    OrientationRange Long_bounds = new OrientationRange();
                    Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
                    Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

                    currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    if (currentSatQuadrant != quadrant)
                    {
                        quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
                        quadrant = currentSatQuadrant;
                    }

                    // collections_t = quadrantCollections;

                    // 4.Consider Collections included inside the satellite window
                    //collections_t = quadrantCollections.Where(x => (x.avgLatitude <= Lat_bounds.MaxBound &&
                    //                                         x.avgLatitude >= Lat_bounds.MinBound) &&
                    //                                         (x.avgLongitude <= Long_bounds.MaxBound &&
                    //                                         x.avgLongitude >= Long_bounds.MinBound)).ToList();

                    int lat_max = Lat_bounds.MaxBound;
                    int lat_min = Lat_bounds.MinBound;
                    int long_max = Long_bounds.MaxBound;
                    int long_min = Long_bounds.MinBound;

                    collections_t = quadrantCollections.Where(x => (x.avgLatitude <= lat_max &&
                                                            x.avgLatitude >= lat_min) &&
                                                            (x.avgLongitude <= long_max &&
                                                            x.avgLongitude >= long_min)).ToList();

                    if (collections_t.Count > 0)
                    {
                        randomIndex = randomGenerator.Next(0, collections_t.Count);
                        var collection = collections_t[randomIndex];

                        // 4.   Consider Collection is in t
                        if (collection.isInTimeRange(turn))
                        {
                            // This selection should to improve
                            randomIndex = randomGenerator.Next(0, collection.Locations.Count);
                            var location = collection.Locations[randomIndex];

                            OrientationVector_t = new Offset();
                            OrientationVector_t.deltaLat = Math.Abs(satLatitude - location.Latitude);
                            OrientationVector_t.deltaLong = Math.Abs(satLongitude - location.Longitude);
                            OrientationVector_t.turn = turn;

                            // 6  Consider orientation of the satellite not exceed the value of D arcseconds.
                            Response res = satellite.canReach(OrientationVector_t);

                            // 7.  Check if location is reachable by camera
                            if (res.reach)
                            {
                                // 8.   Save this shoot
                                individ.AddGene(OrientationVector_t, satellite.Id, satLatitude, satLongitude, collection.Id, location.Id);

                                // Update position of Camera in Satellite
                                satellite.cameraOffsets.deltaLat = OrientationVector_t.deltaLat;
                                satellite.cameraOffsets.deltaLong = OrientationVector_t.deltaLong;
                                satellite.cameraOffsets.turn = OrientationVector_t.turn;
                                //satellite.cameraOffsets.w_lat = res.w_latitude;
                                //satellite.cameraOffsets.w_long = res.w_longitude;

                                int indx = collections.IndexOf(collection);
                                collections[indx].Locations.Remove(location);
                                if (collections[indx].Locations.Count == 0)
                                    collections.RemoveAt(indx);

                                indx = quadrantCollections.IndexOf(collection);
                                quadrantCollections[indx].Locations.Remove(location);
                                if (quadrantCollections[indx].Locations.Count == 0)
                                    quadrantCollections.RemoveAt(indx);
                            }
                        }
                    }
                }
                satellite.cameraOffsets = new Offset(); // Clear Camera Offset
                satellites.Remove(satId);
                satCounter--;
            }

            // 9.   Estimate Fitness
            individ.EstimateFitness();
            return individ;
        }



        // This is just for test only.
        public Individ getNewRandomIndivid(Inputs _inputs)
        {
            Individ individ = new Individ();
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            individ.InitializeChromosome(_inputs);
            List<Collection> collections = _inputs.collections.DeepClone();        // All collections
            List<Collection> quadrantCollections = new List<Collection>();     // Collections filtered by quadrant
            List<Collection> collections_t = new List<Collection>();
            List<int> satellites = _inputs.satellites.Select(x => x.Id).ToList();
            Offset OrientationVector_t = new Offset();
            int satLatitude = 0;
            int satLongitude = 0;
            int currentSatQuadrant = 0;
            int quadrant = 0;

            int randomIndex = 0;
            int satCounter = _inputs.numberOfSatelites;

            int loc_counter = 0;
            List<int> locationAdded = new List<int>();
            
            for (int i = 0; i < _inputs.numberOfSatelites; i++)
            {
                Satellite satellite = _inputs.satellites[i];

                for (int turn = 0; turn < _inputs.duration; turn++)
                {
                    satLatitude = satellite.CalculateLatitudePerTurn(turn);
                    satLongitude = satellite.CalculateLongitudePerTurn(turn);

                    if (Math.Abs(satLatitude) > Constants.EightyFiveDegree)
                        continue;

                    // 3.   Consider satellite covering prerequisite - max orientation value of the satellites
                    OrientationRange Lat_bounds = new OrientationRange();
                    Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
                    Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

                    OrientationRange Long_bounds = new OrientationRange();
                    Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
                    Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

                    currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    if (currentSatQuadrant != quadrant)
                    {
                        quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
                        quadrant = currentSatQuadrant;
                    }

                    int lat_max = Lat_bounds.MaxBound;
                    int lat_min = Lat_bounds.MinBound;
                    int long_max = Long_bounds.MaxBound;
                    int long_min = Long_bounds.MinBound;

                    collections_t = quadrantCollections.Where(x => (x.avgLatitude <= lat_max &&
                                                            x.avgLatitude >= lat_min) &&
                                                            (x.avgLongitude <= long_max &&
                                                            x.avgLongitude >= long_min)).ToList();

                    if (collections_t.Count > 0)
                    {
                        randomIndex = randomGenerator.Next(0, collections_t.Count);
                        var collection = _inputs.collections.Single(x=>x.Id==481);
                        foreach (var location in collection.Locations)
                        {
                            if (locationAdded.Contains(location.Id) == false)
                            {


                                OrientationVector_t = new Offset();
                                OrientationVector_t.deltaLat = Math.Abs(satLatitude - location.Latitude);
                                OrientationVector_t.deltaLong = Math.Abs(satLongitude - location.Longitude);
                                OrientationVector_t.turn = turn;

                                // 6  Consider orientation of the satellite not exceed the value of D arcseconds.
                                Response res = satellite.canReach(OrientationVector_t);
                                if (res.reach)
                                {

                                    loc_counter++;

                                    // Update position of Camera in Satellite
                                    satellite.cameraOffsets.deltaLat = OrientationVector_t.deltaLat;
                                    satellite.cameraOffsets.deltaLong = OrientationVector_t.deltaLong;
                                    satellite.cameraOffsets.turn = OrientationVector_t.turn;

                                    locationAdded.Add(location.Id);
                                    break;
                                }
                                
                            }

                        }
                    }



                }
            }



/*

            while (satCounter > 0)
            {
                randomIndex = randomGenerator.Next(0, satellites.Count);
                int satId = satellites[randomIndex];
                Satellite satellite = _inputs.satellites[satId];

                for (int turn = 0; turn < _inputs.duration; turn++)
                {
                    // 1.   Satellite is moving - Update satellite location
                    satLatitude = satellite.CalculateLatitudePerTurn(turn);
                    satLongitude = satellite.CalculateLongitudePerTurn(turn);

                    // 2.   The satellites won’t be required to take an image of any location above latitude 85° (north or south).
                    if (Math.Abs(satLatitude) > Constants.EightyFiveDegree)
                        continue;

                    // 3.   Consider satellite covering prerequisite - max orientation value of the satellites
                    OrientationRange Lat_bounds = new OrientationRange();
                    Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
                    Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

                    OrientationRange Long_bounds = new OrientationRange();
                    Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
                    Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

                    currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    if (currentSatQuadrant != quadrant)
                    {
                        quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
                        quadrant = currentSatQuadrant;
                    }

                    // collections_t = quadrantCollections;

                    // 4.Consider Collections included inside the satellite window
                    //collections_t = quadrantCollections.Where(x => (x.avgLatitude <= Lat_bounds.MaxBound &&
                    //                                         x.avgLatitude >= Lat_bounds.MinBound) &&
                    //                                         (x.avgLongitude <= Long_bounds.MaxBound &&
                    //                                         x.avgLongitude >= Long_bounds.MinBound)).ToList();

                    int lat_max = Lat_bounds.MaxBound;
                    int lat_min = Lat_bounds.MinBound;
                    int long_max = Long_bounds.MaxBound;
                    int long_min = Long_bounds.MinBound;

                    collections_t = quadrantCollections.Where(x => (x.avgLatitude <= lat_max &&
                                                            x.avgLatitude >= lat_min) &&
                                                            (x.avgLongitude <= long_max &&
                                                            x.avgLongitude >= long_min)).ToList();

                    if (collections_t.Count > 0)
                    {
                        randomIndex = randomGenerator.Next(0, collections_t.Count);
                        var collection = collections_t[randomIndex];

                        // 4.   Consider Collection is in t
                        if (collection.isInTimeRange(turn))
                        {
                            // This selection should to improve
                            randomIndex = randomGenerator.Next(0, collection.Locations.Count);
                            var location = collection.Locations[randomIndex];

                            OrientationVector_t = new Offset();
                            OrientationVector_t.deltaLat = Math.Abs(satLatitude - location.Latitude);
                            OrientationVector_t.deltaLong = Math.Abs(satLongitude - location.Longitude);
                            OrientationVector_t.turn = turn;

                            // 6  Consider orientation of the satellite not exceed the value of D arcseconds.
                            Response res = satellite.canReach(OrientationVector_t);

                            // 7.  Check if location is reachable by camera
                            if (res.reach)
                            {
                                // 8.   Save this shoot
                                individ.AddGene(OrientationVector_t, satellite.Id, satLatitude, satLongitude, collection, location, res, null);

                                // Update position of Camera in Satellite
                                satellite.cameraOffsets.deltaLat = OrientationVector_t.deltaLat;
                                satellite.cameraOffsets.deltaLong = OrientationVector_t.deltaLong;
                                satellite.cameraOffsets.turn = OrientationVector_t.turn;
                                //satellite.cameraOffsets.w_lat = res.w_latitude;
                                //satellite.cameraOffsets.w_long = res.w_longitude;

                                int indx = collections.IndexOf(collection);
                                collections[indx].Locations.Remove(location);
                                if (collections[indx].Locations.Count == 0)
                                    collections.RemoveAt(indx);

                                indx = quadrantCollections.IndexOf(collection);
                                quadrantCollections[indx].Locations.Remove(location);
                                if (quadrantCollections[indx].Locations.Count == 0)
                                    quadrantCollections.RemoveAt(indx);
                            }
                        }
                    }
                }
                satellite.cameraOffsets = new Offset(); // Clear Camera Offset
                satellites.Remove(satId);
                satCounter--;
            }
            */

            // 9.   Estimate Fitness
            individ.EstimateFitness();
            return individ;
        }


    }
}