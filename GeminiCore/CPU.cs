﻿/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
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

        public Memory mem;
        Thread fetchThread;
        AutoResetEvent fetchEvent = new AutoResetEvent(false);
        Thread decodeThread;
        AutoResetEvent decodeEvent = new AutoResetEvent(false);
        Thread executeThread;
        AutoResetEvent executeEvent = new AutoResetEvent(false);
        Thread storeThread;
        AutoResetEvent storeEvent = new AutoResetEvent(false);

        public delegate void FetchDone(object sender, FetchEventArgs args);
        public event FetchDone OnFetchDone;
        public delegate void DecodeDone(object sender, DecodeEventArgs args);
        public event DecodeDone OnDecodeDone;
        public delegate void ExecuteDone(object sender, ExecuteEventArgs args);
        public event ExecuteDone OnExecuteDone;
        public delegate void StoreDone(object sender, StoreEventArgs args);
        public event StoreDone OnStoreDone;

        public delegate void ProgramDone(object sender, ProgramEventArgs args);
        public event ProgramDone OnProgramDone;

        bool areWeDone = false;
        bool branchTaken = false;
        bool branchTakenBefore = false;
        bool loadInst = false;

        public struct fetchStruct
        {
            public int fetchIR;
            public int PC;
        }

        public struct decodeStruct
        {
            public int IR;
            public int PC;
        }

        public struct executeStruct
        {
            public int IR;
            public ushort inst;
            public ushort imm;
            public ushort operand;
            public int ACC;
            public int PC;
        }

        public struct storeStruct
        {
            public int IR;
            public ushort inst;
            public ushort imm;
            public ushort operand;
            public int tempACC;
            public int PC;
        }

        public int GUIfetch;
        public int GUIdecode;
        public int GUIexecute;
        public int GUIstore;

        fetchStruct fetch = new fetchStruct();
        decodeStruct decode = new decodeStruct();
        executeStruct execute = new executeStruct();
        storeStruct store = new storeStruct();

        public Dictionary<int, string> instBinary = new Dictionary<int, string>()
        {
            { 1, "lda" },
            { 2, "sta" },
            { 3, "add" },
            { 4, "sub" },
            { 5, "mul" },
            { 6, "div" },
            { 7, "and" },
            { 8, "or" },
            { 9, "shl" },
            { 10, "nota" },
            { 11, "ba" },
            { 12, "be" },
            { 13, "bl" },
            { 14, "bg" },
            { 15, "nop" },
            { 16, "hlt" }
        };

        public CPU()
        {
            A = 0;
            B = 0;
            ACC = 0;
            ZERO = 0;
            ONE = 1;
            PC = 0;
            MAR = 0;
            MDR = 0;
            TEMP = 0;
            IR = 0;
            CC = 0;

            fetch.PC = 0;
            decode.PC = -1;
            execute.ACC = 0;
            execute.PC = -1;
            store.PC = -1;
            

            fetchThread = new Thread(new ThreadStart(PerformFetch));
            fetchThread.Name = "Fetch Thread";
            fetchThread.Start();

            decodeThread = new Thread(new ThreadStart(PerformDecode));
            decodeThread.Name = "Decode Thread";
            decodeThread.Start();

            executeThread = new Thread(new ThreadStart(PerformExecute));
            executeThread.Name = "Execute Thread";
            executeThread.Start();

            storeThread = new Thread(new ThreadStart(PerformStore));
            storeThread.Name = "Store Thread";
            storeThread.Start();

            GUIfetch = 0;
            GUIdecode = 0;
            GUIexecute = 0;
            GUIstore = 0;
        }

        public void Dispose()
        {
            areWeDone = true;
            fetchEvent.Set();
            fetchThread.Join();

            decodeEvent.Set();
            decodeThread.Join();

            executeEvent.Set();
            executeThread.Join();

            storeEvent.Set();
            storeThread.Join();
        }

        private void PerformFetch()
        {
            while (true)
            {
                if (areWeDone)
                {
                    break;
                }
                fetchEvent.WaitOne();

                fetch.fetchIR = mem.Instructions[fetch.PC];
                if (fetch.PC < mem.Instructions.Count - 1)
                {
                    fetch.PC++;
                    if (OnFetchDone != null)
                    {
                        OnFetchDone(this, new FetchEventArgs(fetch.fetchIR));
                    }
                }
            }
        }

        public void PerformDecode()
        {
            while (true)
            {
                if (areWeDone)
                {
                    break;
                }
                decodeEvent.WaitOne();

                if (decode.IR > -1 && decode.PC > -1 && decode.PC < mem.Instructions.Count)
                {
                    execute.inst = (ushort)(decode.IR >> 9);
                    execute.imm = (ushort)((decode.IR & 256) >> 8);
                    ushort temp = (ushort)(decode.IR << 8);
                    execute.operand = (ushort)(temp >> 8);

                    if (OnDecodeDone != null)
                    {
                        OnDecodeDone(this, new DecodeEventArgs(decode.IR));
                    }
                }
            }
        }

        public void PerformExecute()
        {
            while (true)
            {
                if (areWeDone)
                {
                    break;
                }
                executeEvent.WaitOne();

                if (execute.PC > -1 && execute.PC < mem.Instructions.Count)
                {
                    if (execute.imm == 0)
                    {
                        if (execute.operand > 255)
                        {
                            MessageBox.Show("Runtime error: segmentation fault at instruction " + PC,
                                "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    switch (execute.inst)
                    {
                        case 1:
                            if (execute.imm == 1)
                            {
                                execute.ACC = execute.operand;
                                loadInst = true;
                            }
                            else
                            {
                                execute.ACC = mem[execute.operand];
                                loadInst = true;
                            }
                            break;
                        case 2:
                            break;
                        case 3:
                            if (execute.imm == 1)
                            {
                                execute.ACC += execute.operand;
                            }
                            else
                            {
                                execute.ACC += mem[execute.operand];
                            }
                            break;
                        case 4:
                            if (execute.imm == 1)
                            {
                                execute.ACC -= execute.operand;
                            }
                            else
                            {
                                execute.ACC -= mem[execute.operand];
                            }
                            break;
                        case 5:
                            if (execute.imm == 1)
                            {
                                execute.ACC *= execute.operand;
                            }
                            else
                            {
                                execute.ACC *= mem[execute.operand];
                            }
                            break;
                        case 6:
                            if (execute.imm == 1)
                            {
                                execute.ACC /= execute.operand;
                            }
                            else
                            {
                                execute.ACC /= mem[execute.operand];
                            }
                            break;
                        case 7:
                            if (execute.imm == 1)
                            {
                                if (execute.ACC > 0 && execute.operand > 0)
                                {
                                    execute.ACC = 1;
                                }
                                else
                                {
                                    execute.ACC = 0;
                                }
                            }
                            else
                            {
                                if (execute.ACC > 0 && mem[execute.operand] > 0)
                                {
                                    execute.ACC = 1;
                                }
                                else
                                {
                                    execute.ACC = 0;
                                }
                            }
                            break;
                        case 8:
                            if (execute.imm == 1)
                                if (execute.ACC > 0 || execute.operand > 0)
                                {
                                    execute.ACC = 1;
                                }
                                else
                                {
                                    execute.ACC = 0;
                                }
                            else
                                if (execute.ACC > 0 || mem[execute.operand] > 0)
                                {
                                    execute.ACC = 1;
                                }
                                else
                                {
                                    execute.ACC = 0;
                                }
                            break;
                        case 9:
                            execute.ACC = execute.ACC << execute.operand;
                            break;
                        case 10:
                            if (execute.ACC > 0) execute.ACC = 0;
                            else execute.ACC = 1;
                            break;
                        case 11: // ba
                            PC = execute.operand;
                            branchTaken = true;
                            break;
                        case 12: // be
                            if (CC == 0)
                            {
                                PC = execute.operand;
                                branchTaken = true;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 13: // bl
                            if (CC == -1)
                            {
                                PC = execute.operand;
                                branchTaken = true;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 14: // bg
                            if (CC == 1)
                            {
                                PC = execute.operand;
                                branchTaken = true;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 15:
                            execute.ACC += ZERO;
                            break;
                        case 16:
                            PC = mem.Instructions.Count;
                            break;
                        default:
                            break;
                    }
                    if (execute.ACC > 0)
                    {
                        CC = 1;
                    }
                    else if (execute.ACC < 0)
                    {
                        CC = -1;
                    }
                    else
                    {
                        CC = 0;
                    }
                    if (OnExecuteDone != null)
                    {
                        OnExecuteDone(this, new ExecuteEventArgs(execute.IR));
                    }
                }
            }
        }

        public void PerformStore()
        {
            while (true)
            {
                //Console.WriteLine("store pc: " + store.PC);
                if (areWeDone)
                {
                    break;
                }
                storeEvent.WaitOne();
                if (store.PC > -1 && store.PC < mem.Instructions.Count)
                {
                    switch (store.inst)
                    {
                        case 1:
                            if (store.imm == 1)
                            {
                                ACC = store.tempACC;
                            }
                            else
                            {
                                ACC = store.tempACC;
                            }
                            break;
                        case 2:
                            mem[store.operand] = store.tempACC;
                            break;
                        case 3:
                            if (store.imm == 1)
                            {
                                ACC = store.tempACC;
                            }
                            else
                            {
                                ACC = store.tempACC;
                            }
                            break;
                        case 4:
                            if (store.imm == 1)
                            {
                                ACC = store.tempACC;
                            }
                            else
                            {
                                ACC = store.tempACC;
                            }
                            break;
                        case 5:
                            if (store.imm == 1)
                            {
                                ACC = store.tempACC;
                            }
                            else
                            {
                                ACC = store.tempACC;
                            }
                            break;
                        case 6:
                            if (store.imm == 1)
                            {
                                ACC = store.tempACC;
                            }
                            else
                            {
                                ACC = store.tempACC;
                            }
                            break;
                        case 7:
                            if (store.imm == 1)
                            {
                                if (store.tempACC > 0 && store.operand > 0)
                                {
                                    ACC = store.tempACC;
                                }
                                else
                                {
                                    ACC = store.tempACC;
                                }
                            }
                            else
                            {
                                if (store.tempACC > 0 && mem[store.operand] > 0)
                                {
                                    ACC = store.tempACC;
                                }
                                else
                                {
                                    ACC = store.tempACC;
                                }
                            }
                            break;
                        case 8:
                            if (store.imm == 1)
                                if (store.tempACC > 0 || store.operand > 0)
                                {
                                    ACC = store.tempACC;
                                }
                                else
                                {
                                    ACC = store.tempACC;
                                }
                            else
                                if (ACC > 0 || mem[store.operand] > 0)
                                {
                                    ACC = store.tempACC;
                                }
                                else
                                {
                                    ACC = store.tempACC;
                                }
                            break;
                        case 9:
                            ACC = store.tempACC;
                            break;
                        case 10:
                            if (ACC > 0) ACC = store.tempACC;
                            else ACC = store.tempACC;
                            break;
                        case 11:
                            PC = store.tempACC;
                            break;
                        case 12:
                            if (CC == 0)
                            {
                                PC = store.tempACC;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 13:
                            if (CC == -1)
                            {
                                PC = store.tempACC;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 14:
                            if (CC == 1)
                            {
                                PC = store.tempACC;
                            }
                            else
                            {
                                //do nothing
                            }
                            break;
                        case 15:
                            ACC = store.tempACC;
                            break;
                        case 16:
                            PC = mem.Instructions.Count;
                            break;
                        default:
                            break;
                    }
                    if (ACC > 0)
                    {
                        CC = 1;
                    }
                    else if (ACC < 0)
                    {
                        CC = -1;
                    }
                    else
                    {
                        CC = 0;
                    }
                }
                if (OnStoreDone != null)
                {
                    OnStoreDone(this, new StoreEventArgs(store.IR));
                }
                else if (store.PC == mem.Instructions.Count)
                {
                    Console.WriteLine("about to dispose");
                    Dispose();
                    Console.WriteLine("disposed");
                    if (OnProgramDone != null)
                    {
                        OnProgramDone(this, new ProgramEventArgs());
                        Console.WriteLine("on program done called");
                    }
                }
            }
        }

        public void executeInstruction()
        {
            if (loadInst)
            {
                // wait one cycle on load
                Console.WriteLine("load wait");
                loadInst = false;
            }
            else if (branchTaken)
            {
                fetch.PC = PC;
                Console.WriteLine("fetch PC: " + fetch.PC);
                decode.PC = -1;
                execute.PC = -1;

                decode.IR = -1;

                store.inst = execute.inst;
                store.imm = execute.imm;
                store.operand = execute.operand;
                store.tempACC = execute.ACC;
                store.PC = execute.PC;

                branchTaken = false;
                branchTakenBefore = true;
            }
            else
            {
                if (!branchTakenBefore)
                {
                    //IR
                    store.IR = execute.IR;
                    execute.IR = decode.IR;
                    decode.IR = fetch.fetchIR;

                    //fetch
                    decode.PC = fetch.PC;

                    //decode
                    execute.PC = decode.PC;

                    //execute
                    store.inst = execute.inst;
                    store.imm = execute.imm;
                    store.operand = execute.operand;
                    store.tempACC = execute.ACC;
                    store.PC = execute.PC;
                }
                branchTakenBefore = false;

                fetchEvent.Set();
                decodeEvent.Set();
                executeEvent.Set();
                storeEvent.Set();
            }
        }

        public void resetCPU()
        {
            A = 0;
            B = 0;
            ACC = 0;
            ZERO = 0;
            ONE = 1;
            PC = 0;
            MAR = 0;
            MDR = 0;
            TEMP = 0;
            IR = 0;
            CC = 0;
        }

        //public void executeInstruction()
        //{
        //    if (PC < mem.Instructions.Count)
        //    {
        //        ushort inst = (ushort)(mem.Instructions[PC] >> 9);
        //        ushort imm = (ushort)((mem.Instructions[PC] & 256) >> 8);
        //        ushort temp = (ushort)(mem.Instructions[PC] << 8);
        //        ushort operand = (ushort)(temp >> 8);

        //        if (imm == 0)
        //        {
        //            if (operand > 255)
        //            {
        //                MessageBox.Show("Runtime error: segmentation fault at instruction " + PC, 
        //                    "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }

        //        switch (inst)
        //        {
        //            case 1:
        //                if (imm == 1)
        //                {
        //                    ACC = operand;
        //                }
        //                else
        //                {
        //                    ACC = mem[operand];
        //                }
        //                PC++;
        //                break;
        //            case 2:
        //                mem[operand] = ACC;
        //                PC++;
        //                break;
        //            case 3:
        //                if (imm == 1)
        //                {
        //                    ACC += operand;
        //                }
        //                else
        //                {
        //                    ACC += mem[operand];
        //                }
        //                PC++;
        //                break;
        //            case 4:
        //                if (imm == 1)
        //                {
        //                    ACC -= operand;
        //                }
        //                else
        //                {
        //                    ACC -= mem[operand];
        //                }
        //                PC++;
        //                break;
        //            case 5:
        //                if (imm == 1)
        //                {
        //                    ACC *= operand;
        //                }
        //                else
        //                {
        //                    ACC *= mem[operand];
        //                }
        //                PC++;
        //                break;
        //            case 6:
        //                if (imm == 1)
        //                {
        //                    ACC /= operand;
        //                }
        //                else
        //                {
        //                    ACC /= mem[operand];
        //                }
        //                PC++;
        //                break;
        //            case 7:
        //                if (imm == 1)
        //                {
        //                    if (ACC > 0 && operand > 0)
        //                    {
        //                        ACC = 1;
        //                    }
        //                    else
        //                    {
        //                        ACC = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    if (ACC > 0 && mem[operand] > 0)
        //                    {
        //                        ACC = 1;
        //                    }
        //                    else
        //                    {
        //                        ACC = 0;
        //                    }
        //                }
        //                PC++;
        //                break;
        //            case 8:
        //                if (imm == 1)
        //                    if (ACC > 0 || operand > 0)
        //                    {
        //                        ACC = 1;
        //                    }
        //                    else
        //                    {
        //                        ACC = 0;
        //                    }
        //                else
        //                    if (ACC > 0 || mem[operand] > 0)
        //                    {
        //                        ACC = 1;
        //                    }
        //                    else
        //                    {
        //                        ACC = 0;
        //                    }
        //                PC++;
        //                break;
        //            case 9:
        //                ACC = ACC << operand;
        //                PC++;
        //                break;
        //            case 10:
        //                if (ACC > 0) ACC = 0;
        //                else ACC = 1;
        //                PC++;
        //                break;
        //            case 11:
        //                PC = operand;
        //                break;
        //            case 12:
        //                if (CC == 0)
        //                {
        //                    PC = operand;
        //                }
        //                else
        //                {
        //                    PC++;
        //                }
        //                break;
        //            case 13:
        //                if (CC == -1)
        //                {
        //                    PC = operand;
        //                }
        //                else
        //                {
        //                    PC++;
        //                }
        //                break;
        //            case 14:
        //                if (CC == 1)
        //                {
        //                    PC = operand;
        //                }
        //                else
        //                {
        //                    PC++;
        //                }
        //                break;
        //            case 15:
        //                ACC += ZERO;
        //                PC++;
        //                break;
        //            case 16:
        //                PC = mem.Instructions.Count;
        //                PC++;
        //                break;
        //            default:
        //                break;
        //        }
        //        if (ACC > 0)
        //        {
        //            CC = 1;
        //        }
        //        else if (ACC < 0)
        //        {
        //            CC = -1;
        //        }
        //        else
        //        {
        //            CC = 0;
        //        }
        //    }
        //}


        public void executeAllInstructions()
        {
            while (PC < mem.Instructions.Count)
            {
                executeInstruction();
            }
            Console.WriteLine("Cache Size: " + mem.cacheSize);
            Console.WriteLine("Cache Type: " + mem.cacheType);
            Console.WriteLine("Block Size: " + mem.blockSize);
        }

        public string firstInstToString()
        {
            try
            {
                if (mem.Instructions.Count > 0)
                {
                    string instruction = "";
                    ushort inst = (ushort)(mem.Instructions[0] >> 9);
                    ushort imm = (ushort)((mem.Instructions[0] & 256) >> 8);
                    ushort temp = (ushort)(mem.Instructions[0] << 8);
                    ushort operand = (ushort)(temp >> 8);

                    instruction = instBinary[inst] + " " + imm + " " + operand;
                    return instruction;
                }
            }
            catch(Exception e)
            {
                return "No Instructions";
            }
            return "No Instructions";
        }

        public string instToString(int ir)
        {
            string instruction = "";
            ushort inst = (ushort)(ir >> 9);
            ushort imm = (ushort)((ir & 256) >> 8);
            ushort temp = (ushort)(ir << 8);
            ushort operand = (ushort)(temp >> 8);


            if (instBinary.ContainsKey(inst))
            {
                instruction = instBinary[inst] + " " + imm + " " + operand;
            }
            else
            {
                instruction = "Instruction not found";
            }
            return instruction;
        }

        public string nextInstToString()
        {
            if (PC < mem.Instructions.Count)
            {
                string instruction = "";
                ushort inst = (ushort)(mem.Instructions[PC] >> 9);
                ushort imm = (ushort)((mem.Instructions[PC] & 256) >> 8);
                ushort temp = (ushort)(mem.Instructions[PC] << 8);
                ushort operand = (ushort)(temp >> 8);

                instruction = instBinary[inst] + " " + imm + " " + operand;
                return instruction;
            }
            else
            {
                return "End of program";
            }
        }
    }
}
