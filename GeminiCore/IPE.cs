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

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();
            var count = 0;
            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*?)\s*:$");
                Regex emptyLine = new Regex(@"^\s*$");
                Regex comment = new Regex(@"!(.*)$");
                var emptyMatch = emptyLine.Match(line);
                var labelStmtMatch = labelStmtFormat.Match(line);
                if (comment.Match(line).Success) { 
                    count++;
                    Console.WriteLine(line);
                    comment.Replace(line, "");
                    Console.WriteLine(line);
                }
                if (labelStmtMatch.Success)
                {
                   var label = labelStmtMatch.Groups["label"].Value;
                   Console.WriteLine(label);
                }
            }
            Console.WriteLine(count);
        }
    }
}
