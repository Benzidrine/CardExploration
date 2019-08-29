using System;
using System.Collections.Generic;
using System.Text;

namespace MLBlackjack.models
{
    /// <summary>
    /// Defines an Exploration Policy that the agent can adopt
    /// The Polciy should balance explotation vs exploration via the Epsilon value
    /// </summary>
    interface IExplorationPolicy
    {

        /// <summary>
        /// Epsilon quantifies the exploration rate instance.
        /// </summary>
        float Epsilon { get; set; }

        /// <summary>
        ///Given the current state provides the next action to be taken
        /// </summary>
        Int64 ChooseAction(Int64 State);

        /// <summary>
        ///Provides the utility of being in the provided state and taking the provided action i.e expected reward
        ///Quality of taking the action given the current state
        /// </summary>
        Int64 GetQValue(Int64 State, Int64 Action);

        /// <summary>
        ///Given a list of States and Actions taken plus a final reward or rewards for them the policy should update 
        ///such that is maximises the reward.
        /// </summary>
        void UpdatePolicy(Int64[] States, Int64[] Actions, double[] Reward);

    }
}
