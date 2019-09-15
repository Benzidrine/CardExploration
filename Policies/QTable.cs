using System;
using System.Collections.Generic;
using System.Linq;

namespace CardExploration.Policies
{
    public class ActionTable
    {
        public List<int> Actions {get; set;}
        public List<double> Values {get; set;}

        public int MaxValueArg {
            get {
                return Values.IndexOf(this.Values.Max());;
            }

        }
        public int Max(){
            return Actions[MaxValueArg];
        }
    }
}