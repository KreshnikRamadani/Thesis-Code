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
    public class Individ
    {
        public List<Gene>[] Chromosome { get; set; }
       // public BasicInfo[] CollectionsInfo { get; set; }     
        public int Fitness { get; set; }

        // Test
        public Dictionary<int,BasicInfo> KeyValuePairs { get; set; }  // Keys represent Collection ID captured     
        //public Dictionary<int,int> Loading { get; set; }

        // Helper properties
        public int CollectionsCounter { get; set; }
        public int LocationsCounter { get; set; }
        public double AverageCollectionWorth { get; set; }      // Average worth of completed Collections
                          
        
        public string Path { get; set; }    // For data export

        public Individ()
        {
        }

        /*
        public Individ(Inputs _inputs)
        {
            int a_counter = 0;
            int b_counter = 0;
            int c_counter = 0;

            InitializeChromosome(_inputs);
            List<Collection> collections = _inputs.collections.ToList();        // All collections
            List<Collection> quadrantCollections = new List<Collection>();     // Collections filtered by quadrant
            List<Collection> collections_t = new List<Collection>();
            List<int> satellites = _inputs.satellites.Select(x => x.Id).ToList();
            Offset OrientationVector_t = new Offset();
            int satLatitude = 0;
            int satLongitude = 0;
            int currentSatQuadrant = 0;
            int quadrant = 0;

            int currentSatSection = 0;
            int section = 0;

            int randomIndex = 0;
            int satCounter = _inputs.numberOfSatelites;
            while (satCounter > 0)
            {
                randomIndex = Functions.randomGenerator.Next(0, satellites.Count);
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


                    // Start Test
                    currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    currentSatSection = Functions.assignSection(satLatitude);
                    if (currentSatQuadrant != quadrant || currentSatSection != section)
                    {
                        quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant && x.section == currentSatSection).ToList();
                        quadrant = currentSatQuadrant;
                        section = currentSatSection;
                    }
                    // End Test


                    //currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    //if (currentSatQuadrant != quadrant)
                    //{
                    //    quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
                    //    quadrant = currentSatQuadrant;
                    //}

                    //// 3.   Consider satellite covering prerequisite - max orientation value of the satellites
                    //OrientationRange Lat_bounds = new OrientationRange();
                    //Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
                    //Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

                    //OrientationRange Long_bounds = new OrientationRange();
                    //Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
                    //Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

                    // 4.   Consider Collections included inside the satellite window

                    collections_t = quadrantCollections;

                    //collections_t = (from items in quadrantCollections
                    //                 where (Math.Pow(items.avgLatitude - satLatitude, 2) + Math.Pow(items.avgLongitude - satLongitude, 2)) < satellite.D
                    //                 select items).ToList();

                    //collections_t = quadrantCollections.Where(x => (x.avgLatitude <= Lat_bounds.MaxBound &&
                    //                                         x.avgLatitude >= Lat_bounds.MinBound) &&
                    //                                         (x.avgLongitude <= Long_bounds.MaxBound &&
                    //                                         x.avgLongitude >= Long_bounds.MinBound)).ToList();

                    if (collections_t.Count > 0)
                    {
                        a_counter++;
                        randomIndex = Functions.randomGenerator.Next(0, collections_t.Count);
                        var collection = collections_t[randomIndex];

                        // 4.   Consider Collection is in t
                        if (collection.isInTimeRange(turn))
                        {
                            b_counter++;

                            // This selection should to improve

                            randomIndex = Functions.randomGenerator.Next(0, collection.Locations.Count);
                            var location = collection.Locations[randomIndex];

                            OrientationVector_t.deltaLat = Math.Abs(satLatitude - location.Latitude);
                            OrientationVector_t.deltaLong = Math.Abs(satLongitude - location.Longitude);
                            OrientationVector_t.turn = turn;

                            // 6  Consider orientation of the satellite not exceed the value of D arcseconds.
                            Response res = satellite.canReach(OrientationVector_t);

                            // 7.  Check if location is reachable by camera
                            if (res.reach)
                            {
                                c_counter++;

                                // 8.   Save this shoot
                                AddGene(OrientationVector_t, satellite.Id, satLatitude, satLongitude, collection, location, res,null);

                                // Update position of Camera in Satellite
                                satellite.cameraOffsets.deltaLat = OrientationVector_t.deltaLat;
                                satellite.cameraOffsets.deltaLong = OrientationVector_t.deltaLong;
                                satellite.cameraOffsets.turn = OrientationVector_t.turn;

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

                satellites.Remove(satId);
                satCounter--;
            }

            // 9.   Estimate Fitness
            EstimateFitness();
        }
        */


        public Individ(Inputs _inputs, int x)
        {
            InitializeChromosome(_inputs);
        }

        public Individ(Inputs _inputs)
        {

            InitializeChromosome(_inputs);

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
                randomIndex = Functions.randomGenerator.Next(0, satellites.Count);
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

                    currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
                    if (currentSatQuadrant != quadrant)
                    {
                        quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
                        quadrant = currentSatQuadrant;
                    }

                    // 3.   Consider satellite covering prerequisite - max orientation value of the satellites
                    OrientationRange Lat_bounds = new OrientationRange();
                    Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
                    Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

                    OrientationRange Long_bounds = new OrientationRange();
                    Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
                    Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

                    // collections_t = quadrantCollections;

                    // 4.   Consider Collections included inside the satellite window
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
                        randomIndex = Functions.randomGenerator.Next(0, collections_t.Count);
                        var collection = collections_t[randomIndex];

                        // 4.   Consider Collection is in t
                        if (collection.isInTimeRange(turn))
                        {
                            // This selection should to improve
                            randomIndex = Functions.randomGenerator.Next(0, collection.Locations.Count);
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
                                AddGene(OrientationVector_t, satellite.Id, satLatitude, satLongitude, collection.Id, location.Id);

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
            EstimateFitness();
        }





        public void AddGene(Offset _cameraOffset, int satId, int satLat, int satLong, int collectionId, int locationId)
        {
            // Take a shoot
            Gene gene = new Gene
            {
                satId = satId,
                satLatitude = satLat,
                satLongitude = satLong,
                Turn = _cameraOffset.turn,

                CameraPosition = new Offset
                {
                    deltaLat = _cameraOffset.deltaLat,
                    deltaLong = _cameraOffset.deltaLong,
                    turn = _cameraOffset.turn,
                    //w_lat = _response.w_latitude,
                    //w_long = _response.w_longitude
                },

                LocationID = locationId,
                CollectionID = collectionId
            };

           
           Chromosome[satId].Add(gene);

           // CollectionsInfo[_location.CollectionID].Photographs.Add(_location.Id);

            KeyValuePairs[collectionId].Photographs.Add(locationId);

            if (!KeyValuePairs[collectionId].Participation.Contains(satId))
            {
               // CollectionsInfo[_location.CollectionID].Participation.Add(satId);
                KeyValuePairs[collectionId].Participation.Add(satId);
            }              

            
        }


        public void EstimateFitness()
        {
            Fitness = 0;
            CollectionsCounter = 0;
            LocationsCounter = 0;

            for (int i = 0; i < KeyValuePairs.Count; i++)
            {
                if (KeyValuePairs[i].L == KeyValuePairs[i].Photographs.Count)
                {
                    Fitness += KeyValuePairs[i].Worth;
                    CollectionsCounter += 1;
                    LocationsCounter += KeyValuePairs[i].Photographs.Count;
                    //foreach (var satId in CollectionsInfo[i].Participation)
                    //{
                    //    Loading[satId] += 1;
                    //}
                }
            }

            // Calculate average worth of completed Collections
            if (CollectionsCounter > 0)
                AverageCollectionWorth = Fitness / CollectionsCounter;
            else
                AverageCollectionWorth = 0;
        }





        #region ShrinkGenome

        // satellite, offset, previousIndex, collId

        public Details InspectSatellite(Satellite satellite, Offset offset, int previousIndex, int collId)
        {
            Details details = new Details();
            bool previousFlag = false;
            bool nextFlag = false;

            int secondPreviousIndex = previousIndex - 1;
            int nextIndex = previousIndex + 1;
            int secondNextIndex = previousIndex + 2;

            int CollId = -1;
            //int CollValue = 0;

            // P R E V I O U S
            details.previousLocus = previousIndex;
            if (previousIndex > -1)
            {
                previousFlag = checkOffset(previousIndex, satellite, offset);
                //Response res = resCheckOffset(previousIndex, satellite, offset);
                //previousFlag = res.reach;
                if (previousFlag)
                {
                    details.isPrevious = true;
                    //details.z_prev_w_Latitude = res.w_latitude;
                    //details.z_prev_w_Longitude = res.w_longitude;
                }
                else
                {
                    // S E C O N D  P R E V I O U S
                    details.isPrevious = false;
                    details.secondPreviousLocus = secondPreviousIndex;

                     var gene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ToList()[previousIndex];
                    /*
                     *Kjo i bjen qe gjeni me index previousIndex duhet te fshihet nese plotesohet kushti per secondPreviousIndex, 
                     * e qe i bjen fshij lokacionin, per me shtu nje tjeter lokacion te te njejtit koleksionin.
                     * Ky kusht e ndalon kete rast.
                     */
                    if (gene.CollectionID != collId)
                    {

                        if (secondPreviousIndex > -1)
                        {
                            previousFlag = checkOffset(secondPreviousIndex, satellite, offset);
                            //res = resCheckOffset(secondPreviousIndex, satellite, offset);
                            //previousFlag = res.reach;
                            if (previousFlag)
                            {
                                details.isSecondPrevious = true;
                                //details.z_prev_w_Latitude = res.w_latitude;
                                //details.z_prev_w_Longitude = res.w_longitude;


                                // START Assign the Result Impact
                                var prevGene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ElementAt(previousIndex);
                                if (KeyValuePairs[prevGene.CollectionID].L == KeyValuePairs[prevGene.CollectionID].Photographs.Count)
                                {
                                    CollId = prevGene.CollectionID;
                                    details.ResultImpact += KeyValuePairs[prevGene.CollectionID].Worth;
                                }
                                // END
                            }
                            else
                            {
                                details.isSecondPrevious = false;
                            }
                        }
                        else
                        {
                            previousFlag = true;
                            details.isPrevious = false;
                            details.isSecondPrevious = true;

                            // START Assign the Result Impact
                            var prevGene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ElementAt(previousIndex);
                            if (KeyValuePairs[prevGene.CollectionID].L == KeyValuePairs[prevGene.CollectionID].Photographs.Count)
                            {
                                CollId = prevGene.CollectionID;
                                details.ResultImpact += KeyValuePairs[prevGene.CollectionID].Worth;
                            }
                            // END
                        }
                    }
                   






                }
            }
            else
            {
                previousFlag = true;
                details.isPrevious = true;
                details.isSecondPrevious = false;
            }

            // N E X T
            if (previousFlag)
            {
                details.nextLocus = nextIndex;
                if (nextIndex < Chromosome[satellite.Id].Count)
                {
                    nextFlag = checkOffset(nextIndex, satellite, offset);
                    //Response res = resCheckOffset(nextIndex, satellite, offset);
                    //nextFlag = res.reach;
                    if (nextFlag)
                    {
                        details.isNext = true;

                        //details.z_next_w_Latitude = res.w_latitude;
                        //details.z_next_w_Longitude = res.w_longitude;

                    }
                    else
                    {
                        // S E C O N D  N E X T

                        details.isNext = false;
                        details.secondNextLocus = secondNextIndex;

                        var gene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ToList()[nextIndex];
                        /*
                          *Kjo i bjen qe gjeni me index nextIndex duhet te fshihet nese plotesohet kushti per secondNextIndex, 
                          * e qe i bjen fshij lokacionin, per me shtu nje tjeter lokacion te te njejtit koleksionin.
                          * Ky kusht e ndalon kete rast.
                        */
                        if (gene.CollectionID != collId)
                        {
                            if (secondNextIndex < Chromosome[satellite.Id].Count)
                            {
                                nextFlag = checkOffset(secondNextIndex, satellite, offset);
                                //res = resCheckOffset(secondNextIndex, satellite, offset);
                                //nextFlag = res.reach;

                                if (nextFlag)
                                {
                                    details.isSecondNext = true;
                                    //details.z_next_w_Latitude = res.w_latitude;
                                    //details.z_next_w_Longitude = res.w_longitude;

                                    // START Assign the Result Impact
                                    var nextGene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ElementAt(nextIndex);
                                    if (KeyValuePairs[nextGene.CollectionID].L == KeyValuePairs[nextGene.CollectionID].Photographs.Count)
                                    {
                                        if (CollId != nextGene.CollectionID)
                                            details.ResultImpact += KeyValuePairs[nextGene.CollectionID].Worth;
                                    }
                                    // END
                                }
                                else
                                {
                                    details.isSecondNext = false;
                                }
                            }
                            else
                            {
                                nextFlag = true;
                                details.isNext = false;
                                details.isSecondNext = true;

                                // START Assign the Result Impact
                                var nextGene = Chromosome[satellite.Id].OrderBy(x => x.Turn).ElementAt(nextIndex);
                                if (KeyValuePairs[nextGene.CollectionID].L == KeyValuePairs[nextGene.CollectionID].Photographs.Count)
                                {
                                    if (CollId != nextGene.CollectionID)
                                        details.ResultImpact += KeyValuePairs[nextGene.CollectionID].Worth;
                                }
                                // END
                            }
                        }
                        

                    }
                }
                else
                {
                    nextFlag = true;
                    details.isNext = true;
                    details.isSecondNext = false;
                }
            }

            details.Result = previousFlag && nextFlag;
            return details;
        }

        #endregion







        public Response resCheckOffset(int index, Satellite _satellite, Offset _cameraOffset)
        {
            Response response = new Response();
            var gene = Chromosome[_satellite.Id].OrderBy(x => x.Turn).ToList()[index];
            double w_latitude = Math.Abs(_cameraOffset.deltaLat - gene.CameraPosition.deltaLat) / Math.Abs((double)_cameraOffset.turn - (double)gene.CameraPosition.turn);
            double w_longitude = Math.Abs(_cameraOffset.deltaLong - gene.CameraPosition.deltaLong) / Math.Abs((double)_cameraOffset.turn - (double)gene.CameraPosition.turn);
            response.reach = w_latitude <= (double)_satellite.W && w_longitude <= (double)_satellite.W;
            if (response.reach)
            {
                response.w_latitude = w_latitude;
                response.w_longitude = w_longitude;
            }

            // return w_latitude <= (double)_satellite.W && w_longitude <= (double)_satellite.W;
            return response;
        }


        public bool checkOffset(int index, Satellite _satellite, Offset _cameraOffset)
        {
            var gene = Chromosome[_satellite.Id].OrderBy(x => x.Turn).ToList()[index];
            double w_latitude = Math.Abs(_cameraOffset.deltaLat - gene.CameraPosition.deltaLat) / Math.Abs((double)_cameraOffset.turn - (double)gene.CameraPosition.turn);
            double w_longitude = Math.Abs(_cameraOffset.deltaLong - gene.CameraPosition.deltaLong) / Math.Abs((double)_cameraOffset.turn - (double)gene.CameraPosition.turn);
            return w_latitude <= (double)_satellite.W && w_longitude <= (double)_satellite.W;
        }



        public void RemoveObstructiveGenes(int satId, Details _details)
        {
            // Template:   G1 - G2 - Gx - G3 - G4
            if (_details.isPrevious && _details.isSecondNext) // G2 - Gx - G4 (remove G3)
            {
                var oldGene = Chromosome[satId].OrderBy(x => x.Turn).ElementAt((int)_details.nextLocus);
                RemoveGene(satId, oldGene);
            }
            else if (_details.isSecondPrevious && _details.isNext)  // G1 - Gx - G3 (remove G2)
            {
                var oldGene = Chromosome[satId].OrderBy(x => x.Turn).ElementAt((int)_details.previousLocus);
                RemoveGene(satId, oldGene);
            }
            else if (_details.isSecondPrevious && _details.isSecondNext)    // G1 - Gx - G4 (remove G2,G3)
            {
                var oldPrevGene = Chromosome[satId].OrderBy(x => x.Turn).ElementAt((int)_details.previousLocus);
                var oldNextGene = Chromosome[satId].OrderBy(x => x.Turn).ElementAt((int)_details.nextLocus);
                // Delete Previous and Delete Next
                RemoveGene(satId, oldPrevGene);
                RemoveGene(satId, oldNextGene);
            }
        }






        public void RemoveGene(int satelliteId, Gene gene)
        {
            Chromosome[satelliteId].Remove(gene);
            //CollectionsInfo[gene.CollectionID].L += 1;
            KeyValuePairs[gene.CollectionID].Photographs.Remove(gene.LocationID);
            // _locationCounter -= 1;
        }






        //// Test test
        //public void SortGenes(int satelliteId)
        //{
        //    Chromosome[satelliteId] = Chromosome[satelliteId].OrderBy(o => o.Turn).ToList();
        //}






        #region Filter_CollectionsList
        
        public List<int> getBestIncompleteCollections(bool nearlyFilled)
        {
            if (nearlyFilled)
            {
                return KeyValuePairs.Where(x => x.Value.Worth >= AverageCollectionWorth &&
                                                  (double)x.Value.L / 2 < x.Value.Photographs.Count() &&
                                                  x.Value.L > x.Value.Photographs.Count())
                                                  .OrderByDescending(x => x.Value.Worth)
                                                  .ToDictionary(z => z.Key, y => y.Value).Keys.ToList();
            }
            else
            {
                return KeyValuePairs.Where(x => x.Value.Worth >= AverageCollectionWorth &&
                                  (double)x.Value.L / 2 > x.Value.Photographs.Count() &&
                                  x.Value.L > x.Value.Photographs.Count())
                                  .OrderByDescending(x => x.Value.Worth)
                                  .ToDictionary(z => z.Key, y => y.Value).Keys.ToList();
            }
        }
        
                     
        public List<int> getNewCollections(bool betterThanAvg)
        {
            if (betterThanAvg)
            {
                return KeyValuePairs.Where(x => x.Value.Worth >= AverageCollectionWorth &&
                                       x.Value.Photographs.Count == 0)
                                       .OrderByDescending(x => x.Value.Worth)
                                       .ToDictionary(z => z.Key, y => y.Value).Keys.ToList();
            }
            else
            {
                return KeyValuePairs.Where(x => x.Value.Worth < AverageCollectionWorth &&
                                      x.Value.Photographs.Count == 0)
                                      .OrderByDescending(x => x.Value.Worth)
                                      .ToDictionary(z => z.Key, y => y.Value).Keys.ToList();
            }
        }





        #endregion







        public Gene getPreviousGene(int satId, int turn)
        {
            return Chromosome[satId].OrderBy(x => x.Turn).Where(x => x.Turn < turn)
                .OrderBy(x => x.Turn)
                .LastOrDefault();
        }


        public Gene getNextGene(int satId, int turn)
        {
            return Chromosome[satId].OrderBy(x => x.Turn).Where(x => x.Turn > turn)
                .OrderBy(x => x.Turn)
                .FirstOrDefault();
        }


        public void InitializeChromosome(Inputs _inputs)
        {
            Chromosome = new List<Gene>[_inputs.numberOfSatelites];
           // CollectionsInfo = new BasicInfo[_inputs.numberOfCollections];
            KeyValuePairs = new Dictionary<int, BasicInfo>(_inputs.numberOfCollections);
            // Loading = new Dictionary<int, int>(_inputs.numberOfSatelites);

            for (int i = 0; i < (_inputs.numberOfSatelites + Math.Abs(_inputs.numberOfSatelites - _inputs.numberOfCollections)); i++)
            {
                if (i < _inputs.numberOfSatelites)
                {
                    Chromosome[i] = new List<Gene>();
                   // Loading[i] = 0;
                }
                if (i < _inputs.numberOfCollections)
                {
                    BasicInfo basicInfo = new BasicInfo();
                    basicInfo.Worth = _inputs.collections[i].V;
                    basicInfo.L = _inputs.collections[i].L;
                    basicInfo.Photographs = new List<int>();
                   // CollectionsInfo[i] = basicInfo;
                    KeyValuePairs.Add(i, basicInfo);
                }
            }
            Fitness = 0;
            AverageCollectionWorth = 0;

            // test
            foreach (var collection in _inputs.collections.OrderByDescending(x => x.V))
            {
                Guide guide = new Guide();
                guide.LocationFrom = collection.Locations.First().Id;
                guide.LocationTo = collection.Locations.Last().Id;
                guide.L = collection.L;
                guide.Worth = collection.V;
            }
        }
    }
}