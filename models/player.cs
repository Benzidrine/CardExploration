using CardExploration.models;
using System;
using System.Collections.Generic;

namespace MLBlackjack.models
{
    //inherits most of its methods from is parent only impliments methods pertaining specific to the kind of game
    public class Player : Agent
    {
        List<card> Hand { get; set; }
        Phase Phase { get; set; }
        public Player(IExplorationPolicy ExplorationPolicy) : base(ExplorationPolicy) { }

        public PlayerAction Receive(long State, double Reward)
        {
            int Action = MakeDecision(State);
            UpdateState(State, Action, Reward);
            return (PlayerAction)Action;
        }
        private int MakeDecision(long State)
        {
            //Random Placeholder
            Random rnd = new Random();
            return rnd.Next(0, 2);
        }
    }
}