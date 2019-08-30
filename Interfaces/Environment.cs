using System;
using System.Collections.Generic;
using System.Linq;
using MLBlackjack.extensions;
using MLBlackjack.models;

namespace CardExploration.models
{
    interface IEnvironment
    {
        
        Tuple<long, double> Transition(int playerAction);
        long ReturnState();

    }
}