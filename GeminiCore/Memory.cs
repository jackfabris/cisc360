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
        public List<ushort> Instructions = new List<ushort>();   // Instruction Vector
        public int[] memory = new int[256];// Main Memory

    }
}
