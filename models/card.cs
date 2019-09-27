using System;
using System.Collections;
namespace CardExploration.models
{
    public class card : IComparable
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

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is card Card)
                return Card.value.CompareTo(this.value);
            else
                throw new ArgumentException("Object is not a card");
        }

        override public string ToString()
        {
            return value.ToString();
        }
    }
}
