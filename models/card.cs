namespace MLBlackjack.models
{
    public class card
    {
        public card(int id, int power, int cardsuit)
        {
            ID = id;
            //value equals the blackjack value
            value = power;
            suit = cardsuit;
        }
        public int ID {get; set;}
        public int value {get; set;}
        public int suit {get; set;}
    }
}