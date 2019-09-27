using System;
using System.Collections.Generic;

namespace CardExploration.Interfaces
{
    public interface IApproximator
    {
        ///<summery>
        ///Function Approximator, is assumed to be differentiable, log differentiable and parameterised 
        ///<summery>

        /// <summary>
        /// Return the best approximate value for a given set of input features
        /// </summary>
        /// <param name="Features">
        /// A list of inputs that can be mapped to the output value
        /// </param>
        
        double Value(List<Double> Features);
    
        /// <summary>
        /// Return the gradient of the function with respect to the input features
        /// </summary>
        /// <param name="Features">
        /// A list of inputs that can be mapped to the output value
        /// </param>
        List<double> Gradient(List<Double> Features);

        /// <summary>
        /// Return the log (base e) gradient of the function with respect to the input features
        /// </summary>
        /// <param name="Features">
        /// A list of inputs that can be mapped to the output value
        /// </param>
        List<double> LogGradient(List<Double> Features);

        
        /// <summary>
        /// Increments the existing parameters by the provided list
        /// </summary>
        /// <param name="Features">
        /// A list of paramter updates that can be mapped to the output value
        /// </param>
        void update(List<double> parameters);

    }
}