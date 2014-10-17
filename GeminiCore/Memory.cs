/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication2;


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

        public WindowsFormsApplication2.Form1 form;
        public int cacheType;
        public int cacheSize;
        public int blockSize;

        public struct block
        {
            public int dirty;
            public int tag;
            public int value;
        }

        // DIRECT MAP
        #region
        public int this[int i]
        {
            get
            {
                if (cacheType == 1)
                {
                    int location = i % cacheSize;
                    int location2 = (i + 1) % cacheSize;
                    if (cache[location].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("get hit");
                        return cache[location].value;
                    }
                    else
                    {
                        //MISS
                        missCount++;
                        hitMiss = "Miss";
                        Console.WriteLine("get miss");
                        if (cache[location].dirty == 0)
                        {
                            //CLEAN
                            cache[location].dirty = 0;
                            cache[location].tag = i;
                            cache[location].value = memory[i];
                            if (i > 254 && blockSize == 2)
                            {
                                cache[location2].dirty = 0;
                                cache[location2].tag = i + 1;
                                cache[location2].value = memory[i + 1];
                            }
                            return cache[location].value;
                        }
                        else
                        {
                            //DIRTY
                            memory[cache[location].tag] = cache[location].value;
                            cache[location].dirty = 0;
                            cache[location].tag = i;
                            cache[location].value = memory[i];
                            if (i > 254 && blockSize == 2)
                            {
                                cache[location2].dirty = 0;
                                cache[location2].tag = i + 1;
                                cache[location2].value = memory[i + 1];
                            }
                            return cache[location].value;
                        }
                    }
                }
                else
                {
                    int numSets = cacheSize / 2;
                    int location = i % numSets;
                    int location2 = (i + 1) % numSets;
                    if (cache[2 * location].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("get hit");
                        return cache[2 * location].value;
                    }
                    else if (cache[(2 * location) + 1].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("get hit");
                        return cache[(2 * location) + 1].value;
                    }
                    else
                    {
                        //MISS
                        missCount++;
                        hitMiss = "Miss";
                        Console.WriteLine("get miss");

                        int loc;
                        Random rand = new Random();
                        if (rand.Next(0, 2) == 0)
                        {
                            loc = 2 * location;
                        }
                        else
                        {
                            loc = 2 * location + 1;
                        }
                        if (cache[loc].dirty == 0)
                        {
                            //CLEAN
                            cache[loc].dirty = 0;
                            cache[loc].tag = i;
                            cache[loc].value = memory[i];
                            if (i > 254 && blockSize == 2)
                            {
                                cache[location2].dirty = 0;
                                cache[location2].tag = i + 1;
                                cache[location2].value = memory[i + 1];
                            }
                            return cache[loc].value;
                        }
                        else
                        {
                            //DIRTY
                            memory[cache[loc].tag] = cache[loc].value;
                            cache[loc].dirty = 0;
                            cache[loc].tag = i;
                            cache[loc].value = memory[i];
                            if (i > 254 && blockSize == 2)
                            {
                                cache[location2].dirty = 0;
                                cache[location2].tag = i + 1;
                                cache[location2].value = memory[i + 1];
                            }
                            return cache[loc].value;
                        }
                    }
                }
            }
            set
            {
                if (cacheType == 1)
                {
                    int location = i % cacheSize;
                    if (cache[location].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("set hit");
                        cache[location].value = value;
                        cache[location].dirty = 1;
                    }
                    else
                    {
                        //MISS
                        missCount++;
                        hitMiss = "Miss";
                        Console.WriteLine("set miss");
                        memory[i] = value;
                    }
                }
                else
                {
                    int numSets = cacheSize / 2;
                    int location = i % numSets;
                    if (cache[location * 2].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("set hit");
                        cache[location].value = value;
                        cache[location].dirty = 1;
                    }
                    else if (cache[(2 * location) + 1].tag == i)
                    {
                        //HIT
                        hitCount++;
                        hitMiss = "Hit";
                        Console.WriteLine("set hit");
                        cache[(2 * location) + 1].value = value;
                        cache[(2 * location) + 1].dirty = 1;
                    }
                    else
                    {
                        //MISS
                        missCount++;
                        hitMiss = "Miss";
                        Console.WriteLine("set miss");
                        memory[i] = value;
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

    }
}
