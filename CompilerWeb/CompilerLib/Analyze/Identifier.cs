using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Identifier
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public Identifier? Parent { get; set; }
        public List<Identifier> Childs { get; set; }

        public Identifier(string value, string type, int line, int column)
        {
            Value = value;
            Type = type;
            Line = line;
            Column = column;
            Childs = new List<Identifier>();
        }
        public Identifier(string value, string type)
        {
            Value = value;
            Type = type;
            Line = -1;
            Column = -1;
            Childs = new List<Identifier>();
        }
    }
}
