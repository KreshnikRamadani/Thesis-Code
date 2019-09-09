using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmMasterThesis.Classes
{
    public class TestSlotcs
    {
        public int? previous_2 { get; set; }
        public bool isChoosen_Previous_2 { get; set; }
        public bool delete_previous_2 { get; set; }

        public int? previous_1 { get; set; }
        public bool delete_previous_1 { get; set; }
        public bool isChoosen_Previous_1 { get; set; }

        public int? next_1 { get; set; }
        public bool delete_next_1 { get; set; }
        public bool isChoosen_Next_1 { get; set; }

        public int? next_2 { get; set; }
        public bool delete_next_2 { get; set; }
        public bool isChoosen_Next_2 { get; set; }

        public bool Result { get; set; }

        public Offset offset { get; set; }


        public TestSlotcs()
        {
            isChoosen_Previous_1 = false;
            isChoosen_Previous_2 = false;
            isChoosen_Next_1 = false;
            isChoosen_Next_2 = false;

            Result = false;
            delete_previous_2 = false;
            delete_previous_1 = false;
            delete_next_1 = false;
            delete_next_2 = false;
            offset = new Offset();
        }


    }
}