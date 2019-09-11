using CardExploration.models;
using System;
using System.Collections.Generic;
using CardExploration.Interfaces;

namespace CardExploration.models
{
    //inherits most of its methods from is parent only impliments methods pertaining specific to the kind of game
    public class Player : Agent
    {
        List<card> Hand { get; set; }
        Phase Phase { get; set; }
        public double Reward { get; private set; }

        public Player(IExplorationPolicy ExplorationPolicy) : base(ExplorationPolicy) { }

        public int Receive(long State, Enum Actions, double Reward)
        {
            int Action = MakeDecision(State, Actions);
            UpdateState(State, Action, Reward); 
            return Action;
        }
        private int MakeDecision(long State, Enum Actions)
        {
            return ExplorationPolicy.ChooseAction(State, Actions);
        }
    }
}