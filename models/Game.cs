using System;
using System.Collections.Generic;
using System.Linq;
using MLBlackjack.extensions;
using MLBlackjack.models;

namespace CardExploration.models
{

    //Should inherit most of this from an environment class in future
    public class Game
    {
        public long state {get; set;}
        deck GameDeck {get;set;}
        List<card> DiscardedCards {get;set;}
        //Num of Decks is also the base number for state here
        int NumOfDecks {get;set;}
        //Hand kept track of here for utility purposes only
        List<card> PlayerHand  {get;set;}
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
                    //Get top card
                    card c = GameDeck.Cards.First();
                    //Remove top card 
                    GameDeck.Cards.RemoveAt(0);
                    //Add Card to state
                    state = extensions.AddCardToState(state,c,NumOfDecks);
                    //Add Card to utility Player Hand
                    PlayerHand.Add(c);
                    //check for bust - return negative reward 
                    if (extensions.StateNumberToCardList(NumOfDecks,state).BlackjackTotal() > 21 )
                    {
                        //Discard Player Hand
                        DiscardedCards.AddRange(PlayerHand);
                        PlayerHand = new List<card>();
                        //if cards less than 75% then shuffle in discarded cards
                    }
                    else
                    {

                    }
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