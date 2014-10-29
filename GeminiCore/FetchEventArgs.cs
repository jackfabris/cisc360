using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    class FetchEventArgs : EventArgs
    {
        public int CurrentIR { get; set; }

        public FetchEventArgs(int ir)
        {
            CurrentIR = ir;
        }
    }
}
