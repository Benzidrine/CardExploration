using System.Collections.Generic;
using MLBlackjack.extensions;
using System.Linq;

namespace MLBlackjack.models
{
    public class player
    {
        List<card> hand {get;set;}
        phase phase {get;set;}

        //Create player and deal cards to hand 
        public player(List<card> deck)
        {
            //shuffle deck
            deck.Shuffle();
            //add cards to hand
            hand.AddRange(deck.Take(2));
        }
    }
}