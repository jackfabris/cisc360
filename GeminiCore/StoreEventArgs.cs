using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class StoreEventArgs
    {
        public int CurrentIR { get; set; }

        public StoreEventArgs(int ir)
        {
            CurrentIR = ir;
        }
    }
}
