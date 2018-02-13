using classobj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUConsole
{
    class CPUScheduling
    {
        public List<Scheduled_Process> First_Come_First_Serve(List<Process> processes)
        {
            //Arrange by arrival time
            processes = rearrange_processes_byArrivalTime(processes);

            //Schedule Processes
            List<Scheduled_Process> scheduled_processes = new List<Scheduled_Process>();
            int end_timestamp = 0;
            int start_timestamp;
            foreach (Process p in processes)
            {
                start_timestamp = end_timestamp;

                //When process arrives after idle
                if (p.arrival_time > start_timestamp)
                {
                    scheduled_processes.Add(new Scheduled_Process(start_timestamp, p.arrival_time));
                    end_timestamp = p.burst_time + p.arrival_time;
                    scheduled_processes.Add(new Scheduled_Process(p, p.arrival_time, end_timestamp));
                }
                else
                {
                    end_timestamp += p.burst_time;
                    scheduled_processes.Add(new Scheduled_Process(p, start_timestamp, end_timestamp));
                }
            }

            //Calculate turn around time
            foreach(Scheduled_Process scheduled_process in scheduled_processes)
            {
                //turnaround time = time stamp where process finished - process arrival time
                Console.WriteLine(scheduled_process.end_timestamp + " " + scheduled_process.process.arrival_time );
                scheduled_process.process.turnaround_time = scheduled_process.end_timestamp - scheduled_process.process.arrival_time;
            }

            //Calculate waiting time
            foreach (Scheduled_Process scheduled_process in scheduled_processes)
            {
                //waiting time = turnaround time - burst time
                scheduled_process.process.waiting_time = scheduled_process.process.turnaround_time - scheduled_process.process.burst_time;
            }

            return scheduled_processes;
        }

        public List<Scheduled_Process> Nonpremptive_Priority(List<Process> processes)
        {
            //Arrange by arrival time
            processes = rearrange_processes_byArrivalTime(processes);

            //Arrange by priority
            int index = 0;
            List<int> indexs = new List<int>();
            int i = 0;
            int count = 0;

            //Get arrival time clusters
            while (count < processes.Count)
            {
                if (index < processes.Count)
                {
                    index = processes.Count(o => o.arrival_time == processes[index].arrival_time);
                    indexs.Add(index);
                    count += index;
                    index = count;
                    i++;
                }
                else
                {
                    break;
                }
            }

            //Rearrange by arrival time clusters
            count = 0;
            int index_reset_count = 0;
            List<Process> temp_processes = new List<Process>();
            List<Process> rearranged_processes = new List<Process>();
            for (int y = 0; y < indexs.Count; y++)
            {
                temp_processes.Clear();
                for (int x = 0; x < indexs[y]; x++)
                {
                    temp_processes.Add(processes[count]);
                    count++;
                    index_reset_count++;
                    temp_processes = rearrange_processes_byPriority(temp_processes);
                }
                foreach (Process tp in temp_processes)
                {
                    rearranged_processes.Add(tp);
                }
            }
            processes = rearranged_processes;

            //Rearrange by priority
            int end = 0;
            int start;
            temp_processes = new List<Process>();
            rearranged_processes = new List<Process>();
            count = 0;
            for (int y = 0; y < processes.Count; y++)
            {
                start = end;
                count++;
                temp_processes.Add(processes[y]);

                //If process arrive inbetween
                if (start > processes[y].arrival_time)
                {
                    for (int x = 0; x < count; x++)
                    {
                        rearranged_processes.Add(temp_processes[0]);
                        temp_processes.RemoveAt(0);
                    }
                    temp_processes = rearrange_processes_byPriority(temp_processes);
                    count = 0;
                }
                //If process has to wait or arrives shortly after
                else if (processes[y].arrival_time >= start)
                {

                    for (int x = 0; x < count; x++)
                    {
                        rearranged_processes.Add(temp_processes[0]);
                        temp_processes.RemoveAt(0);
                    }
                    temp_processes = rearrange_processes_byPriority(temp_processes);
                    count = 0;
                }

                //Calculate time_elapsed
                if (processes[y].arrival_time > start)
                {
                    end = processes[y].burst_time + processes[y].arrival_time;
                }
                else
                {
                    end += processes[y].burst_time;
                }
            }
            processes = rearranged_processes;

            ////Schedule Processes
            List<Scheduled_Process> scheduled_processes = new List<Scheduled_Process>();
            int end_timestamp = 0;
            int start_timestamp;
            foreach (Process p in processes)
            {
                start_timestamp = end_timestamp;

                //When process arrives after idle
                if (p.arrival_time > start_timestamp)
                {
                    scheduled_processes.Add(new Scheduled_Process(start_timestamp, p.arrival_time));
                    end_timestamp = p.burst_time + p.arrival_time;
                    scheduled_processes.Add(new Scheduled_Process(p, p.arrival_time, end_timestamp));
                }
                else
                {
                    end_timestamp += p.burst_time;
                    scheduled_processes.Add(new Scheduled_Process(p, start_timestamp, end_timestamp));
                }
            }

            //Calculate turn around time
            foreach (Scheduled_Process scheduled_process in scheduled_processes)
            {
                //turnaround time = time stamp where process finished - process arrival time
                Console.WriteLine(scheduled_process.end_timestamp + " " + scheduled_process.process.arrival_time);
                scheduled_process.process.turnaround_time = scheduled_process.end_timestamp - scheduled_process.process.arrival_time;
            }

            //Calculate waiting time
            foreach (Scheduled_Process scheduled_process in scheduled_processes)
            {
                //waiting time = turnaround time - burst time
                scheduled_process.process.waiting_time = scheduled_process.process.turnaround_time - scheduled_process.process.burst_time;
            }

            return scheduled_processes;
        }

        /// <summary>
        /// Rearrange processes by arrival time in ascending order
        /// </summary>
        /// <param name="processes"></param>
        /// <returns></returns>
        private List<Process> rearrange_processes_byArrivalTime(List<Process> processes)
        {
            return processes.OrderBy(o => o.arrival_time).ToList();
        }

        /// <summary>
        /// Rearrange processes by priority
        /// </summary>
        /// <param name="processes"></param>
        /// <returns></returns>
        private List<Process> rearrange_processes_byPriority(List<Process> processes)
        {
            return processes.OrderByDescending(o => o.priority).ToList();
        }
    }

}
