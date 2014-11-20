/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace GeminiCore
{
    public class CPU : IDisposable
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

        bool fetchRuns = true;
        bool decodeRuns = true;
        bool executeRuns = true;
        bool storeRuns = true;

        bool fetchDone, decodeDone, executeDone, storeDone;

        AutoResetEvent allThreadsDone = new AutoResetEvent(false);
        object allThreadsDoneLock = new object();

        public delegate void FetchDone(object sender, OperationEventArgs args);
        public event FetchDone OnFetchDone;
        public delegate void DecodeDone(object sender, OperationEventArgs args);
        public event DecodeDone OnDecodeDone;
        public delegate void ExecuteDone(object sender, OperationEventArgs args);
        public event ExecuteDone OnExecuteDone;
        public delegate void StoreDone(object sender, OperationEventArgs args);
        public event StoreDone OnStoreDone;

        bool areWeDone = false;
        bool branchTaken = false;
        bool loadInst = false;
        bool mulDivInst = false;
        int mulDivCount = 0;
        public int no_op_count = 0;
        bool bypassing = false;

        public struct fetchStruct
        {
            public ushort fetchIR;
            public int PC;
        }

        public struct decodeStruct
        {
            public ushort IR;
            public int PC;
        }

        public struct executeStruct
        {
            public ushort IR;
            public ushort inst;
            public ushort imm;
            public ushort operand;
            public int ACC;
            public int PC;
        }

        public struct storeStruct
        {
            public ushort IR;
            public ushort inst;
            public ushort imm;
            public ushort operand;
            public int tempACC;
            public int PC;
        }

        public struct branchPredictionStruct
        {
            public int branchPC;
            public int targetPC;
            public int timesTaken;
            public int timesNotTaken;
            public bool taken;
            public bool predictTaken;
        }

        public branchPredictionStruct[] branchPredictionTable;
        public bool predictionHit;

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

            this.OnFetchDone += CPU_OnThreadDone;
            this.OnDecodeDone += CPU_OnThreadDone;
            this.OnStoreDone += CPU_OnThreadDone;
            this.OnExecuteDone += CPU_OnThreadDone;

            GUIfetch = 0;
            GUIdecode = 0;
            GUIexecute = 0;
            GUIstore = 0;
        }

        void CPU_OnThreadDone(object sender, OperationEventArgs args)
        {
            // if all threads are done (use bools)
            switch (args.CurrentThreadType)
            {
                case ThreadType.Fetch:
                    fetchDone = true;
                    break;
                case ThreadType.Decode:
                    decodeDone = true;
                    break;
                case ThreadType.Execute:
                    executeDone = true;
                    break;
                case ThreadType.Store:
                    storeDone = true;
                    break;
            }

            lock (allThreadsDoneLock)
            {
                if (fetchDone && decodeDone && executeDone && storeDone)
                {
                    allThreadsDone.Set();
                }
            }
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
            while (!areWeDone)
            {
                fetchEvent.WaitOne();

                if (fetchRuns)
                {
                    fetch.fetchIR = mem.Instructions[fetch.PC];
                    if (fetch.PC < mem.Instructions.Count - 1)
                    {
                        fetch.PC++;
                    }
                }
                if (OnFetchDone != null)
                {
                    OnFetchDone(this, new OperationEventArgs(ThreadType.Fetch, fetch.fetchIR));
                }
            }
        }

        public void PerformDecode()
        {
            while (!areWeDone)
            {
                decodeEvent.WaitOne();
                
                if (decodeRuns)
                {

                    if (decode.PC > -1 && decode.PC < mem.Instructions.Count)
                    {
                        execute.inst = (ushort)(decode.IR >> 9);
                        execute.imm = (ushort)((decode.IR & 256) >> 8);
                        ushort temp = (ushort)(decode.IR << 8);
                        execute.operand = (ushort)(temp >> 8);
                    }
                }
                if (OnDecodeDone != null)
                {
                    OnDecodeDone(this, new OperationEventArgs(ThreadType.Decode, decode.IR));
                }
            }
        }

        public void PerformExecute()
        {
            while (!areWeDone)
            {
                executeEvent.WaitOne();
                if (executeRuns)
                {
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
                                branchPredictionStruct be = branchPredictionTable[execute.PC];
                                if (be.branchPC < 0)
                                {
                                    be.branchPC = execute.PC;
                                    be.targetPC = execute.operand;
                                    be.taken = false;
                                    be.predictTaken = false;
                                    be.timesNotTaken = 0;
                                    be.timesTaken = 0;
                                }
                                if (be.timesTaken >= 2)
                                {
                                    be.predictTaken = true;
                                }
                                else if (be.timesNotTaken >= 2)
                                {
                                    be.predictTaken = false;
                                }
                                if (CC == 0)
                                {
                                    if (be.predictTaken)
                                    {
                                        predictionHit = true;
                                    }
                                    else
                                    {
                                        predictionHit = false;
                                    }
                                    PC = be.targetPC;
                                    be.taken = true;
                                    branchTaken = true;
                                    be.timesTaken++;
                                    be.timesNotTaken = 0;
                                    branchPredictionTable[execute.PC] = be;
                                }
                                else
                                {
                                    if (be.predictTaken)
                                    {
                                        predictionHit = false;
                                    }
                                    else
                                    {
                                        predictionHit = true;
                                    }
                                    be.taken = false;
                                    branchTaken = false;
                                    be.timesTaken = 0;
                                    be.timesNotTaken++;
                                    branchPredictionTable[execute.PC] = be;
                                }
                                break;
                            case 13: // bl
                                branchPredictionStruct bl = branchPredictionTable[execute.PC];
                                if (bl.branchPC < 0)
                                {
                                    bl.branchPC = execute.PC;
                                    bl.targetPC = execute.operand;
                                    bl.taken = false;
                                    bl.predictTaken = false;
                                    bl.timesNotTaken = 0;
                                    bl.timesTaken = 0;
                                }
                                if (bl.timesTaken >= 2)
                                {
                                    bl.predictTaken = true;
                                }
                                else if (bl.timesNotTaken >= 2)
                                {
                                    bl.predictTaken = false;
                                }
                                if (CC == -1)
                                {
                                    if (bl.predictTaken)
                                    {
                                        predictionHit = true;
                                    }
                                    else
                                    {
                                        predictionHit = false;
                                    }
                                    PC = bl.targetPC;
                                    bl.taken = true;
                                    branchTaken = true;
                                    bl.timesTaken++;
                                    bl.timesNotTaken = 0;
                                    branchPredictionTable[execute.PC] = bl;
                                }
                                else
                                {
                                    if (bl.predictTaken)
                                    {
                                        predictionHit = false;
                                    }
                                    else
                                    {
                                        predictionHit = true;
                                    }
                                    bl.taken = false;
                                    branchTaken = false;
                                    bl.timesTaken = 0;
                                    bl.timesNotTaken++;
                                    branchPredictionTable[execute.PC] = bl;
                                }
                                break;
                            case 14: // bg
                                branchPredictionStruct bg = branchPredictionTable[execute.PC];
                                if (bg.branchPC < 0)
                                {
                                    bg.branchPC = execute.PC;
                                    bg.targetPC = execute.operand;
                                    bg.taken = false;
                                    bg.predictTaken = false;
                                    bg.timesNotTaken = 0;
                                    bg.timesTaken = 0;
                                }
                                if (bg.timesTaken >= 2)
                                {
                                    bg.predictTaken = true;
                                }
                                else if (bg.timesNotTaken >= 2)
                                {
                                    bg.predictTaken = false;
                                }
                                if (CC == 1)
                                {
                                    if (bg.predictTaken)
                                    {
                                        predictionHit = true;
                                    }
                                    else
                                    {
                                        predictionHit = false;
                                    }
                                    PC = bg.targetPC;
                                    bg.taken = true;
                                    branchTaken = true;
                                    bg.timesTaken++;
                                    bg.timesNotTaken = 0;
                                    branchPredictionTable[execute.PC] = bg;
                                }
                                else
                                {
                                    if (bg.predictTaken)
                                    {
                                        predictionHit = false;
                                    }
                                    else
                                    {
                                        predictionHit = true;
                                    }
                                    bg.taken = false;
                                    branchTaken = false;
                                    bg.timesTaken = 0;
                                    bg.timesNotTaken++;
                                    branchPredictionTable[execute.PC] = bg;
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
                    }
                }
                if (OnExecuteDone != null)
                {
                    OnExecuteDone(this, new OperationEventArgs(ThreadType.Execute, execute.IR));
                }
            }
        }

        public void PerformStore()
        {
            while (!areWeDone)
            {
                storeEvent.WaitOne();
                if (storeRuns)
                {
                    if (store.PC > -1 && store.PC < mem.Instructions.Count)
                    {
                        PC = store.PC;
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
                }
                if (OnStoreDone != null)
                {
                    OnStoreDone(this, new OperationEventArgs(ThreadType.Store, store.IR));
                }
            }
        }

        public void executeInstruction()
        {
            Console.WriteLine("NOOPs: " + no_op_count);
            fetchDone = false;
            decodeDone = false;
            executeDone = false;
            storeDone = false;

            //fetchRuns = false;
            //decodeRuns = false;
            //executeRuns = false;
            //storeRuns = false;

            if (loadInst && !bypassing)
            {
                no_op_count++;
                loadInst = false;
            }
            else if (mulDivInst)
            {
                no_op_count++;
                if (mulDivCount == 4)
                {
                    mulDivCount = 0;
                    mulDivInst = false;
                }
                else
                {
                    mulDivCount++;
                }

            }
            else if (!predictionHit && branchTaken)
            {
                no_op_count++;
                fetch.PC = PC;
                decode.PC = -1;
                execute.PC = -1;

                decode.IR = 0;

                store.inst = execute.inst;
                store.imm = execute.imm;
                store.operand = execute.operand;
                store.tempACC = execute.ACC;
                store.PC = execute.PC;

                branchTaken = false;
            }
            else
            {
                //check if branch was taken, if so transfer btwn threads
               

                fetchEvent.Set();
                decodeEvent.Set();
                executeEvent.Set();
                storeEvent.Set();

                allThreadsDone.WaitOne();

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
            if (fetch.PC >= mem.Instructions.Count)
            {
                fetchRuns = false;
            }
            if (decode.PC >= mem.Instructions.Count)
            {
                decodeRuns = false;
            }
            if (execute.PC >= mem.Instructions.Count)
            {
                executeRuns = false;
            }
            if (store.PC >= mem.Instructions.Count)
            {
                storeRuns = false;
                Dispose();
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
