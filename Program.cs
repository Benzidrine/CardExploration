using System;
using System.Linq;
using CardExploration.models;
using CardExploration.extensions;
using System.IO;
using CardExploration.DatabaseContext;
using System.Threading.Tasks;
using CardExploration.Manager;

namespace CardExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            // Example Database Call
            using (var dbContext = new RecordDbContext())
            {
                foreach (var tr in dbContext.TimeRecords)
                {
                    Console.WriteLine(tr.TimeRecordId.ToString());
                }
            }
            //Write Start Time
            Task.Run(() => TimeRecordManager.RecordTime("startTime"));
            
            deck Deck = new deck(1);
            BasicPolicy Policy = new BasicPolicy();

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
            IGameState initial = game.NewRound(0);
            Player player = new Player(Policy);
            
            long counter = 0;

            while (initial.Reward > 0.0)
            {
                initial = game.Transition(player.Receive(initial.State, initial.Reward));
                counter++;
            }

            //Player Score
            Console.WriteLine("Number of Rounds " + counter.ToString());
            
            //Write End Time
            Task.Run(() => TimeRecordManager.RecordTime("endTime"));
            Console.ReadLine();
            ///  
            ///    |----Agent------|
            ///    |               |
            ///    |--Environment--|
            ///
            ///    Passing State and Reward to Agent in return for Action Passed to Environment
        }
    }
} 
