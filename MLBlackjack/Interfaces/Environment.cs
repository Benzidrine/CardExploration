using System;
using System.Collections.Generic;
using System.Linq;
using CardExploration.extensions;
using CardExploration.models;

namespace CardExploration.Interfaces
{
    interface IEnvironment<T>
    {
        
        IGameState<T> Transition(int playerAction);
        long ReturnState();

    }
}