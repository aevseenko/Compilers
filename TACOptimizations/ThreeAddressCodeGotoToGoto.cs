using System.Collections.Generic;
using System.Linq;

namespace SimpleLang
{
    public class ThreeAdressCodeGotoToGoto : Optimizer
    {

        public struct GtotScaner
        {
            public int index;
            public string label;
            public string labelfrom;

            public GtotScaner(int index, string label, string labelfrom)
            {
                this.index = index;
                this.label = label;
                this.labelfrom = labelfrom;
            }
        }

        public override void Optimize(List<Instruction> commands)
        {
            ListOfInstructions = commands.ToList();
            Changed = false;
            List<GtotScaner> list = new List<GtotScaner>();
            for (int i = 0; i < ListOfInstructions.Count; i++)
            {
                if (ListOfInstructions[i].Operation == "goto")
                {
                    list.Add(new GtotScaner(i, ListOfInstructions[i].Label, ListOfInstructions[i].Argument1));
                }
            }

            for (int i = 0; i < commands.Count; i++)
            {
                if (ListOfInstructions[i].Operation == "goto")
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j].label == ListOfInstructions[i].Argument1)
                        {
                            ListOfInstructions[i] = new Instruction("", "goto", list[j].labelfrom.ToString(), "", "");
                            Changed = true;
                        }
                    }
                }
            }
        }
    }
}
