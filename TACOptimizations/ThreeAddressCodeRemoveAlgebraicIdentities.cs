using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLang
{
    //Instruction(string label, string operation, string argument1, string argument2, string result)
    public class ThreeAddressCodeRemoveAlgebraicIdentities : Optimizer
    {
        public override void Optimize(List<Instruction> commands)
        {
            ListOfInstructions = new List<Instruction>();
            Changed = false;          

            for (int i = 0; i < commands.Count; i++)
            {
                //a - a == 0
                bool b;
                bool variablesAreNotBool = !bool.TryParse(commands[i].Argument1, out b) && !bool.TryParse(commands[i].Argument2, out b);
                if (variablesAreNotBool && commands[i].Argument1 == commands[i].Argument2 && commands[i].Operation == "MINUS")
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", "0", "", commands[i].Result));
                    Changed = true;
                    continue;
                }

                //Умножение на 1
                double arg1, arg2;
                bool arg1IsNumber = double.TryParse(commands[i].Argument1, out arg1);
                if (commands[i].Operation == "MULT" && variablesAreNotBool && arg1IsNumber && arg1 == 1)
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", commands[i].Argument2, "", commands[i].Result));
                    Changed = true;
                    continue;
                }
                bool arg2IsNumber = double.TryParse(commands[i].Argument2, out arg2);
                if (commands[i].Operation == "MULT" && variablesAreNotBool && arg2IsNumber && arg2 == 1)
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", commands[i].Argument1, "", commands[i].Result));
                    Changed = true;
                    continue;
                }

                //Суммирование и вычитание с 0                
                if ((commands[i].Operation == "PLUS" || commands[i].Operation == "MINUS") && variablesAreNotBool && arg1IsNumber && arg1 == 0)
                {
                    var sign = commands[i].Operation == "PLUS" ? "" : "-";
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", sign + commands[i].Argument2, "", commands[i].Result));
                    Changed = true;
                    continue;
                }
                if ((commands[i].Operation == "PLUS" || commands[i].Operation == "MINUS") && variablesAreNotBool && arg2IsNumber && arg2 == 0)
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", commands[i].Argument1, "", commands[i].Result));
                    Changed = true;
                    continue;
                }
                
                //Умножение на 0
                if (commands[i].Operation == "MULT" && variablesAreNotBool && (arg1IsNumber && arg1 == 0 || arg2IsNumber && arg2 == 0))
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", "0", "", commands[i].Result));
                    Changed = true;
                    continue;
                }

                //0 Делить на !0
                if (commands[i].Operation == "DIV" && variablesAreNotBool && arg1IsNumber && arg1 == 0 && (arg2IsNumber && arg2 != 0 || !arg2IsNumber))
                {
                    ListOfInstructions.Add(new Instruction(commands[i].Label, "assign", "0", "", commands[i].Result));
                    Changed = true;
                    continue;
                }
                ListOfInstructions.Add(commands[i]);
            }            
        }
    }
}