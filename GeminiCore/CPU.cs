/**
 * Seth Morecraft
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class CPU
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public int ACC { get; private set; }
        public int ZERO { get; private set; }
        public int ONE { get; private set; }
        public int PC { get; private set; }
        public int MAR { get; private set; }
        public int MDR { get; private set; }
        public int TEMP { get; private set; }
        public int IR { get; private set; }
        public int CC { get; private set; }

        public CPU()
        {
            ACC = 0;
        }

        public void nextInstruction()
        {
            ACC++;
        }
    }
}
