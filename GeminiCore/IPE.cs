﻿/**
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
            { "LDA", 1111 },
            { "STA", 1110 },
            { "ADD", 1101 },
            { "SUB", 1100 },
            { "MUL", 1011 },
            { "DIV", 1010 },
            { "AND", 1001 },
            { "OR", 1000 },
            { "SHL", 0111 },
            { "NOTA", 0110 },
            { "BA", 0101 },
            { "BE", 0100 },
            { "BL", 0011 },
            { "BG", 0010 },
            { "NOP", 0001 },
            { "HLT", 0000 }
        };

        //public string[] 

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            var lineIndex = 0;

            Dictionary<string, int> labels = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"!(.*)$");
                Regex memInst = new Regex(@"(?<inst>[a-zA-Z]{2,4}?)\s\$(?<addr>\d*)");
                Regex immInst = new Regex(@"(?<inst>[a-zA-Z]{2,4}?)\s\#\$(?<imm>\d*)");
                var labelStmtMatch = labelStmtFormat.Match(line);
                var memStmtMatch = memInst.Match(line);
                var immStmtMatch = immInst.Match(line);
                // If the line is empty, move to the next line
                if (emptyLine.Match(line).Success)
                {
                    continue;
                }

                // Labels
                if (labelStmtMatch.Success)
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

                if (memStmtMatch.Success)
                {
                    var minst = memStmtMatch.Groups["inst"].Value;
                    var addr = memStmtMatch.Groups["addr"].Value;
                    Console.WriteLine("Mem match: " + minst + " " + addr);
                }
                if (immStmtMatch.Success)
                {
                    var inst = immStmtMatch.Groups["inst"].Value;
                    var imm = immStmtMatch.Groups["imm"].Value;
                    Console.WriteLine("Imm match: " + inst + " " + imm);
                }
                //if (comment.Match(line).Success)
                //{
                //    count++;
                //    Console.WriteLine(line);
                //    comment.Replace(line, "");
                //    Console.WriteLine(line);
                //}

                lineIndex++;
            }
            //Console.WriteLine(count);
        }
    }
}
