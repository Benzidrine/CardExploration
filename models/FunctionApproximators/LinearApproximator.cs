using System;
using System.Linq;
using System.Collections.Generic;
using CardExploration.Interfaces;

namespace CardExploration.models{
    class LinearApproximator : IApproximator
    {
        public List<double> Weights {get; set;}
        public double Value(List<double> Features)
        {
            return Features.Zip(Weights, (a,b)=>a*b).Sum();       
        }
    
        public List<double> Gradient(List<double> Features){
            /// linear functions gradient is equal to its features
            return Features;
        }

        public List<double> LogGradient(List<double> Features)
        {
            return Features.ConvertAll(f=>Math.Log(f));
        }

        public void update(List<double> parameters){
           Weights.Zip(parameters, (a,b)=>a+b);
        }
    }
}