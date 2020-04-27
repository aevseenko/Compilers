using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLang
{
    public abstract class Optimizer
    {
        public bool Changed { get; set; }
        public List<Instruction> ListOfInstructions { get; set; }
        public abstract void Optimize(List<Instruction> instructions);
    }
}
