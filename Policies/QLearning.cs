using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardExploration.Interfaces;
using CardExploration.extensions;

namespace CardExploration.Policies
{
    class Qlearning<T>  : IExplorationPolicy <T> 
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
        private Dictionary<string, ActionTable> QTable;

        public Qlearning(double Epsilon, double DiscountFactor)
        {
            QTable = new Dictionary<string, ActionTable>();
            this.Epsilon = Epsilon;
            this.DiscountFactor = DiscountFactor;

        }

        public int ChooseAction(List<T> State, Enum Actions)
        {
            if (!QTable.ContainsKey(State.ToString()))
            {
                QTable.Add(State.ToString(),
                           Enum.GetValues(Actions.GetType())
                                         .Cast<int>()
                                         .ToDictionary(k => k, v => 0.0));
            }
            return Choose(QTable[State.ToString()]);

        }

        public double GetQValue(List<T> State, int Action)
        {
            if (QTable.TryGetValue(State.ToString(), out Dictionary<int, double> Value))
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
                QTable.Add(State.ToString(), new Dictionary<int, ActionTable>());
                return 0;
            }
        }

        public void UpdatePolicy(List<T> PastState, List<T> CurrentState, int Action, double Reward)
        {
            if (QTable.TryGetValue(CurrentState.ToString(), out Dictionary<int, double> Values))
            {
                QTable[PastState.ToString()][Action] = (1.0 - Epsilon) * GetQValue(PastState, Action) + Epsilon * (Reward + DiscountFactor * Values.Values.Max());
            }
            else
            {
                QTable[PastState.ToString()][Action] = (1.0 - Epsilon) * GetQValue(PastState, Action) + Epsilon * Reward;
            }

        }

        private int Choose(Dictionary<int, Double> Value)
        {
            ///<summary>Choose an key from the Dictionary weighted on its values</summary>
            if(ThreadSafeRandom.ThisThreadsRandom.Next(100) == 1)
            {
                int select = ThreadSafeRandom.ThisThreadsRandom.Next(Value.Count()-1);
                return Value.Keys.ToList()[select];
            } else 
            {
                return Value.Select(x => x.Value == Value.Values.Max())
            }
        }
    }
        
}
