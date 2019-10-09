using System;
using System.Collections.Generic;
using CardExploration.Interfaces;
using CardExploration.models;

namespace CardExploration.models
{
    public class BasicPolicy : IExplorationPolicy
     {

        public long NumStates {get; set;}
        public long NumActions {get; set;}
        public double Epsilon {get; set;}

        public int ChooseAction(List<int> State, IEnumerable<int> Actions){
            //Random Placeholder
            Random rnd = new Random();
            return rnd.Next(0, 2);
    }

        public void UpdatePolicy(List<int> PastState, List<int> CurrentState, int Action, double Reward){

        }

        public double GetQValue(List<int> State, int Action){
            return 1.0;
        }

    }
}