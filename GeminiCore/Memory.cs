/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class Memory
    {
        public List<short> Instructions = new List<short>();   // Instruction Vector
        int[] memory = new int[255];// Main Memory

        public void addInstruction(short x) {
            Instructions.Add(x);
        }
    }
}
