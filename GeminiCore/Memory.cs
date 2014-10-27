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

        public block[] cache;

        public int hitCount = 0;
        public int missCount = 0;
        public string hitMiss = "";

        public int cacheType;
        public int cacheSize;
        public int blockSize;

        public struct block
        {
            public int dirty;
            public int tag;
            public int value;
        }

        // Implementation 1
        #region
        //public int this[int i]
        //{
        //    get
        //    {
        //        //DIRECT MAPPED
        //        if (cacheType == 1)
        //        {
        //            int numBlocks = cacheSize / 2;
        //            int numSets = numBlocks / 2;
        //            int location = i % cacheSize;
        //            int location2 = (i + 1) % cacheSize;
        //            if (cache[location].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("Direct Mapped get hit");
        //                return cache[location].value;
        //            }
        //            else
        //            {
        //                //MISS
        //                missCount++;
        //                hitMiss = "Miss";
        //                Console.WriteLine("Direct Mapped get miss");
        //                if (cache[location].dirty == 1)
        //                {
        //                    memory[cache[location].tag] = cache[location].value;
        //                }
        //                cache[location].dirty = 0;
        //                cache[location].tag = i;
        //                cache[location].value = memory[i];
        //                if (i < 254 && blockSize == 2)
        //                {
        //                    if (cache[location2].dirty == 1)
        //                    {
        //                        memory[cache[location2].tag] = cache[location2].value;
        //                    }
        //                    cache[location2].dirty = 0;
        //                    cache[location2].tag = i + 1;
        //                    cache[location2].value = memory[i + 1];                            
        //                }
        //                return cache[location].value;
        //            }
        //        }
        //        //2-WAY SET ASSOCIATIVE
        //        else
        //        {
        //            int numSets = cacheSize / 2;
        //            int location = i % numSets;
        //            int location2 = (i + 1) % numSets;
        //            if (cache[2 * location].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("2-way get hit");
        //                return cache[2 * location].value;
        //            }
        //            else if (cache[(2 * location) + 1].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("2-way get hit");
        //                return cache[(2 * location) + 1].value;
        //            }
        //            else
        //            {
        //                //MISS
        //                missCount++;
        //                hitMiss = "Miss";
        //                Console.WriteLine("2-way get miss");

        //                int loc;
        //                int loc2;
        //                Random rand = new Random();
        //                if (rand.Next(0, 2) == 0)
        //                {
        //                    loc = 2 * location;
        //                    loc2 = 2 * location2;

        //                }
        //                else
        //                {
        //                    loc = 2 * location + 1;
        //                    loc2 = 2 * location2 + 1;
        //                }
        //                if (cache[loc].dirty == 1)
        //                {
        //                    memory[cache[loc].tag] = cache[loc].value;
        //                }
        //                cache[loc].dirty = 0;
        //                cache[loc].tag = i;
        //                cache[loc].value = memory[i];
        //                if (i < 254 && blockSize == 2)
        //                {
        //                    if (cache[loc2].dirty == 1)
        //                    {
        //                        memory[cache[loc2].tag] = cache[loc2].value;
        //                    }
        //                    cache[loc2].dirty = 0;
        //                    cache[loc2].tag = i + 1;
        //                    cache[loc2].value = memory[i + 1];
        //                }
        //                return cache[loc].value;
        //            }
        //        }
        //    }
        //    set
        //    {
        //        //DIRECT MAPPED
        //        if (cacheType == 1)
        //        {
        //            int location = i % cacheSize;
        //            if (cache[location].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("direct set hit");
        //                if (cache[location].dirty == 1)
        //                {
        //                    memory[cache[location].tag] = cache[location].value;
        //                }
        //                cache[location].value = value;
        //                cache[location].dirty = 1;
        //            }
        //            else
        //            {
        //                //MISS
        //                missCount++;
        //                hitMiss = "Miss";
        //                Console.WriteLine("direct set miss");
        //                memory[i] = value;
        //            }
        //        }
        //        //2-WAY SET ASSOCIATIVE
        //        else
        //        {
        //            int numSets = cacheSize / 2;
        //            int location = i % numSets;
        //            int loc = 2 * location;
        //            if (cache[loc].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("2-way set hit");
        //                if (cache[loc].dirty == 1)
        //                {
        //                    memory[cache[loc].tag] = cache[loc].value;
        //                }
        //                cache[loc].value = value;
        //                cache[loc].dirty = 1;
        //            }
        //            else if (cache[loc + 1].tag == i)
        //            {
        //                //HIT
        //                hitCount++;
        //                hitMiss = "Hit";
        //                Console.WriteLine("2-way set hit");
        //                if (cache[loc + 1].dirty == 1)
        //                {
        //                    memory[cache[loc + 1].tag] = cache[loc + 1].value;
        //                } 
        //                cache[loc + 1].value = value;
        //                cache[loc + 1].dirty = 1;
        //            }
        //            else
        //            {
        //                //MISS
        //                missCount++;
        //                hitMiss = "Miss";
        //                Console.WriteLine("2-way set miss");
        //                memory[i] = value;
        //            }
        //        }
        //    }
        //}
        #endregion

        // Implementation 2
        #region
        public int this[int i]
        {
            get
            {
                int numBlocks = cacheSize;
                int numSets = numBlocks / cacheType;
                int location = (i % numSets) * blockSize;
                bool hit = false;
                int j;
                for (j = 0; j < cacheType * blockSize; j++)
                {
                    if (cache[location + j].tag == i)
                    {
                        hit = true;
                        break;
                    }
                }
                if (hit)
                {
                    hitCount++;
                    hitMiss = "Hit";
                    return cache[location + j].value;
                }
                else
                {
                    missCount++;
                    hitMiss = "Miss";
                    for (int k = 0; k < cacheType * blockSize; k++)
                    {
                        if (cache[location + k].dirty == 1)
                        {
                            memory[i + k * numSets] = cache[location + k].value;
                        }
                        cache[location + k].dirty = 0;
                        cache[location + k].tag = i;
                        cache[location + k].value = memory[i + k * numSets];
                    }
                    return cache[location].value;
                }
            }
            set
            {
                int numBlocks = cacheSize;
                int numSets = numBlocks / cacheType;
                int location = (i % numSets) * blockSize;
                for (int j = 0; j < cacheType * blockSize; j++)
                {
                    if (cache[location + j].tag == i)
                    {
                        if (j == 0)
                        {
                            hitCount++;
                            hitMiss = "Hit";
                        }
                        if (cache[location + j].dirty == 1)
                        {
                            memory[i + j * numSets] = cache[location + j].value;
                        }
                        cache[location + j].value = value;
                        cache[location + j].tag = i;
                        cache[location + j].dirty = 1;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            missCount++;
                            hitMiss = "Miss";
                        }
                        memory[i + j * numSets] = value;
                    }
                }

            }
        }
        #endregion

        public void clear()
        {
            Instructions.Clear();
            memory = new int[256];
            hitCount = 0;
            missCount = 0;
        }

        public void setCache(int cacheSize, int cacheType, int blockSize)
        {
            cache = null;
            this.cacheType = cacheType;
            this.cacheSize = cacheSize;
            this.blockSize = blockSize;
            cache = new block[cacheSize * blockSize];
            for (int i = 0; i < cache.Length; i++)
            {
                cache[i].tag = -1;
            }
        }

    }
}
