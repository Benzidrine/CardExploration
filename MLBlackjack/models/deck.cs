using System.Collections.Generic;

namespace CardExploration.models
{
    /// The state that the agent stores is the history of cards revealed by the dealer Store the as a vector 1..11 where an ace can be moved from 1->11 at will
    /// The agent is expected to identify the 21 sum rule total we don't tell it Its only feedback is the amount of money it currently has i.e reward

    /// The Deck class contains methods and states necessary to model drawing cards from a fair deck containing only 52 cards (no jockers) with black jack values.

    public class deck
    {
        public List<card> Cards {get;set;}
        
        public int id = 0;

        public deck(int NumOfDecks)
        {
            // Instantiate ID to add to each card incrementally
            // Create 52 card deck
            Cards = new List<card>();
            for (int i = 0; i < NumOfDecks; i++)
            {
                CreateDeck();
            }
        }

        public void CreateDeck()
        {
            for (int j = 1; j < 5; j++)
            {
                for (int i = 1; i < 14; i++)
                {
                    id++;
                    Cards.Add(new card(id,i,j));
                }
            }
        }
    }
}
