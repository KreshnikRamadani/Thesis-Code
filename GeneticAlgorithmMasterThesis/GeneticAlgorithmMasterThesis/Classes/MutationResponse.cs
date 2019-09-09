using GeneticAlgorithmMasterThesis.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class MutationResponse
    {
        public int mutationChange { get; set; }     // Sum of added and deleted genes in a generation
        public int parentIndex { get; set; }
        public Individ individ { get; set; }

        public MutationResponse()
        {
            individ = new Individ();
        }
    }
}