using classobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualConsole
{
    class VirtualMemory
    {
        public VirtualMemoryObj First_in_First_out(int num_ofFrames, int frame_size, List<Process> processes) 
        {
            int pageFault = 0;
            int index = 0;
            List<int> memory_sizes = new List<int>();
            Boolean page_existing;

            
            List<int> refStrings = new List<int>();
            int total_process_sizes = 0;
            //calculate total processes sizes
            foreach (Process process in processes)
            {
                total_process_sizes += process.memory_size; 
            }

            //calculate length of reference string based on frame size
            int refString_length = total_process_sizes / frame_size;

            //create reference string
            Random rdm = new Random();
            for (int i = 0; i < refString_length; i++)
            {
                refStrings.Add(rdm.Next(1, refString_length/(processes.Count)));
            }


            List<List<int>> memory_history = new List<List<int>>();
            foreach (int refString in refStrings)
            {
                page_existing = Check_Queue(memory_sizes, refString);

                //When memory_sizes is not full
                if (memory_sizes.Count < num_ofFrames)
                {
                    //Add page if page doesn't exisit
                    if (!page_existing)
                    {
                        memory_sizes.Add(refString);
                        pageFault++;
                    }
                    memory_history.Add(new List<int>(memory_sizes));
                }

                //When memory_sizes is full
                else
                {
                    //Replace page in memory_sizes
                    if (!page_existing)
                    {
                        memory_sizes[index] = refString;
                        index = FIFO_Counter(index, num_ofFrames);
                        pageFault++;
                    }
                    memory_history.Add(new List<int>(memory_sizes));
                }
            }

            List<List<int>> table = format_table(num_ofFrames, refStrings, memory_history);
            VirtualMemoryObj results = new VirtualMemoryObj(table, pageFault, refStrings);
            return results;
        }

        public VirtualMemoryObj Least_Recently_Used(int num_ofFrames, int frame_size, List<Process> processes)
        {
            int pageFault = 0;
            int index = 0;
            List<int> memory_sizes = new List<int>();
            Boolean page_existing;

            int referenceString_index = 0;

            List<int> refStrings = new List<int>();
            int total_process_sizes = 0;
            //calculate total processes sizes
            foreach (Process process in processes)
            {
                total_process_sizes += process.memory_size;
            }

            //calculate length of reference string based on frame size
            int refString_length = total_process_sizes / frame_size;

            //create reference string
            Random rdm = new Random();
            for (int i = 0; i < refString_length; i++)
            {
                refStrings.Add(rdm.Next(0, refString_length / (processes.Count)));
            }

            List<List<int>> memory_history = new List<List<int>>();
            foreach (int refString in refStrings)
            {
                page_existing = Check_Queue(memory_sizes, refString);

                //When memory_sizes is not full
                if (memory_sizes.Count < num_ofFrames)
                {
                    //Add page if page doesn't exisit
                    if (!page_existing)
                    {
                        memory_sizes.Add(refString);
                        pageFault++;
                    }
                    memory_history.Add(new List<int>(memory_sizes));
                }

                //When memory_sizes is full
                else
                {
                    //Replace page in queue
                    if (!page_existing)
                    {
                        index = LRU_Counter(refStrings, memory_sizes, referenceString_index);
                        memory_sizes[index] = refString;
                        pageFault++;
                    }
                    memory_history.Add(new List<int>(memory_sizes));
                }
                referenceString_index++;
            }

            List<List<int>> table = format_table(num_ofFrames, refStrings, memory_history);
            VirtualMemoryObj results = new VirtualMemoryObj(table, pageFault, refStrings);
            return results;
        }

        /// <summary>
        /// Check queue for exisiting refrenced string 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="referenceString"></param>
        /// <returns>Returns a true if string already exists in queue</returns>
        private Boolean Check_Queue(List<int> queue, int referenceString)
        {
            Boolean existing = false;
            foreach (int pageInQueue in queue)
            {
                if (pageInQueue == referenceString)
                {
                    existing = true;
                    break;
                }
            }
            return existing;
        }

        /// <summary>
        /// Index counter for FIFO
        /// </summary>
        /// <param name="last_replacedIndex"></param>
        /// <param name="num_ofFrames"></param>
        /// <returns>Next index</returns>
        private int FIFO_Counter(int last_replacedIndex, int num_ofFrames)
        {
            int next_index;

            if (last_replacedIndex == num_ofFrames - 1)
            {
                next_index = 0;
            }

            else
            {
                next_index = last_replacedIndex + 1;
            }

            return next_index;
        }

        /// <summary>
        /// Index counter for LRU
        /// </summary>
        /// <param name="refStrings">Reference string</param>
        /// <param name="memory_sizes">Current memory_sizes</param>
        /// <param name="current_refString_index">Index of current reference string</param>
        /// <returns></returns>
        private int LRU_Counter(List<int> refStrings, List<int> memory_sizes, int current_refString_index)
        {
            int next_index = 0;
            int memory_index_counter = 0;
            int count = 0;
            int i;

            for (i = current_refString_index - 1; i >= 0; i--)
            {
                memory_index_counter = 0;
                foreach (int m in memory_sizes)
                {
                    if (refStrings[i] == m)
                    {
                        next_index = memory_index_counter;
                        count++;
                    }
                    memory_index_counter++;
                }
                if (count == memory_sizes.Count())
                {
                    break;
                }
            }

            return next_index;
        }

        private List<List<int>> format_table(int num_ofRows, List<int> refStrings, List<List<int>> memory_history)
        {
            List<List<int>> table = new List<List<int>>();
            for (int i = 0; i < num_ofRows; i++)
                table.Add(new List<int>());

            //Rearrange memory_sizes table to be displayed
            int count = 0;
            foreach (List<int> mem in memory_history)
            {
                foreach (int frame in mem)
                {
                    table[count].Add(frame);
                    count++;
                }

                if (count <= num_ofRows)
                {
                    for (int i = count; i < num_ofRows; i++)
                    {
                        table[count].Add(0);
                        count++;
                    }
                }

                count = 0;
            }

            return table;
        }
    }

}
