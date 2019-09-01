using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.models;

namespace CardExploration.Interfaces
{
    interface IEnvironment
    {
        
        Tuple<long, double> Transition(int playerAction);
        long ReturnState();

    }
}