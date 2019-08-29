using System;
using System.Collections.Generic;
using System.Text;

namespace MLBlackjack.models
{
    /// <summary>
    /// The agent interacts with the environment according to a defined policy 
    /// and a set of constraints i.e. the action space and state space
    /// </summary>
    interface IAgent
    {
        /// <summary>
        /// The total number of states that the agent can exist in 
        /// this number is sadly finite however we can use fuzzy logic 
        /// to make the state space continuous or by defining this to be 
        /// the degrees of freedom
        /// can change over time
        /// </summary>
        Int64 NumStates { get; set; }

        /// <summary>
        /// The total number of actions that the agent can take
        /// like the number of states it is possible to define this as
        /// a continuous space by changing this to the degrees of freedom
        /// can change over time
        /// </summary>
        Int64 NumActions { get; set; }

        /// <summary>
        /// Store the agents model of the world in a state variable as a markov chain
        /// </summary>
        Int64 State { get; }

        /// <summary>
        /// how valuable are future rewards compared to present rewards
        /// this allows for uncertainty of future expected rewards to be incorported 
        /// i.e. the agent should never be 100% about the future
        /// can change over time
        /// </summary>
        float DiscountFactor { get; set; }

        /// <summary>
        /// The exploration policy defines the behaviour of the agent
        /// can change over time
        /// </summary>
        IExplorationPolicy ExplorationPolicy { get; set; }

        /// <summary>
        /// Agents decision  (action) based on the available information (state)
        /// </summary>
        /// <param name="State"></param>
        /// <returns>Return the next action based on the current policy and provided state</returns>
        Int64 GetAction(Int64 State);

        /// <summary>
        /// Update the current state with the new state 
        /// Use the reward and state transition to update the policy
        /// </summary>
        /// <param name="State">
        /// The state obtained from taking the provided action
        /// </param>
        /// <param name="Reward">
        /// The reward obtained for taking the provided action
        /// </param>
        /// <param name="Action">
        /// The action taken to transition to the new state
        /// </param>
        void UpdateState(Int64 State, Int64 Action, double Reward);
    }
}
