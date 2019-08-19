using System.Collections.Generic;
using MLBlackjack.extensions;
using System.Linq;
using System;
using CardExploration.models;

namespace MLBlackjack.models
{   
    //Should inherit most of this from an agent class in the future
    public class player
    {
        List<card> hand {get;set;}
        phase phase {get;set;}
        double score {get; set;}

        //Create player and deal cards to hand 
        public player(List<card> deck)
        {
            //shuffle deck
            deck.Shuffle();
            //add cards to hand
            hand.AddRange(deck.Take(2));
        }

        //Take State And Reward and send action
        public PlayerAction Receive(Int64 State, double reward)
        {
            score += reward;
            return MakeDecision(State);
        }

        //Take Action 
        public PlayerAction MakeDecision(Int64 State)
        {
            //Random Placeholder
            Random rnd = new Random();
            switch(rnd.Next(0,2))
            {
                case 0:
                    return PlayerAction.stand;
                case 1:
                    return PlayerAction.hit;
                default:
                    return PlayerAction.stand;
            }
            
        }
    }
}