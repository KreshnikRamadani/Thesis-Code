using GeneticAlgorithmMasterThesis.Algorithm;
using GeneticAlgorithmMasterThesis.Classes;
using GeneticAlgorithmMasterThesis.DataSet;
using GeneticAlgorithmMasterThesis.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithmMasterThesis
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            // test();       
        }

        void test()
        {
            //  Satellite 1
            //Satellite satellite_1 = new Satellite();
            //satellite_1.latitude = 180000;
            //satellite_1.longitude = 8300;
            //satellite_1.velocity = -300;
            //satellite_1.W = 50;
            //satellite_1.D = 500;

            Satellite satellite_1 = new Satellite();
            satellite_1.latitude = 150857;
            satellite_1.longitude = 76067;
            satellite_1.velocity = 20;
            satellite_1.W = 20;
            satellite_1.D = 500;


            //int sat_1_Latitude = 180000;
            //int sat_1_Longitude = 8300;
            //int sat_1_velocity = -300;


            // Ne Turn 12  e ka fotografu Lokacionin 0
            //int location_latitude_0 = 175958;
            //int location_longitude_0 = 8387;

            int location_latitude_0 = 153346;
            int location_longitude_0 = 76229;


            // ---------------------------------------------


            Satellite satellite_0 = new Satellite();
            satellite_0.latitude = 170000;
            satellite_0.longitude = 8300;
            satellite_0.velocity = 300;
            satellite_0.W = 50;
            satellite_0.D = 500;

            // Satellite 0
            //int sat_0_Latitude = 170000;
            //int sat_0_Longitude = 8300;
            //int sat_0_velocity = 300;
            //Offset sat_0_cameraOffset = new Offset();

            // Ne Turn 18 e ka fotografu Lokacionin 1

            //  Location 1
            int location_latitude_1 = 153632;
            int location_longitude_1 = 76360;

            bool flag = false;
            int yes = 0;

            List<Triple> triples = new List<Triple>();
            for (int t = 0; t <= 180000; t++)
            {
                Triple triple = new Triple();
                if (t == 0)
                {
                    triple.latitude = satellite_1.latitude;
                    triple.longitude = satellite_1.longitude;
                    triple.velocity = satellite_1.velocity;
                }
                else
                {
                    //triple = Functions.UpdateSatelliteLocation(satellite_1.latitude,
                    //                                           satellite_1.longitude,
                    //                                           satellite_1.velocity);
                }

                triples.Add(triple);
                satellite_1.latitude = triple.latitude;
                satellite_1.longitude = triple.longitude;
                satellite_1.velocity = triple.velocity;

                Offset cameraOffset = new Offset();
                cameraOffset.deltaLat = Math.Abs(satellite_1.latitude - location_latitude_0);
                cameraOffset.deltaLong = Math.Abs(satellite_1.longitude - location_longitude_0);

                if (canReach_(cameraOffset, satellite_1, t))
                {
                    yes = t;
                    satellite_1.cameraOffsets.deltaLat = cameraOffset.deltaLat;
                    satellite_1.cameraOffsets.deltaLong = cameraOffset.deltaLong;
                    satellite_1.cameraOffsets.turn = t;
                    break;
                }
            }


            List<Test> tests = new List<Test>();
            foreach (var satellite in triples)
            {
                Test test = new Test();
                test.deltaLat = Math.Abs(satellite.latitude - location_latitude_0);
                test.deltaLong = Math.Abs(satellite.longitude - location_longitude_0);
                tests.Add(test);
            }

            var temp = triples.ToList();
        }

        public bool canReach_(Offset cameraOffsets_t, Satellite satellite, int t)
        {
            double w_latitude = Math.Abs(cameraOffsets_t.deltaLat - satellite.cameraOffsets.deltaLat) / ((double)t - (double)satellite.cameraOffsets.turn);
            double w_longitude = Math.Abs(cameraOffsets_t.deltaLong - satellite.cameraOffsets.deltaLong) / ((double)t - (double)satellite.cameraOffsets.turn);
            return w_latitude < satellite.W && w_longitude < satellite.W;
        }



        Inputs inputs = new Inputs();
        Individ BestIndivid = null;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathFile.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            ClearMethod();
            if (txtPathFile.Text.Trim() != "")
            {
                int Counter = 0;
                var Content = File.ReadAllLines(txtPathFile.Text).ToList();
                inputs.duration = int.Parse(Content.ElementAt(Counter).ToString());
                Counter++;
                inputs.numberOfSatelites = int.Parse(Content.ElementAt(Counter).ToString());
                Counter++;

                // Read Collection of Satellites
                for (int i = Counter; i < inputs.numberOfSatelites + 2; i++)
                {
                    Satellite satellite = new Satellite();
                    satellite.Id = i - 2;
                    satellite.latitude = int.Parse(Content.ElementAt(i).Split(' ')[0]);
                    satellite.longitude = int.Parse(Content.ElementAt(i).Split(' ')[1]);
                    satellite.velocity = int.Parse(Content.ElementAt(i).Split(' ')[2]);
                    satellite.W = int.Parse(Content.ElementAt(i).Split(' ')[3]);
                    satellite.D = int.Parse(Content.ElementAt(i).Split(' ')[4]);
                    inputs.satellites.Add(satellite);
                    Counter++;
                }

                // Read Collection of Images
                inputs.numberOfCollections = int.Parse(Content.ElementAt(Counter));
                Counter++;

                int iStart = Counter;
                int jStart = 0;
                int CollectionOrdinal = 0;
                int ImageOrdinal = 0;
                int threshold = 0;
                int end = 0;

                for (int i = iStart; i < Content.Count; i++)
                {
                    Collection collection = new Collection();
                    collection.Id = CollectionOrdinal;
                    collection.V = int.Parse(Content.ElementAt(i).Split(' ')[0]);
                    collection.L = int.Parse(Content.ElementAt(i).Split(' ')[1]);
                    collection.R = int.Parse(Content.ElementAt(i).Split(' ')[2]);

                    threshold = i + collection.L;
                    end = i + collection.L + collection.R;
                    jStart = i + 1;

                    // Helps for preprocessing
                    double sumLatitude = 0;
                    double sumLongitude = 0;

                    for (int j = jStart; j <= end; j++)
                    {
                        if (j <= threshold)
                        {
                            // Location to capture    
                            Location location = new Location();
                            location.Id = ImageOrdinal;
                            location.CollectionID = CollectionOrdinal;
                            location.Latitude = int.Parse(Content.ElementAt(j).Split(' ')[0]);
                            location.Longitude = int.Parse(Content.ElementAt(j).Split(' ')[1]);

                            inputs.locations.Add(location);

                            collection.Locations.Add(location);

                            ImageOrdinal++;

                            // Preprocess
                            sumLatitude += location.Latitude;
                            sumLongitude += location.Longitude;
                        }
                        else
                        {
                            // Time interval -> when to capture
                            TimeRange timeRange = new TimeRange();
                            timeRange.from = int.Parse(Content.ElementAt(j).Split(' ')[0]);
                            timeRange.to = int.Parse(Content.ElementAt(j).Split(' ')[1]);
                            //timeRange.Length = timeRange.to - timeRange.from;
                            collection.TimeRanges.Add(timeRange);
                        }
                        i = end;
                    }

                    collection.avgLatitude = sumLatitude / collection.L;
                    collection.avgLongitude = sumLongitude / collection.L;
                    collection.quadrant = Functions.CoordinateQadrant((int)collection.avgLatitude,
                                                                     (int)collection.avgLongitude);

                   // collection.section = Functions.assignSection((int)collection.avgLatitude);

                    inputs.collections.Add(collection);
                    CollectionOrdinal++;

                    sumLongitude = 0;
                    sumLatitude = 0;
                }

                txtSimulation.Text = inputs.duration.ToString() + " sec";
                txtNumOfSat.Text = inputs.numberOfSatelites.ToString();
                txtNumOfColl.Text = inputs.numberOfCollections.ToString();
                txtNumLocs.Text = inputs.locations.Count.ToString();
            }
            else
            {
                MessageBox.Show("Load the file to read then, please.", "Informim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            inputs.AvailableCollectionsValues = inputs.collections.OrderByDescending(x => x.V).Select(x => x.V).Distinct().ToList();
            // inputs.calculatePreprocessing();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMethod();
        }

        public void ClearMethod()
        {
            txtNumOfColl.Clear();
            txtNumOfSat.Clear();
            txtSimulation.Clear();
            txtNumLocs.Clear();
            inputs = new Inputs();
        }


        private void btnRunGeneticAlgorithm_Click(object sender, EventArgs e)
        {
           
            // Initialize GA
            GeneticAlgorithm ga = new GeneticAlgorithm((int)numPopulationSize.Value,
                                                       (int)numTournamentSize.Value,
                                                       (int)numMaxGenerations.Value,
                                                       (int)numTerminationThreshold.Value,
                                                       Path.GetFileName(txtPathFile.Text).Split('.')[0],
                                                       chbExternalInitialize.Checked);

           BestIndivid = ga.RunGeneticAlgorithm(inputs);
            

            // Check Validation of the Best Solution
            if (!Validation.checkIfLocationIsInTimeRange(BestIndivid, inputs))
                MessageBox.Show("The solution is not valid! \n Problem with time range!");

            if (!Validation.checkCameraOffsetForConsecutivePictures(BestIndivid, inputs))    // This is false..., Why?
                MessageBox.Show("The solution is not valid! \n Camera offset is greater than w arcseconds between taking two consecutive pictures.");

            if (!Validation.checkIfOneLocationIsCapturedOnce(BestIndivid))
                MessageBox.Show("The solution is not valid! \n Location has been captured twice! ");

            if (!Validation.checkIfAtMostOneLocationIsCapturedPerTurn(BestIndivid))
            {
                MessageBox.Show("The solution is not valid! \n More than one location has been captured in a turn by the same satellite! ");
            }
            ExtractSubmissionIntoFile();
            MessageBox.Show("Finished! \n Score: " + BestIndivid.Fitness);
        }
        
        private void btnGenerateSubmissionFile_Click(object sender, EventArgs e)
        {
            if (BestIndivid != null)
            {
                ExportSubmissionFile();
            }
            else
            {
                MessageBox.Show("There is no data to export!", "Informim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void ExportSubmissionFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Nese fajlli nuk ekziston - Krijo
                if (!File.Exists(saveFileDialog.FileName))
                {
                    File.Create(saveFileDialog.FileName).Dispose();
                }

                // Nese fajlli ekziston shkruaj
                if (File.Exists(saveFileDialog.FileName))
                {
                    using (TextWriter tw = new StreamWriter(saveFileDialog.FileName))
                    {
                        string Line = BestIndivid.LocationsCounter + "\n";
                        tw.WriteLine(Line);

                        for (int i = 0; i < BestIndivid.Chromosome.Length; i++)
                        {
                            foreach (var item in BestIndivid.Chromosome[i])
                            {
                                Location location = inputs.locations.FirstOrDefault(x => x.Id == item.LocationID);
                                Line = location.Latitude + " "
                                      + location.Longitude + " "
                                      + item.CameraPosition.turn + " "
                                      + i + " ";
                                tw.WriteLine(Line);
                            }
                        }
                        tw.Close();
                    }
                }
                MessageBox.Show("Submission File has been successfully generated!", "Informim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        void ExtractSubmissionIntoFile()
        {
            //string fileName = @"C:\Users\Kreshnik Ramadani\Desktop\Master thesis\Generated Results\"
            //                              + "sf_"
            //                              + Path.GetFileName(txtPathFile.Text).Split('.')[0]
            //                              + "_" + (int)numPopulationSize.Value
            //                              + "_" + numTournamentSize.Value
            //                              + "_" + (int)numGreedyRate.Value
            //                              + new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()
            //                              + ".txt";

            string pattern = "sf_" + Path.GetFileName(BestIndivid.Path);
            string fileName = BestIndivid.Path.Replace(Path.GetFileName(BestIndivid.Path), pattern);

            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(BestIndivid.LocationsCounter);
                    for (int i = 0; i < BestIndivid.Chromosome.Length; i++)
                    {
                        foreach (var item in BestIndivid.Chromosome[i])
                        {
                            if (BestIndivid.KeyValuePairs[item.CollectionID].L == BestIndivid.KeyValuePairs[item.CollectionID].Photographs.Count)
                            {
                                Location location = inputs.locations.FirstOrDefault(x => x.Id == item.LocationID);
                                sw.WriteLine(location.Latitude + " "
                                          + location.Longitude + " "
                                          + i + " "
                                          + item.CameraPosition.turn + " ");
                            }
                        }
                    }
                }

                MessageBox.Show("Submission File has been successfully generated!", "Informim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
        }



        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Users\Kreshnik Ramadani\Desktop\Master thesis\Initial Solutions Generated\forever_alone\";
            string[] fileArray = Directory.GetFiles(fileName);
        }




    }
}