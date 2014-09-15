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
        List<short> Instructions;   // Instruction Vector
        int[] memory = new int[255];// Main Memory
    }
}
