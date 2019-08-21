using System;
using System.Collections.Generic;
using MLBlackjack.extensions;
using MLBlackjack.models;

namespace CardExploration.models
{

    //Should inherit most of this from an environment class in future
    public class Game
    {
        public long state {get; set;}
        deck GameDeck {get;set;}

        public Game()
        {
            GameDeck = new deck();
        }
        
        public Tuple<Int64,double> ProcessPlayerAction(PlayerAction playerAction)
        {
            switch (playerAction)
            {
                case PlayerAction.hit:
                    //deal new card
                    GameDeck.Cards.Shuffle();
                    //check for bust - return negative reward 

                    break;
                case PlayerAction.stand:
                    //determine the dealer actions and then return whether a win has been achieved and a new state
                    break;
            }
            return new Tuple<long,double>(0,0);
        }

        public static void DealCard()
        {
            
        }
    }
}