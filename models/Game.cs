using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.Interfaces;

namespace CardExploration.models
{

    //Should inherit most of this from an environment class in future
    public class Game : IEnvironment<card>
    {
        public BJGameState GameState {get; set;}
        deck GameDeck {get;set;}
        List<card> DiscardedCards {get;set;}
        //Num of Decks is also the base number for state here
        int NumOfDecks {get;set;}
    
        public Game(int numOfDecks)
        {
            NumOfDecks = numOfDecks;
            GameDeck = new deck(NumOfDecks);
            GameState = new BJGameState(1000);

        }

        //Reward is passed through depending on context like new round after win or loss or game beginning
        public IGameState<card> NewRound(Int32 Reward)
        {
            ReshuffleIfNeccessary();
            DealInitialCardsToPlayer();
            DealInitialCardsToDealer();
            GameState.Reward += Reward;
            return GameState;
        }

        public Int64 ReturnState()
        {
            return Convert.ToInt64(GameState.PlayerHand.CardsToLiteralKey(),2);
        }
        
        public IGameState<card> Transition(int playerActionInput)
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
                        GameState.PlayerHand.Add(c);
                        //check for bust - return negative reward 
                        if (GameState.PlayerHand.BlackjackTotal() > 21 )
                        {
                            return NewRound(-1);
        
                        }
                        else
                        {
                            return  GameState;
                        }
                    case PlayerAction.stand:
                        // TODO: Check for dealer Bust
                        if (GameState.DealerHand.BlackjackTotal() >= 17)
                        {
                            //Check if won
                            if (GameState.PlayerHand.BlackjackTotal() > GameState.DealerHand.BlackjackTotal())
                                return NewRound(1); 
                            //Check for push
                            else if (GameState.PlayerHand.BlackjackTotal() == GameState.DealerHand.BlackjackTotal())
                                return NewRound(0);
                            //Lost
                            else
                                return NewRound(-1);
                        }
                        else
                        {
                            //Dealer actions hit until 17+ or bust 
                            while (GameState.DealerHand.BlackjackTotal() < 17)
                            {
                                //deal new card
                                GameDeck.Cards.Shuffle();
                                //Get top card
                                card dc = GameDeck.Cards.First();
                                //Remove top card 
                                GameDeck.Cards.RemoveAt(0);
                                //Add Card to utility Dealer Hand
                                GameState.DealerHand.Add(dc);
                            }

                            //Check for dealer bust or dealer lost
                            if (GameState.DealerHand.BlackjackTotal() > 21 || GameState.DealerHand.BlackjackTotal() < GameState.PlayerHand.BlackjackTotal())
                                return NewRound(1);
                            //Check for push
                            else if (GameState.PlayerHand.BlackjackTotal() == GameState.DealerHand.BlackjackTotal())
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
            GameState.PlayerHand = new List<card>();
            //deal new card`
            GameDeck.Cards.Shuffle();
            //Get top cards
            GameState.PlayerHand.AddRange(GameDeck.Cards.Take(2));
            //Remove top cards 
            GameDeck.Cards.RemoveRange(0,2);
        }
        
        public void DealInitialCardsToDealer()
        {
            //Instantiate empty player hand 
            GameState.DealerHand = new List<card>();
            //deal new card`
            GameDeck.Cards.Shuffle();
            //Get top cards
            GameState.DealerHand.AddRange(GameDeck.Cards.Take(2));
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