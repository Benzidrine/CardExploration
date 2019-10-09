using System;
using System.Linq;
using System.Collections.Generic;
using CardExploration.Interfaces;
using CardExploration.extensions;

namespace CardExploration.models{
    class LinearApproximator : IApproximator
    {
        private Dictionary<int, List<double>> Weights{get; set;}

        public LinearApproximator(int NumFeatures, int NumActions){
            Random RandNum = ThreadSafeRandom.ThisThreadsRandom;
            Weights = new Dictionary<int, List<double>>();
            for(int i=0; i<NumActions; i++){
                Weights.Add(i, new List<double>());
                for(int j=0; j<NumFeatures; j++){
                    Weights[i].Add(RandNum.NextDouble());
                }
            }
        }
        public double Value(List<double> Features, int Action){
            return Features.Zip(Weights[Action], (a,b)=>a*b).Sum();       
        }
    
        public List<double> Gradient(List<double> PFeatures, int Action){
            /// linear functions gradient is equal to its features
            return PFeatures;
        }

        public List<double> LogGradient(List<double> PFeatures, int Action){
            return PFeatures.ConvertAll(f=>Math.Log(f));
        }

        public void update(List<double> parameters, int Action){
            int i = 0;
            foreach(double weight in parameters){
                Weights[Action][i] += weight;
                i++;
            }
        }
    }
}