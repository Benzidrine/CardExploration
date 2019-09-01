using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;

namespace CardExploration.models
{
    public class BJGameState : IGameState
    {
                ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        public List<card> PlayerHand {get;set;}
        public List<card> DealerHand {get;set;}
        public double Reward {get; set;}
        public long State {get; set;}
        public BJGameState() {
            this.PlayerHand = new List<card>();
            this.DealerHand = new List<card>();
            this.Reward = 0.0;
            UpdateState();
        }

        public BJGameState(List<card> PlayerHand, List<card> DealerHand, double Reward) {
            this.PlayerHand = PlayerHand;
            this.DealerHand = DealerHand;
            this.Reward = Reward;
            UpdateState();
        }

        void UpdateState(){
            //TODO need to incorporate the dealers hand into the state space
            State = Convert.ToInt64(PlayerHand.CardsToLiteralKey(),2); 
        }
    }
}