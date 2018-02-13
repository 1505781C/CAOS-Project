using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classobj
{
    class MainMemoryObj
    {
        public int internalfrag { get; set; }
        public int externalfrag { get; set; }
        public List<Partiton> partitions { get; set; }
        public List<Process> virtual_memory_processes { get; set; }

        public MainMemoryObj(int _internalfrag, int _externalfrag, List<Partiton> _partitions, List<Process> _virtual_memory_processes)
        {
            internalfrag = _internalfrag;
            externalfrag = _externalfrag;
            partitions = _partitions;
            virtual_memory_processes = _virtual_memory_processes;
        }
    }

    class Partiton
    {
        public List<Process> residing_processes { get; set; }
        public int total_partitonSize;
        public int available_partitionSize;

        public Partiton(int _total_partitonSize)
        {
            total_partitonSize = _total_partitonSize;
            available_partitionSize = _total_partitonSize;
            residing_processes = new List<Process>();
        }
    }
}
