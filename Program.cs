using System;
using System.Linq;
using CardExploration.models;
using CardExploration.extensions;


namespace CardExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            deck Deck = new deck(1);
            BasicPolicy Policy = new BasicPolicy();

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

            // From unique value back to list
            Deck.Cards = extensions.extensions.StateNumberToCardList(1,Convert.ToInt64(Deck.Cards.CardsToLiteralKey(),2));

            // Demonstrate Key Literal with new list
            Console.WriteLine(Deck.Cards.CardsToLiteralKey());

            // Test Game Logic
            
            // Single deck game instance
            Game game = new Game(1);
            Tuple<long, double> initial = game.NewRound(0);
            Player player = new Player(Policy);

            for (int i = 0; i < 1000000; i++)
            {
                initial = game.Transition(player.Receive(initial.Item1, initial.Item2));
            }

            //Player Score
            Console.WriteLine(player.Reward.ToString());

            ///  
            ///    |----Agent------|
            ///    |               |
            ///    |--Environment--|
            ///
            ///    Passing State and Reward to Agent in return for Action Passed to Environment
        }
    }
} 
