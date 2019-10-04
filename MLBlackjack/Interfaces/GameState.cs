using System;
using System.Collections.Generic;

namespace CardExploration.Interfaces
{
    public interface IGameState<T>
    {
        ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        Int32 Reward {get; set;}
        List<int> GetState();

    }
}