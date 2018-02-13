using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classobj
{
    class Scheduled_Process
    {
        public Boolean isIdle { get; set; }
        public Process process { get; set; }
        public int start_timestamp { get; set; }
        public int end_timestamp { get; set; }

        public Scheduled_Process(Process _process, int _start_timestamp, int _end_timestamp)
        {
            process = _process;
            start_timestamp = _start_timestamp;
            end_timestamp = _end_timestamp;
            isIdle = false;
        }

        public Scheduled_Process(int _start_timestamp, int _end_timestamp)
        {
            isIdle = true;
            process = new Process(0, 0, 0, 0);
            start_timestamp = _start_timestamp;
            end_timestamp = _end_timestamp;
        }
    }

    class Process
    {
        //Identifier
        public int process_id { get; set; }
        //CPU Scheduling
        public int arrival_time { get; set; }
        public int burst_time { get; set; }
        public int priority { get; set; }
        public int turnaround_time { get; set; }
        public int waiting_time { get; set; }
        //Main Memory
        public int memory_size { get; set; }

        public Process(int _process_id, int _arrival_time, int _burst_time, int _priority, int _memory_size)
        {
            process_id = _process_id;
            arrival_time = _arrival_time;
            burst_time = _burst_time;
            priority = _priority;
            memory_size = _memory_size;
        }

        public Process(int _process_id, int _arrival_time, int _burst_time, int _memory_size)
        {
            process_id = _process_id;
            arrival_time = _arrival_time;
            burst_time = _burst_time;
            priority = 1;
            memory_size = _memory_size;
        }
    }
}
