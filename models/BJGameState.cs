using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.Interfaces;

namespace CardExploration.models
{
    public class BJGameState : IGameState<card>
    {
        ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        public List<card> PlayerHand { get; set; }
        public List<card> DealerHand { get; set; }
        public Int32 Reward {get; set;}
        private List<List<card>> state;

        public List<List<card>> State {
            get
            {
                this.DealerHand.Sort();
                this.PlayerHand.Sort();
                this.State = new List<List<card>>();
                this.State.Add(DealerHand.Take(1).ToList()); //only first card
                this.State.Add(PlayerHand);
                return state;
            }
            set { state = value; }
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