using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexemsAnalyzer
{
    public class Identifier
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public Identifier(string value, string type, int line, int column)
        {
            Value = value;
            Type = type;
            Line = line;
            Column = column;
        }
        public Identifier(string value, string type)
        {
            Value = value;
            Type = type;
            Line = -1;
            Column = -1;
        }
    }
}
