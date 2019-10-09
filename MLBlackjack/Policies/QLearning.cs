using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardExploration.Interfaces;
using CardExploration.extensions;

namespace CardExploration.Policies
{
    using ATable = Dictionary<int, double>;
    using Qtable = Dictionary<String, Dictionary<int, double>>;
    class Qlearning  : IExplorationPolicy
    {
        /// <summary>
        /// how valuable are future rewards compared to present rewards
        /// this allows for uncertainty of future expected rewards to be incorported 
        /// i.e. the agent should never be 100% about the future
        /// can change over time
        /// </summary>
        private double DiscountFactor { get; set; }

        private Random RandNum { get; } = new Random();
        public long NumStates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long NumActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Epsilon { get; set; }
        private Qtable QTable;

        public Qlearning(double Epsilon, double DiscountFactor)
        {
            QTable = new Qtable();
            this.Epsilon = Epsilon;
            this.DiscountFactor = DiscountFactor;

        }
 
        public int ChooseAction(List<int> State, IEnumerable<int> Actions)
        {
            string key = String.Join(",", State);
            IEnumerable<double> Values = from Action in Actions select GetQValue(State, Action);
            return Choose(Values.ToList());
        }

        public double GetQValue(List<int> State, int Action)
        {
            if (QTable.TryGetValue(String.Join(",", State), out ATable Value))
            {
                if (Value.TryGetValue(Action, out double value))
                {
                    return value;
                }
                else
                {
                    Value.Add(Action, value);
                    return 0;
                }
            }
            else
            {
                ATable ATable = new ATable();
                ATable.Add(Action, 0.0);
                QTable.Add(String.Join(",", State), ATable);
                return 0;
            }
        }

        public double getMaxQValue(List<int> State){
           if (QTable.TryGetValue(String.Join(",", State), out ATable Values)){
               return Values.Values.Max();
           } else {
               return 0.0;
           }
        }

        public void UpdatePolicy(List<int> PastState, List<int> CurrentState, int Action, double Reward)
        {
            QTable[String.Join(",", PastState)][Action] = (1.0 - Epsilon) * GetQValue(PastState, Action) + Epsilon * (Reward + DiscountFactor * getMaxQValue(CurrentState));
        }

        private int Choose(List<double> Values){
            List<double> Weights = Values.ConvertAll(v=>Math.Exp(v));
            double MaxVal = Weights.Sum();
            int Action = 0;
            double R = ThreadSafeRandom.ThisThreadsRandom.NextDouble()*MaxVal;
            foreach(double Weight in Weights){
                R -= Weight;
                if(R <= 0.0){
                    break;
                }
                Action++;
            }
            return Action;
        }
    }
        
}
