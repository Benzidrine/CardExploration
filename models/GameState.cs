using System;


namespace CardExploration.models
{
    public interface IGameState
    {
        ///<summery>
        ///Stores aspects of the game state including player hand, dealer hand and  current reward
        ///<summery>
        double Reward {get; set;}
        long State {get; set;}

    }
}