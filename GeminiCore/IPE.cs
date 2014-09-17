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

        //public string[] 

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            var lineIndex = 0;

            Dictionary<string, int> labels = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^\s*(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"\s*!(.*)$");
                Regex memInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\$(?<addr>\d*)");
                Regex immInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}?)\s\#\$(?<imm>\d*)");
                Regex branchInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2}?)\s(?<label>[a-zA-Z]+$?)");
                Regex otherInst = new Regex(@"^\s*(?<inst>[a-zA-Z]{2,4}$?)");
                var labelStmtMatch = labelStmtFormat.Match(line);
                var memStmtMatch = memInst.Match(line);
                var immStmtMatch = immInst.Match(line);
                var branchStmtMatch = branchInst.Match(line);
                var otherStmtMatch = otherInst.Match(line);

                // If the line is empty, move to the next line
                if (emptyLine.Match(line).Success)
                {
                    continue;
                }
                // Labels
                else if (labelStmtMatch.Success)
                {
                    var label = labelStmtMatch.Groups["label"].Value;
                    if (labels.ContainsKey(label))
                    {
                        labels[label] = lineIndex;
                    }
                    else
                    {
                        labels.Add(label, lineIndex);
                    }
                }
                // Memory instruction
                else if (memStmtMatch.Success)
                {
                    var minst = memStmtMatch.Groups["inst"].Value;
                    var addr = memStmtMatch.Groups["addr"].Value;
                    lineIndex++;
                    string[] arr = { minst, addr };
                    Console.WriteLine(binaryEncode(arr));
                    //Console.WriteLine("Mem match: " + minst + " " + addr);
                }
                // Immediate Instruction
                else if (immStmtMatch.Success)
                {
                    var inst = immStmtMatch.Groups["inst"].Value;
                    var imm = immStmtMatch.Groups["imm"].Value;
                    lineIndex++;
                    //Console.WriteLine("Imm match: " + inst + " " + imm);
                    string[] arr = { inst, imm, "" };
                    Console.WriteLine(binaryEncode(arr));
                }
                // Branch instruction
                else if (branchStmtMatch.Success)
                {
                    var inst = branchStmtMatch.Groups["inst"].Value;
                    var label = branchStmtMatch.Groups["label"].Value;
                    lineIndex++;
                    //Console.WriteLine("Branch match: " + inst + " " + label);
                }
                // Other statement (nop, nota, hlt)
                else if (otherStmtMatch.Success)
                {
                    var inst = otherStmtMatch.Groups["inst"].Value;
                    lineIndex++;
                   // Console.WriteLine("Other match: " + inst);
                }
                //if (comment.Match(line).Success)
                //{
                //    Console.WriteLine(line);
                //}
            }
        }

        public short binaryEncode(string[] arr)
        {
            short result = 0;
            if (arr.Length > 2) //Immediate flag is on
            {
                if (instBinary.ContainsKey(arr[0]))
                {
                    int inst = instBinary[arr[0]];
                    int imm = 1;
                    int operand = Convert.ToInt32(arr[1]);
                    result = (short)inst;
                    result = (short)(result << 9);
                    result = (short)(result | imm); 
                    result = (short)(result | operand);
                    Console.WriteLine("result: " + result);

                }
            }
            else
            {
                if (instBinary.ContainsKey(arr[0]))
                {
                    int inst = instBinary[arr[0]];
                    int imm = 0;
                    int operand = Convert.ToInt32(arr[1]);
                    result = (short)inst;
                    result = (short)(result << 9);
                    result = (short)(result | imm);
                    result = (short)(result | operand);
                    Console.WriteLine("result: " + result);
                }
            }
            return result;
        }
    }
}
