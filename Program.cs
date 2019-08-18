using System;
using System.Linq;
using MLBlackjack.models;
using MLBlackjack.extensions;

namespace MLBlackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            deck Deck = new deck();

            Console.ReadLine();
            // Demonstrate Card Total
            Console.WriteLine(Deck.Cards.CardTotal().ToString());
            
            Deck.Cards.RemoveAt(51);
            Deck.Cards.RemoveAt(13);
            Deck.Cards.RemoveAt(4);

            // Demonstrate Key Literal
            Console.WriteLine(Deck.Cards.CardsToLiteralKey());

            // Key Literal to unique value
            Console.WriteLine(Convert.ToInt64(Deck.Cards.CardsToLiteralKey(),2));

            
        }
    }
} 
