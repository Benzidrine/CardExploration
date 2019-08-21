namespace MLBlackjack.models
{
    public class card
    {
        public card()
        {

        }

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

        public static card PositionToCard(int Position)
        {
            //Find suit
            int suit = Position % 4 + 1;
            //value = ((position - (position % 4)) / 4) + 1
            int value = ((Position - (Position % 4)) / 4) + 1;
            return new card(0,value,suit);
        }
    }
}