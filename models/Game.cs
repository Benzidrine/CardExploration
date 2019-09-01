using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.Interfaces;

namespace CardExploration.models
{

    //Should inherit most of this from an environment class in future
    public class Game : IEnvironment
    {
        public long state {get; set;}
        deck GameDeck {get;set;}
        List<card> DiscardedCards {get;set;}
        //Num of Decks is also the base number for state here
        int NumOfDecks {get;set;}
        //Hand kept track of here for utility purposes and memory management
        List<card> PlayerHand  {get;set;}
        List<card> DealerHand {get;set;}
        public Game(int numOfDecks)
        {
            NumOfDecks = numOfDecks;
            GameDeck = new deck(NumOfDecks);
        }

        //Reward is passed through depending on context like new round after win or loss or game beginning
        public Tuple<Int64,double> NewRound(double reward)
        {
            ReshuffleIfNeccessary();
            DealInitialCardsToPlayer();
            DealInitialCardsToDealer();
            //Get Player State 
            long Playerstate = Convert.ToInt64(PlayerHand.CardsToLiteralKey(),2);
            //Get Dealer State, unsure of where this goes for now
            int DealerState = DealerHand.First().value;
            return new Tuple<long,double>(Playerstate,reward);
        }

        public Int64 ReturnState()
        {
            return Convert.ToInt64(PlayerHand.CardsToLiteralKey(),2);
        }
        
        public Tuple<Int64,double> Transition(int playerActionInput)
        {
            if (Enum.TryParse(typeof(PlayerAction),playerActionInput.ToString(),true,out var playerAction))
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
                        //Add Card to utility Player Hand
                        PlayerHand.Add(c);
                        //check for bust - return negative reward 
                        if (PlayerHand.BlackjackTotal() > 21 )
                        {
                            return NewRound(-1);
                        }
                        else
                        {
                            return new Tuple<long,double>(Convert.ToInt64(PlayerHand.CardsToLiteralKey(),2),0.1);
                        }
                    case PlayerAction.stand:
                        // TODO: Check for dealer Bust
                        if (DealerHand.BlackjackTotal() >= 17)
                        {
                            //Check if won
                            if (PlayerHand.BlackjackTotal() > DealerHand.BlackjackTotal())
                                return NewRound(1); 
                            //Check for push
                            else if (PlayerHand.BlackjackTotal() == DealerHand.BlackjackTotal())
                                return NewRound(0);
                            //Lost
                            else
                                return NewRound(-1);
                        }
                        else
                        {
                            //Dealer actions hit until 17+ or bust 
                            while (DealerHand.BlackjackTotal() < 17)
                            {
                                //deal new card
                                GameDeck.Cards.Shuffle();
                                //Get top card
                                card dc = GameDeck.Cards.First();
                                //Remove top card 
                                GameDeck.Cards.RemoveAt(0);
                                //Add Card to utility Dealer Hand
                                DealerHand.Add(dc);
                            }

                            //Check for dealer bust or dealer lost
                            if (DealerHand.BlackjackTotal() > 21 || DealerHand.BlackjackTotal() < PlayerHand.BlackjackTotal())
                                return NewRound(1);
                            //Check for push
                            else if (PlayerHand.BlackjackTotal() == DealerHand.BlackjackTotal())
                                return NewRound(0);
                            //Lost
                            else
                                return NewRound(-1);
                        }
                }
            }
            throw new Exception("PayerAction was not handled");
        }

        public void DealInitialCardsToPlayer()
        {
            //Instantiate empty player hand 
            PlayerHand = new List<card>();
            //deal new card`
            GameDeck.Cards.Shuffle();
            //Get top cards
            PlayerHand.AddRange(GameDeck.Cards.Take(2));
            //Remove top cards 
            GameDeck.Cards.RemoveRange(0,2);
        }
        
        public void DealInitialCardsToDealer()
        {
            //Instantiate empty player hand 
            DealerHand = new List<card>();
            //deal new card`
            GameDeck.Cards.Shuffle();
            //Get top cards
            DealerHand.AddRange(GameDeck.Cards.Take(2));
            //Remove top cards 
            GameDeck.Cards.RemoveRange(0,2);
        }
        
        public void ReshuffleIfNeccessary()
        {
            int NumOfCards = NumOfDecks * 52;
            //If a deck has less than 25% capacity it is reshuffled
            if (GameDeck.Cards.Count < (NumOfCards * 0.25))
            {
                GameDeck = new deck(NumOfDecks);
            }
        }
    }
}