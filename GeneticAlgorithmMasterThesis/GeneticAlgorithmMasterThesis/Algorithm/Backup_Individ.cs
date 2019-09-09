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
    class Backup_Individ
    {
        //public List<Gene>[] Chromosome { get; set; }
        //public Dictionary<int, Guide> Information { get; set; }
        //public BasicInfo[] CollectionsInfo { get; set; }  // Position represent Collection ID captured        
        //public int Fitness { get; set; }

        //// Test - during development process
        //public int _locationCounter { get; set; }
        //public int _collectionCounter { get; set; }


        //public Backup_Individ()
        //{
        //}

        //public Backup_Individ(Inputs _inputs)
        //{
        //    InitializeChromosome(_inputs);
        //    List<Collection> collections = _inputs.collections.ToList();        // All collections
        //    List<Collection> quadrantCollections = new List<Collection>();     // Collections filtered by quadrant
        //    List<Collection> collections_t = new List<Collection>();
        //    List<int> satellites = _inputs.satellites.Select(x => x.Id).ToList();
        //    Offset OrientationVector_t = new Offset();
        //    int satLatitude = 0;
        //    int satLongitude = 0;
        //    int currentSatQuadrant = 0;
        //    int quadrant = 0;
        //    int randomIndex = 0;
        //    int satCounter = _inputs.numberOfSatelites;
        //    while (satCounter > 0)
        //    {
        //        randomIndex = Functions.randomGenerator.Next(0, satellites.Count);
        //        int satId = satellites[randomIndex];
        //        Satellite satellite = _inputs.satellites[satId];

        //        for (int turn = 0; turn < _inputs.duration; turn++)
        //        {
        //            // 1.   Satellite is moving - Update satellite location
        //            satLatitude = satellite.CalculateLatitudePerTurn(turn);
        //            satLongitude = satellite.CalculateLongitudePerTurn(turn);

        //            // 2.   The satellites won’t be required to take an image of any location above latitude 85° (north or south).
        //            if (Math.Abs(satLatitude) > Constants.EightyFiveDegree)
        //                continue;

        //            currentSatQuadrant = Functions.CoordinateQadrant(satLatitude, satLongitude);
        //            if (currentSatQuadrant != quadrant)
        //            {
        //                quadrantCollections = collections.Where(x => x.quadrant == currentSatQuadrant).ToList();
        //                quadrant = currentSatQuadrant;
        //            }

        //            // 3.   Consider satellite covering prerequisite - max orientation value of the satellites
        //            OrientationRange Lat_bounds = new OrientationRange();
        //            Lat_bounds.MinBound = satLatitude - satellite.D + 1;   // Latitude
        //            Lat_bounds.MaxBound = satLatitude + satellite.D;       // Latitude

        //            OrientationRange Long_bounds = new OrientationRange();
        //            Long_bounds.MinBound = satLongitude - satellite.D + 1;  // Longitude
        //            Long_bounds.MaxBound = satLongitude + satellite.D;      // Longitude

        //            // 4.   Consider Collections included inside the satellite window
        //            // collections_t = quadrantCollections;

        //            collections_t = quadrantCollections.Where(x => (x.avgLatitude <= Lat_bounds.MaxBound &&
        //                                                     x.avgLatitude >= Lat_bounds.MinBound) &&
        //                                                     (x.avgLongitude <= Long_bounds.MaxBound &&
        //                                                     x.avgLongitude >= Long_bounds.MinBound)).ToList();

        //            if (collections_t.Count > 0)
        //            {
        //                randomIndex = Functions.randomGenerator.Next(0, collections_t.Count);
        //                var collection = collections_t[randomIndex];

        //                // 4.   Consider Collection is in t
        //                if (collection.isInTimeRange(turn))
        //                {
        //                    randomIndex = Functions.randomGenerator.Next(0, collection.Locations.Count);
        //                    var location = collection.Locations[randomIndex];

        //                    OrientationVector_t.deltaLat = Math.Abs(satLatitude - location.Coordinates.Latitude);
        //                    OrientationVector_t.deltaLong = Math.Abs(satLongitude - location.Coordinates.Longitude);
        //                    OrientationVector_t.turn = turn;

        //                    // 6  Consider orientation of the satellite not exceed the value of D arcseconds.
        //                    // if (OrientationVector_t.deltaLat <= satellite.D && OrientationVector_t.deltaLong <= satellite.D)
        //                    {
        //                        Response res = satellite.canReach(OrientationVector_t);
        //                        // 7.  Check if location is reachable by camera
        //                        if (res.reach)
        //                        {
        //                            // 8.   Save this shoot
        //                            AddGene(OrientationVector_t, satellite.Id, satLatitude, satLongitude, collection, location, res);

        //                            //Gene gene = new Gene
        //                            //{
        //                            //    CameraPosition = new Offset
        //                            //    {
        //                            //        deltaLat = OrientationVector_t.deltaLat,
        //                            //        deltaLong = OrientationVector_t.deltaLong,
        //                            //        turn = turn
        //                            //    },
        //                            //    SatelliteCoordinates = new Coordinates
        //                            //    {
        //                            //        Latitude = satellite.latitude,
        //                            //        Longitude = satellite.longitude,
        //                            //    },

        //                            //    LocationID = location.Id
        //                            //};

        //                            //Chromosome[satellite.Id].Add(gene);
        //                            //CollectionsInfo[location.CollectionID].L -= 1;
        //                            //CollectionsInfo[location.CollectionID].Photographs.Add(location.Id);
        //                            //test_captured_locations_No += 1;

        //                            // Update position of Camera in Satellite
        //                            satellite.cameraOffsets.deltaLat = OrientationVector_t.deltaLat;
        //                            satellite.cameraOffsets.deltaLong = OrientationVector_t.deltaLong;
        //                            satellite.cameraOffsets.turn = OrientationVector_t.turn;

        //                            int indx = collections.IndexOf(collection);
        //                            collections[indx].Locations.Remove(location);
        //                            if (collections[indx].Locations.Count == 0)
        //                                collections.RemoveAt(indx);

        //                            indx = quadrantCollections.IndexOf(collection);
        //                            quadrantCollections[indx].Locations.Remove(location);
        //                            if (quadrantCollections[indx].Locations.Count == 0)
        //                                quadrantCollections.RemoveAt(indx);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        satellites.Remove(satId);
        //        satCounter--;
        //    }



        //    // 9.   Estimate Fitness
        //    EstimateFitness();
        //    //for (int i = 0; i < CollectionsInfo.Length; i++)
        //    //{
        //    //    if (CollectionsInfo[i].L == 0)
        //    //    {
        //    //        Fitness += CollectionsInfo[i].WorthCollection;
        //    //        test_captured_collections_No += 1;
        //    //    }
        //    //}
        //}



        //public void AddGene(Offset _cameraOffset, int satId, int satLat, int satLong, Collection collection, Location _location, Response _response)
        //{
        //    // Take a shoot
        //    Gene gene = new Gene
        //    {
        //        CameraPosition = new Offset
        //        {
        //            deltaLat = _cameraOffset.deltaLat,
        //            deltaLong = _cameraOffset.deltaLong,
        //            turn = _cameraOffset.turn,
        //            w_lat = _response.w_latitude,
        //            w_long = _response.w_longitude
        //        },
        //        SatelliteCoordinates = new Coordinates
        //        {
        //            Latitude = satLat,
        //            Longitude = satLong,
        //        },

        //        LocationID = _location.Id,
        //        CollectionID = collection.Id,
        //        GeneWeight = collection.V
        //    };

        //    Chromosome[satId].Add(gene);
        //    CollectionsInfo[_location.CollectionID].L -= 1;
        //    CollectionsInfo[_location.CollectionID].Photographs.Add(_location.Id);

        //    Information[_location.CollectionID].L -= 1;
        //    Information[_location.CollectionID].Photographs.Add(_location.Id);

        //    _locationCounter += 1;
        //}


        //public void RemoveGene(int satelliteId, Gene gene)
        //{
        //    // var geneToRemove = Chromosome[satelliteId].Single(x => x.LocationID == locationId);
        //    Chromosome[satelliteId].Remove(gene);
        //    CollectionsInfo[gene.CollectionID].L += 1;
        //    CollectionsInfo[gene.CollectionID].Photographs.Remove(gene.LocationID);
        //    _locationCounter -= 1;
        //}


        //public void EstimateFitness()
        //{
        //    Fitness = 0;
        //    _collectionCounter = 0;
        //    for (int i = 0; i < CollectionsInfo.Length; i++)
        //    {
        //        if (CollectionsInfo[i].L == 0)
        //        {
        //            Fitness += CollectionsInfo[i].WorthCollection;
        //            _collectionCounter += 1;
        //        }
        //    }
        //}


        //void InitializeChromosome(Inputs _inputs)
        //{
        //    Chromosome = new List<Gene>[_inputs.numberOfSatelites];
        //    CollectionsInfo = new BasicInfo[_inputs.numberOfCollections];
        //    Information = new Dictionary<int, Guide>(_inputs.numberOfCollections);

        //    for (int i = 0; i < (_inputs.numberOfSatelites + Math.Abs(_inputs.numberOfSatelites - _inputs.numberOfCollections)); i++)
        //    {
        //        if (i < _inputs.numberOfSatelites)
        //        {
        //            Chromosome[i] = new List<Gene>();
        //        }
        //        if (i < _inputs.numberOfCollections)
        //        {
        //            BasicInfo basicInfo = new BasicInfo();
        //            basicInfo.WorthCollection = _inputs.collections[i].V;
        //            basicInfo.L = _inputs.collections[i].L;
        //            basicInfo.Photographs = new List<int>();
        //            CollectionsInfo[i] = basicInfo;
        //        }
        //    }
        //    Fitness = 0;

        //    // test
        //    foreach (var collection in _inputs.collections.OrderByDescending(x => x.V))
        //    {
        //        Guide guide = new Guide();
        //        guide.LocationFrom = collection.Locations.First().Id;
        //        guide.LocationTo = collection.Locations.Last().Id;
        //        guide.L = collection.L;
        //        guide.Worth = collection.V;
        //        Information.Add(collection.Id, guide);
        //    }
        //}
    }
}
