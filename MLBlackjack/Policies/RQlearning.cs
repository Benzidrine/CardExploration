using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardExploration.Interfaces;
using CardExploration.extensions;

namespace CardExploration.Policies{
    class RQlearning : IExplorationPolicy
    {
        private double DiscountFactor { get; set; }
        public IApproximator Qfunc {get; set;}
        public long NumStates { get; set; }
        public long NumActions { get; set; }
        public double Epsilon { get; set; }
        private List<int> Actions {get; set;}

        public RQlearning(double Epsilon, double DiscountFactor, IApproximator Qfunc)
        {
            this.Epsilon = Epsilon;
            this.DiscountFactor = DiscountFactor;
            this.Qfunc = Qfunc;
        }
        /// <summary>
        /// Select a random Action weighted on its value
        /// </summary>
        /// <param name="Values">Weighted values for each action</param>
        /// <returns>Selected action</returns>
        private int Choose(IEnumerable<int> Values){
            int Action = 0;
            int M = Values.Max();
            int L = Values.Min();
            if(M > 0){
            int R = ThreadSafeRandom.ThisThreadsRandom.Next(M-L);
            foreach(int value in Values){
                R -= (value-L);
                if(R <= 0){
                    break;
                }
                Action++;
            }
            }
            else{
                Action = 0;
            }            
            return Action;
        }

        /// <summary>
        /// Selects an Action based on the maximum predicted Qvalue
        /// </summary>
        /// <param name="State">The current game state assumes it exihibits the markov property</param>
        /// <param name="Actions">An Enum of Available actions</param>
        /// <returns>Selected Action</returns>
        public int ChooseAction(List<int> State, IEnumerable<int> Actions)
        {
            this.Actions = Actions.ToList();
            IEnumerable<int> Values = from int Action 
                in Actions
                select (int)GetQValue(State, Action);
            return Choose(Values);
         }
        
        /// <summary>
        /// Gets the maximum predicted Q value for the current state
        /// </summary>
        /// <param name="State">The current game state assumes it exihibits the markov property</param>
        /// <param name="Actions">A list of available actions</param>
        /// <returns>The maxium predicted Q value</returns>
        public double getMaxQValue(List<int> State, List<int> Actions){
           return Actions.Select(Action=>GetQValue(State, Action)).Max();
        }

        /// <summary>
        /// Calculates the predicted Q value for the state action pair
        /// </summary>
        ///   <param name="State">The current state assumed to exibit the markov property</typeparam>
        /// <param name="Action">The select action</typeparam>
        /// <returns>predicted Q value</returns>
        public double GetQValue(List<int> State, int Action)
        {       
            
            /*actions always come first as it assumed the action space is constant */
                return Qfunc.Value(State.ConvertAll(v=>Convert.ToDouble(v)), Action);
        }

        /// <summary>
        /// Calculates the temporal difference or prediction error for the current Q approximator
        /// </summary>
        /// <param name="PastState">The state that the agent transistion from</param>
        /// <param name="CurrentState">The state that the agent has transitioned to</param>
        /// <param name="Action">The action taken by the agent to cause transition</param>
        /// <param name="Reward">The reward recieved from its transition</param>
        /// <returns>The Temporal Difference or prediction value error</returns>
        public double TD(List<int> PastState, List<int> CurrentState, int Action, double Reward){
            return Reward + DiscountFactor*getMaxQValue(CurrentState, this.Actions) - GetQValue(PastState, Action);
        }

        /// <summary>
        /// Update the existing policy based on the available information
        /// </summary>
        /// <param name="PastState">The state that the agent transistion from</param>
        /// <param name="CurrentState">The state that the agent has transitioned to</param>
        /// <param name="Action">The action taken by the agent to cause transition</param>
        /// <param name="Reward">The reward recieved from its transition</param>
        public void UpdatePolicy(List<int> PastState, List<int> CurrentState, int Action, double Reward)
        {
            /*prediction error*/
            double error = TD(PastState, CurrentState, Action, Reward);
            /*current action state feature set*/

            /*parameter increment values*/
            List<double> parameters = Qfunc.Gradient(PastState.ConvertAll(v=>Convert.ToDouble(v)), Action).ConvertAll(v=>v*error*Epsilon);
            /*update parameters in approximator*/
            Qfunc.update(parameters, Action);
        }
    }
} 