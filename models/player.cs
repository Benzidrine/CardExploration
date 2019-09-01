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

        public int Receive(long State, double Reward)
        {
            int Action = MakeDecision(State);
            UpdateState(State, Action, Reward); 
            return Action;
        }
        private int MakeDecision(long State)
        {
            return ExplorationPolicy.ChooseAction(State);
        }
    }
}