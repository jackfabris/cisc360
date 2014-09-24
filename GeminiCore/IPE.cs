/**
 * Jack Fabris and Ben Handanyan
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class IPE
    {
        public string FileToParse { get; set; }

        public IPE(string filename)
        {
            this.FileToParse = filename;
        }

        public Dictionary<string, int> instBinary = new Dictionary<string, int>()
        {
            { "lda", 1 },
            { "sta", 2 },
            { "add", 3 },
            { "sub", 4 },
            { "mul", 5 },
            { "div", 6 },
            { "and", 7 },
            { "or", 8 },
            { "shl", 9 },
            { "nota", 10 },
            { "ba", 11 },
            { "be", 12 },
            { "bl", 13 },
            { "bg", 14 },
            { "nop", 15 },
            { "hlt", 16 }
        };

        public Memory mem;

        public void ParseFile()
        {
            //List<short> instructions = new List<short>();
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            var lineIndex = 0;
            Dictionary<string, int> labels = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^\s*(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"\s*!(.*)$");
                Regex memInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\$(?<addr>\d*)\s*(?<rest>.*)");
                Regex immInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\#\$(?<imm>\d*)\s*(?<rest>.*)");
                Regex branchInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2}?)\s(?<label>[a-zA-Z]+$?)\s*(?<rest>.*)");
                Regex otherInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{3,}?)\s+(?<rest>.*)");
                Regex otherInstNoCom = new Regex(@"^\s*(?<inst>[a-zA-Z]*?)$");
                var labelStmtMatch = labelStmtFormat.Match(line);
                var memStmtMatch = memInst.Match(line);
                var immStmtMatch = immInst.Match(line);
                var branchStmtMatch = branchInst.Match(line);
                var otherStmtMatch = otherInst.Match(line);
                var otherNoComStmtMatch = otherInstNoCom.Match(line);

                // If the line is empty, move to the next line
                if (emptyLine.Match(line).Success)
                {
                    continue;
                }
                // Labels
                else if (labelStmtMatch.Success)
                {
                    Console.WriteLine("label statement match");
                    var label = labelStmtMatch.Groups["label"].Value;
                    if (labels.ContainsKey(label))
                    {
                        labels[label] = lineIndex + 1;
                    }
                    else
                    {
                        labels.Add(label, lineIndex + 1);
                    }
                }
                // Memory instruction
                else if (memStmtMatch.Success)
                {
                    Console.WriteLine("memory match");
                    var minst = memStmtMatch.Groups["inst"].Value;
                    var addr = memStmtMatch.Groups["addr"].Value;
                    var rest = memStmtMatch.Groups["rest"].Value;
                    Console.WriteLine("rest: " + rest);
                    if (rest.Length == 0 || rest[0] == '!')
                    {
                        lineIndex++;
                        string[] arr = { minst, addr };
                        mem.Instructions.Add(binaryEncode(arr));
                    }
                    else
                    {
                        Console.WriteLine("Invalid memory instruction");
                    }

                }
                // Immediate Instruction
                else if (immStmtMatch.Success)
                {
                    Console.WriteLine("immediate match");
                    var inst = immStmtMatch.Groups["inst"].Value;
                    var imm = immStmtMatch.Groups["imm"].Value;
                    var rest = immStmtMatch.Groups["rest"].Value;
                    Console.WriteLine("rest: " + rest);
                    if (rest.Length == 0 || rest[0] == '!')
                    {
                        lineIndex++;
                        string[] arr = { inst, imm, "" };
                        mem.Instructions.Add(binaryEncode(arr));
                    }
                    else
                    {
                        Console.WriteLine("invalid immediate instruction");
                    }
                }
                // Branch instruction
                else if (branchStmtMatch.Success)
                {
                    Console.WriteLine("branch match");
                    lineIndex++;
                }
                // Other statement w/ comment (nop, nota, hlt)
                else if (otherStmtMatch.Success)
                {
                    Console.WriteLine("other match");
                    var inst = otherStmtMatch.Groups["inst"].Value;
                    var rest = otherStmtMatch.Groups["rest"].Value;
                    Console.WriteLine("rest: " + rest);
                    if (rest.Length == 0 || rest[0] == '!')
                    {
                        lineIndex++;
                        string[] arr = { inst };
                        mem.Instructions.Add(binaryEncode(arr));
                    }
                    else
                    {
                        Console.WriteLine("invalid other instruction");
                    }
                }
                // Other statement w/o comment
                else if (otherNoComStmtMatch.Success)
                {
                    Console.WriteLine("other match");
                    var inst = otherNoComStmtMatch.Groups["inst"].Value;
                    lineIndex++;
                    string[] arr = { inst };
                    mem.Instructions.Add(binaryEncode(arr));
                }
            }
            var index = 0;
            for (var line = 0; line < lines.Count; line++) {
                Regex labelStmtFormat = new Regex(@"^\s*(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"\s*!(.*)$");
                Regex memInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\$(?<addr>\d*)\s*(?<rest>.*)");
                Regex immInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\#\$(?<imm>\d*)\s*(?<rest>.*)");
                Regex branchInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2}?)\s(?<label>[a-zA-Z]+$?)\s*(?<rest>.*)");
                Regex otherInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{3,}?)\s+(?<rest>.*)");
                Regex otherInstNoCom = new Regex(@"^\s*(?<inst>[a-zA-Z]*?)$");
                var labelStmtMatch = labelStmtFormat.Match(lines[line]);
                var memStmtMatch = memInst.Match(lines[line]);
                var immStmtMatch = immInst.Match(lines[line]);
                var branchStmtMatch = branchInst.Match(lines[line]);
                var otherStmtMatch = otherInst.Match(lines[line]);
                var otherNoComStmtMatch = otherInstNoCom.Match(lines[line]);
                if (memStmtMatch.Success || immStmtMatch.Success || otherStmtMatch.Success || otherNoComStmtMatch.Success)
                {
                    index++;
                }
                else if (branchStmtMatch.Success)
                {
                    var inst = branchStmtMatch.Groups["inst"].Value;
                    var labelIndex = 0;
                    var label = branchStmtMatch.Groups["label"].Value;
                    if(labels.ContainsKey(label)) {
                        labelIndex = labels[label];
                    }
                    string[] arr = { inst, labelIndex.ToString() };
                    mem.Instructions.Insert(index - 1,binaryEncode(arr));
                    index++;
                }
            }
            // write to file
            FileStream fs = new FileStream(@"C:\Users\Jack\Documents\College\14F\CISC360\g.out", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            foreach (short x in mem.Instructions)
            {
                bw.Write(x);
                Console.WriteLine(x);
            }
            bw.Close();

            // read binary
            Console.WriteLine("reading binary file");
            using (BinaryReader br = new BinaryReader(File.Open(@"C:\Users\Jack\Documents\College\14F\CISC360\g.out", FileMode.Open)))
            {
                int pos = 0;
                int length = (int)br.BaseStream.Length;
                while (pos < length)
                {
                    int v = br.ReadInt16();
                    Console.WriteLine("instruction: " + v);

                    ushort inst = (ushort)(v >> 9);
                    ushort imm = (ushort)((v & 256) >> 8);
                    ushort temp = (ushort)(v << 8);
                    ushort operand = (ushort)(temp >> 8);
                    Console.WriteLine("inst: " + inst);
                    Console.WriteLine("imm: " + imm);
                    Console.WriteLine("operand: " + operand);

                    pos += sizeof(short);
                }
            }
        }

        public ushort binaryEncode(string[] arr)
        {
            ushort result = 0;
            if (arr.Length > 2) //Immediate flag is on
            {
                if (instBinary.ContainsKey(arr[0]))
                {
                    int inst = instBinary[arr[0]];
                    int imm = 256;
                    int operand = Convert.ToInt32(arr[1]);
                    result = (ushort)inst;
                    result = (ushort)(result << 9);
                    result = (ushort)(result | imm); 
                    result = (ushort)(result | operand);
                }
            }
            else if (arr.Length > 1)
            {
                {
                    if (instBinary.ContainsKey(arr[0]))
                    {
                        int inst = instBinary[arr[0]];
                        int imm = 0;
                        int operand = Convert.ToInt32(arr[1]);
                        result = (ushort)inst;
                        result = (ushort)(result << 9);
                        result = (ushort)(result | imm);
                        result = (ushort)(result | operand);
                    }
                }
            }
            else
            {
                if (instBinary.ContainsKey(arr[0]))
                {
                    int inst = instBinary[arr[0]];
                    result = (ushort)inst;
                    result = (ushort)(result << 9);
                }
            }
            return result;
        }
    }
}
