using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classobj
{
    class VirtualMemoryObj
    {
        public List<List<int>> table { get; set; }
        public int faults { get; set; }
        public List<int> refStrings { get; set; }

        public VirtualMemoryObj(List<List<int>> _table, int _faults, List<int> _refStrings)
        {
            table = _table;
            faults = _faults;
            refStrings = _refStrings;
        }
    }
}
