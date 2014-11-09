using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class ExecuteEventArgs
    {
        public int CurrentIR { get; set; }

        public ExecuteEventArgs(int ir)
        {
            CurrentIR = ir;
        }
    }
}
