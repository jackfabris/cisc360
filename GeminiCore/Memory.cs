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

        public int cacheSize = 8;
        public block[] cache = new block[8];

        public struct block
        {
            public int dirty;
            public int tag;
            public int value;
        }

        public int this[int i]
        {
            get
            {
                int location = i % cacheSize;
                if (cache[location].dirty == 0)
                {
                    if (i == cache[location].tag)
                    {
                        return cache[location].value;
                    }
                    else
                    {
                        block b = new block();
                        b.tag = i;
                        b.dirty = 0;
                        b.value = memory[i];
                        cache[location] = b;
                        return cache[location].value;
                    }
                }
                else
                {
                    block b = new block();
                    b.tag = i;
                    b.dirty = 0;
                    b.value = memory[i];
                    cache[location] = b;
                    return cache[location].value;
                }
            }
            /*
             * Random replacement - if each block of cache is only supposed to be linked to certain
             * addresses in memory, why would you randomly replace the blocks of cache?
             * 
             * Dirty tag - when should something be marked as dirty?
             * 
             * Set - how do we get what they want to set it to?
             */
            set
            {
                int location = i % cacheSize;
                if (cache[location].dirty == 0)
                {
                    block b = new block();
                    b.tag = i;
                    b.dirty = 1;
                    b.value = memory[i];
                    cache[location] = b;
                    memory[i] = cache[location].value;
                }
                else
                {
                    memory[cache[location].tag] = cache[location].value;
                    block b = new block();
                    b.tag = i;
                }
            }
        }

        public void clear()
        {
            Instructions.Clear();
            memory = new int[256];
        }

    }
}
