using System;
using CardExploration.Interfaces;
using CardExploration.models;

namespace CardExploration.models
{
    public class BasicPolicy : IExplorationPolicy {

        public long NumStates {get; set;}
        public long NumActions {get; set;}
        public double Epsilon {get; set;}

        public int ChooseAction(long State, Enum Actions){
            //Random Placeholder
            Random rnd = new Random();
            return rnd.Next(0, 2);
    }

        public void UpdatePolicy(long PastState, long CurrentState, int Action, double Reward){

        }

        public long GetQValue(long State, int Action){
            return 1;
        }

        double IExplorationPolicy.GetQValue(long State, int Action)
        {
            throw new NotImplementedException();
        }
    }
}