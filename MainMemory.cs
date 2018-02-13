using classobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class MainMemory
    {

        public MainMemoryObj First_Fit(List<int> partition_sizes, List<Process> processes)
        {
            int num_of_partitions = partition_sizes.Count;
            int num_of_processes = processes.Count;
            List<int> checker = declare_checker(num_of_partitions);
            List<int> original = partition_sizes;

            Boolean useVirtualMemory;

            int internalfrag = 0;
            int externalfrag = 0;

            List<Partiton> partition = new List<Partiton>();
            //Create a partition object for each partion size listed
            foreach (int partitonSize in partition_sizes)
            {
                partition.Add(new Partiton(partitonSize));
            }

            List<Process> virtual_memory_processes = new List<Process>();
            //iterate processes to enter memory
            for (int x = 0; x < num_of_processes; x++)
            {
                useVirtualMemory = true;
                //select a partition
                for (int y = 0; y < num_of_partitions; y++)
                {
                    if (partition_sizes[y] >= processes[x].memory_size)
                    {
                        checker[y] = 1;

                        partition[y].residing_processes.Add(processes[x]);
                        partition_sizes[y] -= processes[x].memory_size;
                        partition[y].available_partitionSize = partition_sizes[y];

                        useVirtualMemory = false;
                        break;
                    }
                }

                if(useVirtualMemory)
                {
                    virtual_memory_processes.Add(processes[x]);
                }
            }

            //Internal fragmentation
            foreach (Partiton p in partition)
            {
                if (p.residing_processes.Count > 0)
                    internalfrag += p.available_partitionSize;
            }

            //External fragmentation
            for (int x = 0; x < num_of_partitions; x++)
            {
                if(partition[x].residing_processes.Count == 0)
                {
                    externalfrag += partition[x].available_partitionSize;
                }
            }

            MainMemoryObj result = new MainMemoryObj(internalfrag, externalfrag, partition, virtual_memory_processes);
            return result;
        }

        public MainMemoryObj Best_Fit(List<int> partition_sizes, List<Process> processes)
        {
            int num_of_partitions = partition_sizes.Count;
            int num_of_processes = processes.Count;
            List<int> checker = declare_checker(num_of_partitions);
            List<int> original = partition_sizes;

            Boolean useVirtualMemory;

            int internalfrag = 0;
            int externalfrag = 0;

            List<Partiton> partition = new List<Partiton>();
            //Create a partition object for each partion size listed
            foreach (int partitonSize in partition_sizes)
            {
                partition.Add(new Partiton(partitonSize));
            }

            List<Process> virtual_memory_processes = new List<Process>();
            for (int x = 0; x < num_of_processes; x++)
            {
                useVirtualMemory = true;
                int best = -1;
                for (int y = 0; y < num_of_partitions; y++)
                {
                    if (partition_sizes[y] >= processes[x].memory_size)
                    {
                        useVirtualMemory = false;
                        if (best == -1)
                        {
                            checker[y] = 1;
                            best = y;
                        }
                        else if (partition_sizes[best] > partition_sizes[y])
                        {
                            checker[y] = 1;
                            best = y;
                        }
                    }
                }

                if (best != -1)
                {
                    //enter memory
                    partition[best].residing_processes.Add(processes[x]);
                    partition_sizes[best] -= processes[x].memory_size;
                    partition[best].available_partitionSize = partition_sizes[best];
                }

                if(useVirtualMemory)
                {
                    virtual_memory_processes.Add(processes[x]);
                }
            }

            //Internal fragmentation
            foreach (Partiton p in partition)
            {
                if (p.residing_processes.Count > 0)
                    internalfrag += p.available_partitionSize;
            }

            //External fragmentation
            for (int x = 0; x < num_of_partitions; x++)
            {
                if (partition[x].residing_processes.Count == 0)
                {
                    externalfrag += partition[x].available_partitionSize;
                }
            }

            MainMemoryObj result = new MainMemoryObj(internalfrag, externalfrag, partition, virtual_memory_processes);
            return result;
        }

        public MainMemoryObj Worst_Fit( List<int> partition_sizes, List<Process> processes)
        {
            int num_of_partitions = partition_sizes.Count;
            int num_of_processes = processes.Count;
            List<int> checker = declare_checker(num_of_partitions);
            List<int> original = partition_sizes;

            Boolean useVirtualMemory;

            int internalfrag = 0;
            int externalfrag = 0;

            List<Partiton> partition = new List<Partiton>();
            //Create a partition object for each partion size listed
            foreach (int partitonSize in partition_sizes)
            {
                partition.Add(new Partiton(partitonSize));
            }

            List<Process> virtual_memory_processes = new List<Process>();
            for (int x = 0; x < num_of_processes; x++)
            {
                useVirtualMemory = true;
                int worst = -1;
                for (int z = 0; z < num_of_partitions; z++)
                {
                    if (partition_sizes[z] >= processes[x].memory_size)
                    {
                        useVirtualMemory = false;
                        if (worst == -1)
                        {
                            checker[z] = 1;
                            worst = z;
                        }
                        else if (partition_sizes[worst] < partition_sizes[z])
                        {
                            checker[z] = 1;
                            worst = z;
                        }
                    }

                }
                if (worst != -1)
                {
                    partition[worst].residing_processes.Add(processes[x]);
                    partition_sizes[worst] -= processes[x].memory_size;
                    partition[worst].available_partitionSize = partition_sizes[worst];
                }

                if (useVirtualMemory)
                {
                    virtual_memory_processes.Add(processes[x]);
                }
            }

            //Internal fragmentation
            foreach (Partiton p in partition)
            {
                if (p.residing_processes.Count > 0)
                    internalfrag += p.available_partitionSize;
            }

            //external fragmentation
            for (int x = 0; x < num_of_partitions; x++)
            {
                if (partition[x].residing_processes.Count == 0)
                {
                    externalfrag += partition[x].available_partitionSize;
                }
            }

            MainMemoryObj result = new MainMemoryObj(internalfrag, externalfrag, partition, virtual_memory_processes);
            return result;
        }

        private List<int> declare_checker(int list_size)
        {
            List<int> checker = new List<int>();
            for (int i = 0; i < list_size; i++)
            {
                checker.Add(0);
            }
            return checker;
        }
    }
}

