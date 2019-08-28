using System.Collections.Generic;
using MLBlackjack.extensions;
using System.Linq;
using System;
using CardExploration.models;

namespace MLBlackjack.models
{   
    //Should inherit most of this from an agent class in the future
    public class Player
    {
        List<card> hand {get;set;}
        phase phase {get;set;}
        public double score {get; set;}

        //Create player and deal cards to hand 
        public Player()
        {
        }

        //Take State And Reward and send action
        public PlayerAction Receive(Tuple<Int64,double> GameInfo)
        {
            score += GameInfo.Item2;
            return MakeDecision(GameInfo.Item1);
        }

        //Take Action 
        public int MakeDecision(Int64 State)
        {
            //Random Placeholder
            Random rnd = new Random();
            return rnd.Next(0,2);
        }
    }
}