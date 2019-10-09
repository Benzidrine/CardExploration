using System;
using System.Collections.Generic;
using System.Text;

namespace CardExploration.Interfaces
{
    /// <summary>
    /// Defines an Exploration Policy that the agent can adopt
    /// The Polciy should balance explotation vs exploration via the Epsilon value
    /// </summary>
    public interface IExplorationPolicy 
    {
        /// <summary>
        /// The total number of states that the agent can exist in 
        /// this number is sadly finite however we can use fuzzy logic 
        /// to make the state space continuous or by defining this to be 
        /// the degrees of freedom
        /// can change over time
        /// </summary>
        long NumStates { get; set; }

        /// <summary>
        /// The total number of actions that the agent can take
        /// like the number of states it is possible to define this as
        /// a continuous space by changing this to the degrees of freedom
        /// can change over time
        /// </summary>
        long NumActions { get; set; }

        /// <summary>
        /// Epsilon quantifies the exploration rate instance.
        /// </summary>
        double Epsilon { get; set; }

        /// <summary>
        ///Given the current state provides the next action to be taken
        /// </summary>
        int ChooseAction(List<int> State, IEnumerable<int> Actions);

        /// <summary>
        ///Provides the utility of being in the provided state and taking the provided action i.e expected reward
        ///Quality of taking the action given the current state
        /// </summary>
        double GetQValue(List<int> State, int Action);

        /// <summary>
        ///Given a list of States and Actions taken plus a final reward or rewards for them the policy should update 
        ///such that is maximises the reward.
        /// </summary>
        void UpdatePolicy(List<int> PastState, List<int> CurrentState, int Action, double Reward);

    }
}
