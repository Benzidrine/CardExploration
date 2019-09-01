using System;
using CardExploration.Interfaces;

namespace CardExploration.models
{
    public class BasicPolicy : IExplorationPolicy {

        public long NumStates {get; set;}
        public long NumActions {get; set;}
        public double Epsilon {get; set;}

        public int ChooseAction(long State){
            //Random Placeholder
            Random rnd = new Random();
            return rnd.Next(0, 2);
    }

        public void UpdatePolicy(long PastState, long CurrentState, int Action, double Reward){

        }

        public long GetQValue(long State, int Action){
            return 1;
        }
    }
}