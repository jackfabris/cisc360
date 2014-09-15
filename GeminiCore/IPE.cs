/**
 * Seth Morecraft
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

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            var count = 0;
            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"!(.*)$");
                Regex memInst = new Regex(@"[a-zA-Z]{2,4}\s\$\d*");
                Regex immInst = new Regex(@"[a-zA-Z]{2,4}\s\#\$\d*");
                var emptyMatch = emptyLine.Match(line);
                var labelStmtMatch = labelStmtFormat.Match(line);
                if (memInst.Match(line).Success)
                {
                    Console.WriteLine(line);
                }
                if (comment.Match(line).Success)
                {
                    count++;
                    Console.WriteLine(line);
                    comment.Replace(line, "");
                    Console.WriteLine(line);
                }
                //if (labelStmtMatch.Success)
                //{
                //   var label = labelStmtMatch.Groups["label"].Value;
                //   Console.WriteLine(label);
                //}
            }
            //Console.WriteLine(count);
        }
    }
}
