using System;


namespace CardExploration.Interfaces
{
    public interface IGameState
    {
        ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        Int32 Reward {get; set;}
        long State {get; set;}

    }
}