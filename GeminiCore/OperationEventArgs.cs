using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public enum ThreadType
    {
        Fetch,
        Decode,
        Execute,
        Store
    }

    public class OperationEventArgs : EventArgs
    {
        public short CurrentIR { get; set; }
        public ThreadType CurrentThreadType { get; set; }

        public OperationEventArgs(ThreadType type, ushort ir)
        {
            CurrentThreadType = type;
            CurrentIR = ir;
        }
    }
}
