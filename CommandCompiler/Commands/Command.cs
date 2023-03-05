using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCompiler.Commands
{
    public class Command
    {
        public string CommandName { get; }
        public string ShortCommandDescription { get; }
        public string CommandDescription { get; }
        public string Pattern { get; }

        public Command(string name, string decription, string shortDescription, string pattern)
        {
            CommandName = name;
            CommandDescription = decription;
            ShortCommandDescription = shortDescription;
            Pattern = pattern;
        }
    }
}
