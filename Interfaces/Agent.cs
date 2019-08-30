using System;
using System.Collections.Generic;
using System.Text;

namespace MLBlackjack.models
{
    /// <summary>
    /// The agent interacts with the environment according to a defined policy 
    /// and a set of constraints i.e. the action space and state space
    /// </summary>
    public abstract class Agent
    {


        /// <summary>
        /// Store the agents model of the world in a state variable as a markov chain
        /// </summary>
        public long State { get; set; }

        /// <summary>
        /// how valuable are future rewards compared to present rewards
        /// this allows for uncertainty of future expected rewards to be incorported 
        /// i.e. the agent should never be 100% about the future
        /// can change over time
        /// </summary>
        public double DiscountFactor { get; set; }

        /// <summary>
        /// The exploration policy defines the behaviour of the agent
        /// can change over time
        /// </summary>
        private IExplorationPolicy ExplorationPolicy { get; set; }

        public Agent(IExplorationPolicy ExplorationPolicy)
        {
            this.ExplorationPolicy = ExplorationPolicy;
        }

        /// <summary>
        /// Agents decision  (action) based on the available information (state) and policy
        /// </summary>     
        public int MakeDecision()
        {
            return ExplorationPolicy.ChooseAction(this.State);
        }

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
        public void UpdateState(long State, int Action, double Reward)
        {
            ExplorationPolicy.UpdatePolicy(State, this.State, Action, Reward);
            this.State = State;
        }
    }
}
