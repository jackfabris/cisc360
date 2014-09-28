/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void executeInstruction()
        {
            if (PC < mem.Instructions.Count)
            {
                ushort inst = (ushort)(mem.Instructions[PC] >> 9);
                ushort imm = (ushort)((mem.Instructions[PC] & 256) >> 8);
                ushort temp = (ushort)(mem.Instructions[PC] << 8);
                ushort operand = (ushort)(temp >> 8);

                if (imm == 0)
                {
                    if (operand > 255)
                    {
                        MessageBox.Show("Runtime error: segmentation fault at instruction " + PC, 
                            "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                switch (inst)
                {
                    case 1:
                        if (imm == 1)
                        {
                            ACC = operand;
                        }
                        else
                        {
                            ACC = mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 2:
                        mem.memory[operand] = ACC;
                        PC++;
                        break;
                    case 3:
                        if (imm == 1)
                        {
                            ACC += operand;
                        }
                        else
                        {
                            ACC += mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 4:
                        if (imm == 1)
                        {
                            ACC -= operand;
                        }
                        else
                        {
                            ACC -= mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 5:
                        if (imm == 1)
                        {
                            ACC *= operand;
                        }
                        else
                        {
                            ACC *= mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 6:
                        if (imm == 1)
                        {
                            ACC /= operand;
                        }
                        else
                        {
                            ACC /= mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 7:
                        if (imm == 1)
                        {
                            ACC = ACC & operand;
                        }
                        else
                        {
                            ACC = ACC & mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 8:
                        if (imm == 1)
                        {
                            ACC = ACC | operand;
                        }
                        else
                        {
                            ACC = ACC | mem.memory[operand];
                        }
                        PC++;
                        break;
                    case 9:
                        ACC = ACC << operand;
                        PC++;
                        break;
                    case 10:
                        ACC = ~ACC;
                        PC++;
                        break;
                    case 11:
                        PC = operand;
                        break;
                    case 12:
                        if (ACC == 0)
                        {
                            PC = operand;
                        }
                        else
                        {
                            PC++;
                        }
                        break;
                    case 13:
                        Console.WriteLine(ACC);
                        if (ACC < 0)
                        {
                            PC = operand;
                        }
                        else
                        {
                            PC++;
                        }
                        break;
                    case 14:
                        if (ACC > 0)
                        {
                            PC = operand;
                        }
                        else
                        {
                            PC++;
                        }
                        break;
                    case 15:
                        ACC += ZERO;
                        PC++;
                        break;
                    case 16:
                        PC = mem.Instructions.Count;
                        PC++;
                        break;
                    default:
                        break;
                }
            }
        }

        public void executeAllInstructions()
        {
            while (PC < mem.Instructions.Count)
            {
                executeInstruction();
            }
        }

        public string firstInstToString()
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
            return "No Instructions";
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
