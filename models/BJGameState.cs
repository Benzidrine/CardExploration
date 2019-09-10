using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.Interfaces;

namespace CardExploration.models
{
    public class BJGameState : IGameState
    {
        ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        public List<card> PlayerHand { get; set; }
        public List<card> DealerHand { get; set; }
        public Int32 Reward {get; set;}
        private long state;

        public long State {
            get
            {
                this.DealerHand.Sort();
                this.PlayerHand.Sort();
                this.State = long.Parse(string.Concat(DealerHand) + string.Concat(PlayerHand));
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

        void UpdateState(){
            this.DealerHand.Sort();
            this.PlayerHand.Sort();
            this.State = long.Parse(string.Concat(DealerHand) + string.Concat(PlayerHand));
        }

    }
}