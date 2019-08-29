using System;
using System.Collections.Generic;
using System.Linq;
using MLBlackjack.extensions;
using MLBlackjack.models;

namespace CardExploration.models
{
    interface IEnvironment
    {
        
        Tuple<Int64,double> Transition(int playerAction);
        Int64 ReturnState();

        double ReturnReward(Int64 State);
    }
}