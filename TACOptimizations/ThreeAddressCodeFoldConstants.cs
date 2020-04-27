﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLang
{
    public class ThreeAddressCodeFoldConstants : Optimizer    
    {
        public override void Optimize(List<Instruction> instructions)
        {
            Changed = false;
            ListOfInstructions = new List<Instruction>();
            for (int i = 0; i < instructions.Count; ++i)
            {
                if (instructions[i].Argument2 != "")
                    if (int.TryParse(instructions[i].Argument1, out var intArg1) && int.TryParse(instructions[i].Argument2, out var intArg2))
                    {
                        var constant = CalculateConstant(instructions[i].Operation, intArg1, intArg2);
                        ListOfInstructions.Add(new Instruction(instructions[i].Label, "assign", constant, "", instructions[i].Result));
                        Changed = true;
                        continue;
                    }
                    else if (bool.TryParse(instructions[i].Argument1, out var boolArg1) && bool.TryParse(instructions[i].Argument2, out var boolArg2))
                    {
                        var constant = CalculateConstant(instructions[i].Operation, boolArg1, boolArg2);
                        ListOfInstructions.Add(new Instruction(instructions[i].Label, "assign", constant, "", instructions[i].Result));
                        Changed = true;
                        continue;
                    }
                ListOfInstructions.Add(instructions[i]);
            }           
        }

        private static string CalculateConstant(string operation, bool boolArg1, bool boolArg2)
        {
            switch (operation)
            {
                case "OR":
                    return (boolArg1 || boolArg2).ToString();
                case "AND":
                    return (boolArg1 && boolArg2).ToString();
                case "EQUAL":
                    return (boolArg1 == boolArg2).ToString();
                case "NOTEQUAL":
                    return (boolArg1 != boolArg2).ToString();
                default:
                    throw new InvalidOperationException();
            }
        }

        private static string CalculateConstant(string operation, int intArg1, int intArg2)
        {
            switch (operation)
            {
                case "EQUAL":
                    return (intArg1 == intArg2).ToString();
                case "NOTEQUAL":
                    return (intArg1 != intArg2).ToString();
                case "LESS":
                    return (intArg1 < intArg2).ToString();
                case "GREATER":
                    return (intArg1 > intArg2).ToString();
                case "EQGREATER":
                    return (intArg1 >= intArg2).ToString();
                case "EQLESS":
                    return (intArg1 <= intArg2).ToString();
                case "PLUS":
                    return (intArg1 + intArg2).ToString();
                case "MINUS":
                    return (intArg1 - intArg2).ToString();
                case "MULT":
                    return (intArg1 * intArg2).ToString();
                case "DIV":
                    return (intArg1 / intArg2).ToString();
                default:
                    throw new InvalidOperationException();
            }
        }        
    }
}
