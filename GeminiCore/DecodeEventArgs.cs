using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class DecodeEventArgs
    {
        public int CurrentIR { get; set; }

        public DecodeEventArgs(int ir)
        {
            CurrentIR = ir;
        }
    }
}
