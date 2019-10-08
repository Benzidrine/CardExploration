using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using CardExploration.extensions;
using CardExploration.Interfaces;

namespace CardExploration.models
{
    ///<summery>
    ///Stores aspects of the game state including player hand, dealer hand and  current reward
    ///<summery>
    public class BJGameState : IGameState<card>
    {

        public List<card> PlayerHand { get; set; }
        public List<card> DealerHand { get; set; }
        public Int32 Reward {get; set;}

        public List<int> GetState() {
                this.PlayerHand.Sort();
                List<int> State = new List<int>();
                State.Add(DealerHand.First().value); //only first card
                State.AddRange(PlayerHand.ConvertAll(card=>card.value));
                return State;

        }
        public BJGameState() {
            this.PlayerHand = new List<card>();
            this.DealerHand = new List<card>();
            this.Reward = 0;
        }

        public BJGameState(Int32 Reward) {
            this.PlayerHand = new List<card>();
            this.DealerHand = new List<card>();
            this.Reward = Reward;
        }

        public BJGameState(List<card> PlayerHand, List<card> DealerHand, Int32 Reward) {
            this.PlayerHand = PlayerHand;
            this.DealerHand = DealerHand;
            this.Reward = Reward;
        }



    }
}