using System;
using System.Linq;
using System.Collections.Generic;
using CardExploration.models;
using CardExploration.Interfaces;
using CardExploration.extensions;
using CardExploration.Policies;
using System.IO;
using CardExploration.DatabaseContext;
using System.Threading.Tasks;
using CardExploration.Manager;
using System.Diagnostics;
namespace CardExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            //Write Start Time
            Task.Run(() => TimeRecordManager.RecordTime("startTime"));
             string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            deck Deck = new deck(1);
            IApproximator Qfunc = new LinearApproximator(10, 2);
            IExplorationPolicy Policy = new RQlearning(0.0001, 0.99, Qfunc);
            PlayerAction Actions = new PlayerAction();
            Agent player = new Agent(Policy);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "learning rate.csv")))
            { 
            for(int i=0; i<10000; i++){
                    // Single deck game instance
                    Game game = new Game(1);
                    IGameState<card> initial = game.NewRound(0);
                    long counter = 0;

                    while (initial.Reward > 0.0)
                    {
                        int action = player.MakeDecision(initial.GetState(), Enum.GetValues(Actions.GetType()).Cast<int>());
                        initial = game.Transition(action);
                        player.UpdatePolicy(initial.GetState(), action, initial.Reward);       
                        counter++;
                    }

                    //Player Score
                    outputFile.WriteLine(counter.ToString());
                    Console.WriteLine("Number of Rounds " + counter.ToString());
                }  
            }
            //Write End Time
/*            Task.Run(() => TimeRecordManager.RecordTime("endTime"));*/ 
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
