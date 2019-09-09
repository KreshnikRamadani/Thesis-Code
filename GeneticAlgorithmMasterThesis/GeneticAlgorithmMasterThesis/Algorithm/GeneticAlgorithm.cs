using GeneticAlgorithmMasterThesis.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithmMasterThesis.Utils;
using GeneticAlgorithmMasterThesis.Classes;
using System.IO;
using Force.DeepCloner;

namespace GeneticAlgorithmMasterThesis.Algorithm
{
    public class GeneticAlgorithm : InitialIndivid
    {
        List<Results> IterationResults = new List<Results>();
        List<int> satS = new List<int>();

        private string datasetName;
        private int populationSize;
        // private double mutationRate;
        // private double crossoverRate;
        // private int elitismCount;
        protected int tournamentSize;      // Tournament selection
        protected int maxGeneration;
      //  private int greedyRate;            // Additional parameter
        private int terminationThreshold;  // Additional parameter

        private bool initFlag = true;   // Technical parameter

        public GeneticAlgorithm(int populationSize, int tournamentSize, int maxGeneration,int terminationThreshold, string datasetName, bool externalInit)
        {
            this.populationSize = populationSize;
            // this.mutationRate = mutationRate;
            // this.crossoverRate = crossoverRate;
            // this.elitismCount = elitismCount;
            this.tournamentSize = tournamentSize;
            this.maxGeneration = maxGeneration;
            // this.greedyRate = greedyRate;
            this.terminationThreshold = terminationThreshold;
            this.datasetName = datasetName;
            this.initFlag = externalInit;
        }
               
        public Individ RunGeneticAlgorithm(Inputs inputs)
        {
            // 2:  P <- {}
            Individ[] P = Enumerable.Repeat(new Individ(), populationSize).ToArray();
            Individ[] Q = Enumerable.Repeat(new Individ(), populationSize).ToArray();
            MutationResponse[] _Q = Enumerable.Repeat(new MutationResponse(), populationSize).ToArray();
            IterationResults = new List<Results>();

            var time1 = DateTime.Now;

            if (initFlag)
            {
                // External Initialization of Population
                InitializePopulationFromFiles(P, inputs);
            }
            else
            {
                // 4:   P < -P U { new random individual}
                for (int individualCount = 0; individualCount < populationSize; individualCount++)  // Loop over population size
                {
                    P[individualCount] = new Individ(inputs);  // Create individ and Add to population          .DeepClone()
                }
            }

            // Export Initial solution
            if (!initFlag)
            {
                Eksporto_Zgjidhjet_Iniciale(P, inputs); // Eksporto Zgjidhjet iniciale
            }
            
            // 5:   Best <- []
            Individ Best = P[0];
            int tempFitnessValue = 0;
            int counter = 0;

            // Keep track of current generation
            int generation = 0;
            do
            {
                Best = P[0];
                for (int i = 1; i < populationSize; i++)
                {
                    if (P[i].Fitness > Best.Fitness)
                        Best = P[i];
                }

                Results results = new Results();
                results.Gneration = generation;
                results.Fitness = Best.Fitness;
                results.SnapshotDate = DateTime.Now - time1;
                IterationResults.Add(results);

                // Keep track the counter for early termination
                if (Best.Fitness != tempFitnessValue)
                {
                    tempFitnessValue = Best.Fitness;
                    counter = 0;    // Reset the counter
                }
                else
                {
                    counter++;
                }

                for (int i = 0; i < populationSize; i += 2)
                {
                    Dictionary<int, Individ> parents = TournamentSelection2(P, 2);    // Select Parents

                    /**Crossover & Mutate
                     *
                     * 
                    // List<Individ> CrossoverResponse = Crossover(parents.ElementAt(0).Value, parents.ElementAt(1).Value, inputs);

                    // Crossover
                     List<Individ> CrossoverResponse = TwoPointCrossover(parents.ElementAt(0).Value, parents.ElementAt(1).Value, inputs);

                    // Mutate
                    MutationResponse child_A = Mutate(CrossoverResponse[0], inputs, generation);
                    child_A.parentIndex = parents.ElementAt(0).Key;

                    MutationResponse child_B = Mutate(CrossoverResponse[1], inputs, generation);
                    child_B.parentIndex = parents.ElementAt(1).Key;
                     * **/

                    // Only Mutate
                    MutationResponse child_A = Mutate(parents.ElementAt(0).Value, inputs, generation);
                    child_A.parentIndex = parents.ElementAt(0).Key;

                    MutationResponse child_B = Mutate(parents.ElementAt(1).Value, inputs, generation);
                    child_B.parentIndex = parents.ElementAt(1).Key;

                    _Q[i] = child_A;
                    _Q[i + 1] = child_B;
                }

                // Nderrimi i gjeneratave
                List<MutationResponse> offsprings = _Q.Where(x => x.mutationChange > 0).OrderByDescending(z => z.individ.Fitness).ToList();   // Filter only offstrings that have only one mutation change
                var parentsWithOffsprings = getMutatedIndividuals(P, offsprings);
                var indexes = offsprings.Select(z => z.parentIndex).Distinct().ToList();
                for (int i = 0; i < indexes.Count; i++)
                {
                    var tempResponses = offsprings.Where(x => x.parentIndex == indexes[i]).ToList();
                    var maxMutationChangeResponse = tempResponses.OrderByDescending(x => x.mutationChange).FirstOrDefault();
                    parentsWithOffsprings.Add(maxMutationChangeResponse.individ);
                }

                if (offsprings.Count > 0)
                {
                    Q = parentsWithOffsprings.ToArray();
                    P = (Individ[])Q.Clone();
                }

                // Check if termination threshold is achieved
                if (counter == terminationThreshold)
                    break;

                generation++;
            }
            while (generation < maxGeneration);

            var time2 = DateTime.Now;
            var diffTime = time2 - time1;

            // Save details per generation in text file.
            Best.Path = ExtractDetailsPerGenerationsIntoTextFile();
            
            return Best;
        }

        public List<Individ> getMutatedIndividuals(Individ[] parents, List<MutationResponse> children)
        {
            // List<Individ> returnList = parents.ToList();
            Dictionary<int, Individ> keyValuePairs = new Dictionary<int, Individ>();
            for (int i = 0; i < populationSize; i++)
            {
                keyValuePairs.Add(i, parents[i]);
            }

            for (int i = 0; i < children.Count; i++)    // Children's loop
            {
                for (int j = 0; j < populationSize; j++)    // Popullation's loop
                {
                    if (Math.Abs(children[i].individ.Fitness - parents[j].Fitness) < 10)
                    {
                        var temp = CalculateSimilarityIndividuals(children[i].individ, parents[j]);
                        var temp1 = CalculateSimilarityIndividuals(parents[j], children[i].individ);
                        if (temp > 0.95)
                        {
                            keyValuePairs.Remove(j);
                            break;
                        }
                    }
                }
            }
            return keyValuePairs.Values.ToList();
        }
               

        public double CalculateSimilarityIndividuals(Individ from, Individ to)
        {
            double res = 0;
            int a = 0;
            int b = 0;

            //for (int k = 0; k < from.CollectionsInfo.Count(); k++)
            //{
            //    a += from.CollectionsInfo[k].Photographs.Intersect(to.CollectionsInfo[k].Photographs).Count();
            //    b += from.CollectionsInfo[k].Photographs.Count;
            //}
            for (int i = 0; i < from.Chromosome.Length; i++)
            {
                a += from.Chromosome[i].Intersect(to.Chromosome[i]).Count();
                b += from.Chromosome[i].Count;
            }
            res = (double)a / (double)b;
            return res;
        }
         
        




        #region TournamentSelection
        /*
         * Tournament Selection 
         */
        private Individ TournamentSelection(Individ[] population) //, int? notThis
        {
            List<int> SelectedIndivids = selectKindividuals();
            int BestIndividIndex = SelectedIndivids[0];
            int CurrentIndivid = 0;
            double BestIndividQuality = population[BestIndividIndex].Fitness;
            for (int i = 1; i < tournamentSize; i++)
            {
                CurrentIndivid = SelectedIndivids[i];
                if (population[CurrentIndivid].Fitness > BestIndividQuality)
                {
                    BestIndividQuality = population[CurrentIndivid].Fitness;
                    BestIndividIndex = CurrentIndivid;
                }
            }
            // return new TournamentResponse { parentIndex = CurrentIndivid, individ = population[BestIndividIndex] };
            return population[BestIndividIndex];
        }




        private Dictionary<int, Individ> TournamentSelection2(Individ[] population, int selectionSize) //, int? notThis
        {
            Dictionary<int, Individ> keyValuePairs = new Dictionary<int, Individ>();
            for (int i = 0; i < populationSize; i++)
            {
                keyValuePairs.Add(i, population[i]);
            }

            for (int i = 0; i < populationSize - tournamentSize; i++)
            {
                int randomIndex = Functions.randomGenerator.Next(0, keyValuePairs.Count);
                keyValuePairs.Remove(randomIndex);
            }

            Dictionary<int, Individ> temps = keyValuePairs.OrderByDescending(u => u.Value.Fitness).Take(selectionSize).ToDictionary(z => z.Key, y => y.Value);

            return temps;
        }







        List<int> selectKindividuals()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < populationSize; i++)
            {
                result.Add(i);
            }

            int end = populationSize - tournamentSize;
            for (int i = 0; i < end; i++)
            {
                int randomIndex = Functions.randomGenerator.Next(0, result.Count);
                result.RemoveAt(randomIndex);
            }
            return result;
        }
        #endregion




        private List<Individ> DeepCrossover(Individ parent_one, Individ parent_two, Inputs inputs)
        {
            List<Individ> Result = new List<Individ>();
            Individ child1 = new Individ(inputs, 0);
            Individ child2 = new Individ(inputs, 0);
            List<Gene> temp1 = new List<Gene>();
            List<Gene> temp2 = new List<Gene>();
            //int startIndex = 0;
            //int endIndex = 0;

            int start_turn = Functions.randomGenerator.Next(0, inputs.duration / 2);
            int end_turn = Functions.randomGenerator.Next(inputs.duration / 2, inputs.duration);

            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                // Child 1
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn >= start_turn && x.Turn <= end_turn).OrderBy(x => x.Turn).ToList())
                {
                    child1.AddGene(gene.CameraPosition, i,
                                      gene.satLatitude,
                                      gene.satLongitude,
                                      gene.CollectionID,
                                      gene.LocationID);

                    temp1.Add(gene);
                }

                // Child 2
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn >= start_turn && x.Turn <= end_turn).OrderBy(x => x.Turn).ToList())
                {
                    child2.AddGene(gene.CameraPosition, i,
                             gene.satLatitude,
                             gene.satLongitude,
                             gene.CollectionID,
                             gene.LocationID);

                    temp2.Add(gene);
                }
            }


            // Right side 
            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                // Child 1
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn > end_turn).OrderBy(x => x.Turn).ToList())
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child1.AddGene(gene.CameraPosition, i,
                                   gene.satLatitude,
                                   gene.satLongitude,
                                   gene.CollectionID,
                                   gene.LocationID);

                        temp1.Add(gene);
                    }
                }

                // Child 2
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn > end_turn).OrderBy(x => x.Turn).ToList())
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child2.AddGene(gene.CameraPosition, i,
                                  gene.satLatitude,
                                  gene.satLongitude,
                                  gene.CollectionID,
                                  gene.LocationID);

                        temp2.Add(gene);
                    }
                }
            }

            // Left side 
            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                // Child 1
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn < start_turn).OrderBy(x => x.Turn).ToList())
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child1.AddGene(gene.CameraPosition, i,
                                   gene.satLatitude,
                                   gene.satLongitude,
                                   gene.CollectionID,
                                   gene.LocationID);

                        temp1.Add(gene);
                    }
                }

                // Child 2
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn < start_turn).OrderBy(x => x.Turn).ToList())
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child2.AddGene(gene.CameraPosition, i,
                                  gene.satLatitude,
                                  gene.satLongitude,
                                  gene.CollectionID,
                                  gene.LocationID);

                        temp2.Add(gene);
                    }
                }
            }

            child1.EstimateFitness();
            child2.EstimateFitness();
            Result.Add(child1);
            Result.Add(child2);
            return Result;
        }
      
        private List<Individ> Crossover(Individ parent_one, Individ parent_two, Inputs inputs)
        {
            List<Individ> Result = new List<Individ>();
            Individ child1 = new Individ(inputs, 0);
            Individ child2 = new Individ(inputs, 0);
            List<Gene> temp1 = new List<Gene>();
            List<Gene> temp2 = new List<Gene>();
            int startIndex = Functions.randomGenerator.Next(0, parent_one.Chromosome.Length - 3);
            int endIndex = Functions.randomGenerator.Next(startIndex + 1, parent_one.Chromosome.Length);

            // Portion of consecutive genes
            for (int i = startIndex; i <= endIndex; i++)
            {
                // Child 1
                foreach (var gene in parent_one.Chromosome[i])
                {
                    child1.AddGene(gene.CameraPosition, i,
                                   gene.satLatitude,
                                   gene.satLongitude,
                                   gene.CollectionID,
                                   gene.LocationID);

                    temp1.Add(gene);
                }

                // Child 2
                foreach (var gene in parent_two.Chromosome[i])
                {
                    child2.AddGene(gene.CameraPosition, i,
                                   gene.satLatitude,
                                   gene.satLongitude,
                                   gene.CollectionID,
                                   gene.LocationID);

                    temp2.Add(gene);
                }
            }



            // Right side 
            for (int i = endIndex + 1; i < parent_one.Chromosome.Length; i++)
            {
                // Child 1
                foreach (var gene in parent_two.Chromosome[i])
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child1.AddGene(gene.CameraPosition, i,
                                   gene.satLatitude,
                                   gene.satLongitude,
                                   gene.CollectionID,
                                   gene.LocationID);

                        temp1.Add(gene);
                    }
                }

                // Child 2
                foreach (var gene in parent_one.Chromosome[i])
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child2.AddGene(gene.CameraPosition, i,
                                  gene.satLatitude,
                                  gene.satLongitude,
                                  gene.CollectionID,
                                  gene.LocationID);

                        temp2.Add(gene);
                    }
                }
            }

            // Left side 
            for (int i = 0; i < startIndex; i++)
            {
                // Child 1
                foreach (var gene in parent_two.Chromosome[i])
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {

                        child1.AddGene(gene.CameraPosition, i,
                        gene.satLatitude,
                        gene.satLongitude,
                        gene.CollectionID,
                        gene.LocationID);

                        temp1.Add(gene);
                    }
                }

                // Child 2
                foreach (var gene in parent_one.Chromosome[i])
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                    {
                        child2.AddGene(gene.CameraPosition, i,
                                  gene.satLatitude,
                                  gene.satLongitude,
                                  gene.CollectionID,
                                  gene.LocationID);

                        temp2.Add(gene);
                    }
                }
            }

            child1.EstimateFitness();
            child2.EstimateFitness();
            Result.Add(child1);
            Result.Add(child2);
            return Result;
        }


        // This is OK-ay
        private List<Individ> TwoPointCrossover(Individ parent_one, Individ parent_two, Inputs inputs)
        {
            List<Individ> Result = new List<Individ>();
            Individ child1 = new Individ(inputs, 0);
            Individ child2 = new Individ(inputs, 0);
            List<Gene> temp1 = new List<Gene>();
            List<Gene> temp2 = new List<Gene>();
            int k1 = Functions.randomGenerator.Next(0, parent_one.Chromosome.Length / 2);
            int k2 = Functions.randomGenerator.Next(k1 + 1, parent_one.Chromosome.Length);

            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                if (i <= k1)
                {
                    // Child 1
                    foreach (var gene in parent_one.Chromosome[i])
                    {
                        if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child1.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp1.Add(gene);
                        }
                    }

                    // Child 2
                    foreach (var gene in parent_two.Chromosome[i])
                    {
                        if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child2.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp2.Add(gene);
                        }
                    }
                }
                else if (i > k1 && i <= k2)
                {
                    // Child 1
                    foreach (var gene in parent_two.Chromosome[i])
                    {
                        if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child1.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp1.Add(gene);
                        }
                    }

                    // Child 2
                    foreach (var gene in parent_one.Chromosome[i])
                    {
                        if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child2.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp2.Add(gene);
                        }
                    }
                }
                else if (i > k2)
                {
                    // Child 1
                    foreach (var gene in parent_one.Chromosome[i])
                    {
                        if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child1.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp1.Add(gene);
                        }
                    }

                    // Child 2
                    foreach (var gene in parent_two.Chromosome[i])
                    {
                        if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID) == null)
                        {
                            child2.AddGene(gene.CameraPosition, i,
                                           gene.satLatitude,
                                           gene.satLongitude,
                                           gene.CollectionID,
                                           gene.LocationID);

                            temp2.Add(gene);
                        }
                    }
                }
            }

            child1.EstimateFitness();
            child2.EstimateFitness();
            Result.Add(child1);
            Result.Add(child2);
            return Result;
        }



        private bool addGeneticInformation(Individ _offspring,
                                              Satellite _satellite,
                                              Gene _previousGene,
                                              Gene _newGene,
                                              Gene _nextGene)
        {
            if (_previousGene != null && _nextGene != null) // Left Side Check
            {
                // Previous Check
                _satellite.cameraOffsets.deltaLat = _previousGene.CameraPosition.deltaLat;
                _satellite.cameraOffsets.deltaLong = _previousGene.CameraPosition.deltaLong;
                _satellite.cameraOffsets.turn = _previousGene.CameraPosition.turn;
                bool tempRes = _satellite.canReach(_newGene.CameraPosition).reach;

                // Next Check
                _satellite.cameraOffsets.deltaLat = _newGene.CameraPosition.deltaLat;
                _satellite.cameraOffsets.deltaLong = _newGene.CameraPosition.deltaLong;
                _satellite.cameraOffsets.turn = _newGene.CameraPosition.turn;

                if (tempRes && _satellite.canReach(_nextGene.CameraPosition).reach)
                {
                    _offspring.AddGene(_newGene.CameraPosition,
                                       _satellite.Id,
                                       _newGene.satLatitude,
                                       _newGene.satLongitude,
                                       _newGene.CollectionID,
                                       _newGene.LocationID);
                    return true;
                }
            }
            else if (_previousGene != null && _nextGene == null)  // Right Side Check
            {
                _satellite.cameraOffsets.deltaLat = _previousGene.CameraPosition.deltaLat;
                _satellite.cameraOffsets.deltaLong = _previousGene.CameraPosition.deltaLong;
                _satellite.cameraOffsets.turn = _previousGene.CameraPosition.turn;

                if (_satellite.canReach(_newGene.CameraPosition).reach)
                {
                    _offspring.AddGene(_newGene.CameraPosition,
                                        _satellite.Id,
                                        _newGene.satLatitude,
                                        _newGene.satLongitude,
                                        _newGene.CollectionID,
                                        _newGene.LocationID);
                    return true;
                }
            }
            else if (_previousGene == null && _nextGene != null) // Left Side Check
            {
                // Next Check
                _satellite.cameraOffsets.deltaLat = _newGene.CameraPosition.deltaLat;
                _satellite.cameraOffsets.deltaLong = _newGene.CameraPosition.deltaLong;
                _satellite.cameraOffsets.turn = _newGene.CameraPosition.turn;
                if (_satellite.canReach(_nextGene.CameraPosition).reach)
                {
                    _offspring.AddGene(_newGene.CameraPosition,
                                       _satellite.Id,
                                       _newGene.satLatitude,
                                       _newGene.satLongitude,
                                       _newGene.CollectionID,
                                       _newGene.LocationID);
                    return true;
                }
            }
            else 
            {
                // Left Side or Right Side when the list is empty
                _offspring.AddGene(_newGene.CameraPosition,
                                    _satellite.Id,
                                    _newGene.satLatitude,
                                    _newGene.satLongitude,
                                    _newGene.CollectionID,
                                    _newGene.LocationID);
                return true;
            }
         
            return false;
        }


        private List<Individ> DeepOrderedCrossover(Individ parent_one, Individ parent_two, Inputs inputs)
        {
            List<Individ> Result = new List<Individ>();
            Individ child1 = new Individ(inputs, 0);
            Individ child2 = new Individ(inputs, 0);
            List<Gene> temp1 = new List<Gene>();
            List<Gene> temp2 = new List<Gene>();
            int k1 = 0;
            int k2 = 0;

            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                k1 = Functions.randomGenerator.Next(0, inputs.duration / 2);
                k2 = Functions.randomGenerator.Next(k1 + 1, inputs.duration);

                // Child 1
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn >= k1 && x.Turn <= k2).OrderBy(x => x.Turn))
                {
                    child1.AddGene(gene.CameraPosition, i, gene.satLatitude, gene.satLongitude, gene.CollectionID, gene.LocationID);
                    temp1.Add(gene);
                }

                // Child 2
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn >= k1 && x.Turn <= k2).OrderBy(x => x.Turn))
                {
                    child2.AddGene(gene.CameraPosition, i, gene.satLatitude, gene.satLongitude, gene.CollectionID, gene.LocationID);
                    temp2.Add(gene);
                }
            }


            // Right Side
            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                Satellite satellite = inputs.satellites[i];

                //Child 1
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn > k2).OrderBy(x => x.Turn))
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID || (x.Turn == gene.Turn && x.satId == i)) == null)
                    {
                        // Check if this is possible
                        Gene previousGene = child1.Chromosome[i].LastOrDefault();
                        if (addGeneticInformation(child1, satellite, previousGene, gene, null))
                            temp1.Add(gene);
                    }
                }

                satellite.cameraOffsets = new Offset();

                //Child 2
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn > k2).OrderBy(x => x.Turn))
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID || (x.Turn == gene.Turn && x.satId == i)) == null)
                    {
                        // Check if this is possible
                        Gene previousGene = child2.Chromosome[i].LastOrDefault();
                        if (addGeneticInformation(child2, satellite, previousGene, gene, null))
                            temp2.Add(gene);
                    }
                }
                satellite.cameraOffsets = new Offset();
            }


            // Left Side
            for (int i = 0; i < inputs.numberOfSatelites; i++)
            {
                Satellite satellite = inputs.satellites[i];

                //Child 1
                foreach (var gene in parent_two.Chromosome[i].Where(x => x.Turn < k1).OrderBy(x => x.Turn))
                {
                    if (temp1.FirstOrDefault(x => x.LocationID == gene.LocationID || (x.Turn == gene.Turn && x.satId == i)) == null)
                    {
                        // Check if this is possible
                        Gene previousGene = child1.getPreviousGene(satellite.Id, gene.Turn);
                        Gene nextGene = child1.getNextGene(satellite.Id, gene.Turn);

                        if (addGeneticInformation(child1, satellite, previousGene, gene, nextGene))
                            temp1.Add(gene);
                    }
                }

                satellite.cameraOffsets = new Offset();

                //Child 2
                foreach (var gene in parent_one.Chromosome[i].Where(x => x.Turn < k1).OrderBy(x => x.Turn))
                {
                    if (temp2.FirstOrDefault(x => x.LocationID == gene.LocationID || (x.Turn == gene.Turn && x.satId == i)) == null)
                    {
                        // Check if this is possible
                        Gene previousGene = child2.getPreviousGene(satellite.Id, gene.Turn);
                        Gene nextGene = child2.getNextGene(satellite.Id, gene.Turn);
                        if (addGeneticInformation(child2, satellite, previousGene, gene, nextGene))
                            temp2.Add(gene);
                    }
                }
                satellite.cameraOffsets = new Offset();
            }

            child1.EstimateFitness();
            child2.EstimateFitness();
            Result.Add(child1);
            Result.Add(child2);
            return Result;
        }

















        private MutationResponse Mutate(Individ individ, Inputs inputs, int generation)
        {
            // int lastBestValue = (int)(inputs.numberOfCollections * 0.2);  // Last best value of 10% from best collections.

            int mutationChange = 0;
            MutationResponse mutationResponse = new MutationResponse();

            Satellite satellite = new Satellite();
            int satelliteId = 0;
            int randomIndex = 0;
            int collectionId = 0;
            int satLatitude = 0;
            int satLongitude = 0;
            
            Offset cameraOffsets_t = new Offset();
            // int index = 0;
            Response previousRes = new Response();  // Vec sa per sy e faqe :)
            bool captured = false;

            List<int> candidateCollections = new List<int>();
            List<Details> detailsList = new List<Details>();
            Collection collection = new Collection();

            // Koleksionet > se AvgValue qe jane te mbushura me shume se 50%
            //var candidateCollections = individ.KeyValuePairs.Where(x => x.Value.Worth >= individ.AverageCollectionWorth &&
            //                                      (double)x.Value.L / 2 < x.Value.Photographs.Count() &&
            //                                      x.Value.L > x.Value.Photographs.Count())
            //                                      .OrderByDescending(x => x.Value.Worth)
            //                                      .ToDictionary(z => z.Key, y => y.Value).Keys.ToList();


            // Koleksionet > se AvgValue qe jane te mbushura me shume se 50%
            candidateCollections = individ.getBestIncompleteCollections(true);

            // Koleksionet > se AvgValue qe jane te mbushura me pak se 50%
            if (candidateCollections.Count == 0)
                candidateCollections = individ.getBestIncompleteCollections(false);



            // This is added for "forever_alone" dataset
            if (candidateCollections.Count == 0 && generation % 2 == 0)
                candidateCollections = individ.getNewCollections(true);
            else if (candidateCollections.Count == 0)
                candidateCollections = individ.getNewCollections(false);
            

            randomIndex = Functions.randomGenerator.Next(0, candidateCollections.Count);
            collectionId = candidateCollections[randomIndex];
            collection = inputs.collections.Single(x => x.Id == collectionId);

            var locations = collection.Locations.Select(x => x.Id)
                                .Except(individ.KeyValuePairs[collection.Id].Photographs).ToList();
            
            var participations = individ.KeyValuePairs[collection.Id].Participation.ToList();

            foreach (var locId in locations)
            {
                satellite = new Satellite();
                Location location = inputs.collections[collection.Id].Locations.Single(x => x.Id == locId);

                satellite = new Satellite();
                if (participations.Count > 0)
                {
                    randomIndex = Functions.randomGenerator.Next(0, participations.Count);
                    satelliteId = participations[randomIndex];
                    satellite = inputs.satellites.SingleOrDefault(x => x.Id == satelliteId);
                }
                else
                {
                    randomIndex = Functions.randomGenerator.Next(0, inputs.numberOfSatelites);
                    satellite = inputs.satellites[randomIndex];
                }

                foreach (var timeRange in collection.TimeRanges)
                {
                    for (int i = timeRange.from; i <= timeRange.to; i++)
                    {
                        Gene busyGene = individ.Chromosome[satellite.Id].SingleOrDefault(x => x.Turn == i);
                        if (busyGene != null)
                        {
                            if (busyGene.CollectionID != collection.Id &&
                                individ.KeyValuePairs[busyGene.CollectionID].Worth < individ.AverageCollectionWorth &&
                                individ.KeyValuePairs[busyGene.CollectionID].L >
                                individ.KeyValuePairs[busyGene.CollectionID].Photographs.Count)
                            {
                                individ.RemoveGene(satellite.Id, busyGene);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        satLatitude = satellite.CalculateLatitudePerTurn(i);
                        satLongitude = satellite.CalculateLongitudePerTurn(i);

                        if ((location.Latitude <= satLatitude + satellite.D &&
                        location.Latitude >= satLatitude - satellite.D + 1) &&
                        (location.Longitude <= satLongitude + satellite.D &&
                        location.Longitude >= satLongitude - satellite.D + 1))
                        {
                            cameraOffsets_t = new Offset();
                            cameraOffsets_t.deltaLat = Math.Abs(satLatitude - location.Latitude);
                            cameraOffsets_t.deltaLong = Math.Abs(satLongitude - location.Longitude);
                            cameraOffsets_t.turn = i;

                            Gene prevGene = individ.getPreviousGene(satellite.Id, i);
                            int prevIndex = individ.Chromosome[satellite.Id].OrderBy(x => x.Turn).ToList().IndexOf(prevGene);

                            Details details = individ.InspectSatellite(satellite, cameraOffsets_t, prevIndex, location.CollectionID);
                            if (details.Result && details.isPrevious && details.isNext)
                            {
                                // 1. Just ADD
                                individ.AddGene(cameraOffsets_t,
                                satellite.Id,
                                satLatitude,
                                satLongitude,
                                collection.Id,
                                location.Id);
                                captured = true;
                                mutationChange += 1;
                                break;
                            }
                            else if (details.Result && details.ResultImpact == 0)
                            {
                                //2. Add, Remove, No-Impact
                                individ.RemoveObstructiveGenes(satellite.Id, details);
                                individ.AddGene(cameraOffsets_t,
                                               satellite.Id,
                                               satLatitude,
                                               satLongitude,
                                               collection.Id,
                                               location.Id);
                                captured = true;
                                mutationChange += 1;
                                break;
                            }
                            else if (details.Result && !detailsList.Contains(details))
                            {
                                details.SatId = satellite.Id;
                                details.offset.deltaLat = cameraOffsets_t.deltaLat;
                                details.offset.deltaLong = cameraOffsets_t.deltaLong;
                                details.offset.turn = cameraOffsets_t.turn;
                                detailsList.Add(details);
                            }
                        }
                    }

                    if (captured)
                        break;
                }
                                                       

                if (detailsList.Count > 0 && !captured)
                {
                    // 3. Add, Remove, Impact 
                    // if (generation % greedyRate == 0)  // && ((double)individ.CollectionsInfo[collection.Id].L / 2) > locations.Count)  // This second condition should be tested
                    {
                        var detailsLowResultImpact = detailsList.Where(x => x.ResultImpact > 0)
                                                               .OrderBy(x => x.ResultImpact)
                                                               .FirstOrDefault();
                        if (detailsLowResultImpact != null)
                        {
                            // if (detailsLowResultImpact.ResultImpact < individ.AverageCollectionWorth)
                           //  if (detailsLowResultImpact.ResultImpact < collection.V)
                            if (2 * detailsLowResultImpact.ResultImpact < collection.V)
                            {

                                individ.RemoveObstructiveGenes(detailsLowResultImpact.SatId, detailsLowResultImpact);
                                satLatitude = satellite.CalculateLatitudePerTurn(detailsLowResultImpact.offset.turn);
                                satLongitude = satellite.CalculateLongitudePerTurn(detailsLowResultImpact.offset.turn);

                                individ.AddGene(detailsLowResultImpact.offset,
                                               detailsLowResultImpact.SatId,
                                               satLatitude,
                                               satLongitude,
                                               collection.Id,
                                               location.Id);
                                mutationChange += 1;
                            }
                        }
                    }
                }
                captured = false;
                detailsList.Clear();
            }

            // Estimate Fitness;
            individ.EstimateFitness();
            mutationResponse.individ = individ;
            mutationResponse.mutationChange = mutationChange;
            return mutationResponse;
        }


                                      
               
        private string ExtractDetailsPerGenerationsIntoTextFile()
        {

            string fileName = @"C:\Users\Kreshnik Ramadani\Desktop\Master thesis\Generated Results\"
                                          + datasetName
                                          + "_" + populationSize
                                          + "_" + tournamentSize
                                         // + "_" + greedyRate
                                          + "_" + new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()
                                          + ".txt";
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    foreach (var item in IterationResults)
                    {
                        sw.WriteLine(item.Gneration + "     " + item.Fitness + "     " + item.SnapshotDate);
                    }
                }

                //System.Windows.Forms.MessageBox.Show("Details into text file has been succesfuly extracted!",
                //    "Informim",
                //    System.Windows.Forms.MessageBoxButtons.OK,
                //    System.Windows.Forms.MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }

            return fileName;
        }
        
        #region Eksportimi_Zgjidhjeve_Iniciale

        private void Eksporto_Zgjidhjet_Iniciale(Individ[] Population, Inputs _inputs)
        {
            for (int i = 0; i < populationSize; i++)
            {
                bool one = Validation.checkIfLocationIsInTimeRange(Population[i], _inputs);
                bool two = Validation.checkCameraOffsetForConsecutivePictures(Population[i], _inputs);
                bool three = Validation.checkIfOneLocationIsCapturedOnce(Population[i]);
                bool four = Validation.checkIfAtMostOneLocationIsCapturedPerTurn(Population[i]);
                if (one && two && three && four)
                {
                    ExtractInitialSolutionIntoTextFile(Population[i]);
                }
            }
        }


        private void ExtractInitialSolutionIntoTextFile(Individ _individ)
        {

            string fileName = @"C:\Users\Kreshnik Ramadani\Desktop\Master thesis\Initial Solutions Generated\constellation\"
                                          + datasetName
                                          + "_" + _individ.Fitness
                                          + "_" + _individ.CollectionsCounter
                                          + "_" + _individ.LocationsCounter
                                          //  + "_" + new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()
                                          + ".txt";
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    for (int i = 0; i < _individ.Chromosome.Length; i++)
                    {
                        sw.WriteLine(_individ.Chromosome[i].Count);
                        foreach (var item in _individ.Chromosome[i])
                        {
                            sw.WriteLine(item.LocationID + " "
                                         + item.CollectionID + " "
                                         + item.Turn + " "
                                         + item.satLatitude + " "
                                         + item.satLongitude + " "
                                         );

                            sw.WriteLine(item.CameraPosition.deltaLat + " "
                                         + item.CameraPosition.deltaLong + " "
                                         + item.CameraPosition.turn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
        }

        #endregion
        
        public void InitializePopulationFromFiles(Individ[] Population, Inputs _inputs)
        {
            string fileName = @"C:\Users\Kreshnik Ramadani\Desktop\Master thesis\Initial Solutions Generated\constellation\";  //constellation , forever_alone
            string[] fileArray = Directory.GetFiles(fileName);
            var temp = fileArray.ToList();  // .Where(x=>x.Contains("constellation_205")).ToList();
            int randomIndex = 0;
            Individ individ = new Individ();
            for (int i = 0; i < populationSize; i++)
            {
                randomIndex = Functions.randomGenerator.Next(0, temp.Count);
                var randomFileIndivid = temp[randomIndex];
                var fileContent = File.ReadAllLines(randomFileIndivid).ToList();

                // LocationID, CollectionID, Turn, satLatitude, satLongitude 
                // CameraPosition.deltaLat, CameraPosition.deltaLong, CameraPosition.turn

                int counter = -1;
                individ = new Individ(_inputs, 0);

                for (int j = 0; j < fileContent.Count; j++)
                {
                    var itemSplit = fileContent[j].Trim().Split(' ').ToList();
                    if (itemSplit.Count == 1)
                    {
                        counter++;
                    }
                    else if (itemSplit.Count == 5)
                    {
                        Gene gene = new Gene();
                        gene.LocationID = int.Parse(itemSplit[0]);
                        gene.CollectionID = int.Parse(itemSplit[1]);
                        gene.Turn = int.Parse(itemSplit[2]);
                        gene.satLatitude = int.Parse(itemSplit[3]);
                        gene.satLongitude = int.Parse(itemSplit[4]);

                        itemSplit = fileContent[j + 1].Trim().Split(' ').ToList();
                        gene.CameraPosition.deltaLat = int.Parse(itemSplit[0]);
                        gene.CameraPosition.deltaLong = int.Parse(itemSplit[1]);
                        gene.CameraPosition.turn = int.Parse(itemSplit[2]);
                        gene.satId = counter;
                        individ.Chromosome[counter].Add(gene);
                        individ.KeyValuePairs[gene.CollectionID].Photographs.Add(gene.LocationID);

                        if (!individ.KeyValuePairs[gene.CollectionID].Participation.Contains(counter))
                            individ.KeyValuePairs[gene.CollectionID].Participation.Add(counter);
                    }
                }

                temp.Remove(randomFileIndivid);

                //Estimate Fitness
                individ.EstimateFitness();

                bool one = Validation.checkIfLocationIsInTimeRange(individ, _inputs);
                bool two = Validation.checkCameraOffsetForConsecutivePictures(individ, _inputs);
                bool three = Validation.checkIfOneLocationIsCapturedOnce(individ);
                bool four = Validation.checkIfAtMostOneLocationIsCapturedPerTurn(individ);
                if (one && two && three && four)
                {
                    Population[i] = individ;
                }
                else
                {
                    int x = 0;
                }
            }

        }
    }
}