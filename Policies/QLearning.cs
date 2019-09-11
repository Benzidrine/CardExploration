using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardExploration.Interfaces;

namespace CardExploration.Policies
{
    class Qlearning : IExplorationPolicy
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

        private Dictionary<long, Dictionary<int, double>> QValue;

        public Qlearning(double Epsilon)
        {
            QValue = new Dictionary<long, Dictionary<int, double>>();
            this.Epsilon = Epsilon;

        }

        public int ChooseAction(long State, Enum Actions)
        {
            if (!QValue.ContainsKey(State))
            {
                QValue.Add(State,
                           Enum.GetValues(Actions.GetType())
                                         .Cast<int>()
                                         .ToDictionary(k => k, v => 0.0));
            }
            return Choose(QValue[State]);

        }

        public double GetQValue(long State, int Action)
        {
            if (QValue.TryGetValue(State, out Dictionary<int, double> Value))
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
                QValue.Add(State, new Dictionary<int, double>());
                return 0;
            }
        }

        public void UpdatePolicy(long PastState, long CurrentState, int Action, double Reward)
        {
            if (QValue.TryGetValue(CurrentState, out Dictionary<int, double> Values))
            {
                QValue[PastState][Action] = (1.0 - Epsilon) * GetQValue(PastState, Action) + Epsilon * (Reward + DiscountFactor * Values.Values.Max());
            }
            else
            {
                QValue[PastState][Action] = (1.0 - Epsilon) * GetQValue(PastState, Action) + Epsilon * Reward;
            }

        }

        private int WeightedChoice(Dictionary<int, double>.Enumerator Value, double sum, double randNum)
        {
            ///<summary>Returns a random key from the dictionary that is weighted based on the values</summary>
            ///<param name="Value">The action value table</param>
            ///<param name="randNum">random number generated for selection of the action</param>
            ///<param name="sum">should be the sum of the values in the dictionary</param>

            Value.MoveNext(); //retrieve the next key value pair
            var entry = Value.Current; //retrieve the current key value pair
            sum += entry.Value; //deincrement the sum
            if (randNum <= sum) //random key selection based on weights from 
            {
                return entry.Key;
            }
            else
            {
                return WeightedChoice(Value, randNum, sum); //recursive
            }
        }

        private int Choose(Dictionary<int, Double> Value)
        {
            ///<summary>Choose an key from the Dictionary weighted on its values</summary>
            double randNum = RandNum.NextDouble() * Value.Values.Sum();
            double sum = 0.0;
            foreach (var entry in Value)
            {
                if (sum <= entry.Value)
                {
                    return entry.Key;
                }
                else
                {
                    sum += entry.Value;
                }
            }
            throw new System.ArgumentException("Well done you broke math");
        }
    }
        
}
