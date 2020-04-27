using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace SimpleLang
{
    
    public class ThreeAddressCodeOptimizer
    {       
        public List<Instruction> Run(List<Instruction> originalList)
        {
            List<Optimizer> ListOfOptimizers = new List<Optimizer>();
            ListOfOptimizers.Add(new ThreeAddressCodeFoldConstants());
            //ListOfOptimizers.Add(new ThreeAddressCodeRemoveNoop());
            //ListOfOptimizers.Add(new ThreeAddressCodeDefUse());
            //ListOfOptimizers.Add(new ThreeAddressCodeRemoveGotoThroughGoto());
            ListOfOptimizers.Add(new ThreeAddressCodeRemoveAlgebraicIdentities());
            //ListOfOptimizers.Add(new DeleteDeadCodeWithDeadVars());
            

            var instructions = originalList;
            int currentOpt = 0, enabledOpt = 0;

            while (enabledOpt < ListOfOptimizers.Count) {
                while (currentOpt <= enabledOpt) {
                    ListOfOptimizers[currentOpt].Optimize(instructions);                    
                    if (ListOfOptimizers[currentOpt].Changed)
                    {
                        instructions = ListOfOptimizers[currentOpt].ListOfInstructions;
                        currentOpt = 0;
                    }
                    else 
                        currentOpt++;                    
                }
                enabledOpt = enabledOpt + 1;
            }
            return instructions;
        }
    }
}
