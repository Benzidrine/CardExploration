namespace CardExploration.models
{
    public class card
    {
        public card()
        {

        }

        public card(int id, int value, int suit)
        {
            /// <summary>
            /// instantiate the card object with a id, value and suit
            /// </summary>
            /// <param name="id">An identification number</param>
            /// <param name="value">Cards value in the game</param>
            /// <param name="suit">Cards sub identity as a member of a suit</param>
            this.id = id;
            this.value = value;
            this.suit = suit;
        }
        public int id {get; set;}
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
