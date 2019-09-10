using System;
using System.Collections.Generic;
using System.Text;
using CardExploration.Interfaces;

namespace CardExploration.Policies
{
    class DictionaryPolicy : IExplorationPolicy
    {
        public long NumStates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long NumActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Epsilon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Dictionary<long, int> Policy;

        public DictionaryPolicy()
        {
            Policy = new Dictionary<long, int>();
        }

        public int ChooseAction(long State)
        {
            if (Policy.TryGetValue(State, out int value))
            {
                return value;
            }
            else
            {
                Policy.Add(State, 0);
                return 0;
            }
        }

            public long GetQValue(long State, int Action)
        {
            throw new NotImplementedException();
        }

        public void UpdatePolicy(long PastState, long CurrentState, int Action, double Reward)
        {
            throw new NotImplementedException();
        }
    }
}
