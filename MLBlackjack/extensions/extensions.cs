using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using CardExploration.models;

namespace CardExploration.extensions
{
    public static class extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int CardTotal(this List<card> list)
        {
            int CardTotal = 0;
            foreach (card c in list)
            {
                CardTotal += c.value;
            }
            return CardTotal;
        }

        // This takes into account changing aces
        public static int BlackjackTotal(this List<card> list)
        {
            int CardTotal = 0;
            
            foreach (card c in list)
            {
                // Limit value to 10
                int cardValue = c.value > 10 ? 10 : c.value;
                // Change Aces
                if (cardValue == 1) {  CardTotal += 11; }
                else CardTotal += cardValue;
            }
            if (CardTotal > 21)
            {
                CardTotal = 0;
                // Change first ace to 1
                bool ConvertedAce = false;
                foreach (card c in list)
                {
                    // Limit value to 10
                    int cardValue = c.value > 10 ? 10 : c.value;
                    if (cardValue == 1 && !ConvertedAce) {  CardTotal += cardValue; ConvertedAce = !ConvertedAce; }
                    else if (cardValue == 1) {  CardTotal += 11; }
                    else CardTotal += cardValue;
                }
            }
            return CardTotal;
        }


        // Take any number of cards and create a 52 length binrary literal based on the value and suit.  
        public static string CardsToLiteralKey(this List<card> list)
        {
            string LitKey = "";
            for (int i = 0; i < 52; i++)
            {
                int NumOfCardsInPosition = 0;
                // Check that card is in the right position and if so add to int to place in literal later
                foreach (card c in list)
                {
                    if (i == c.CardLiteralKeyPosition())
                    {
                        NumOfCardsInPosition++;
                    }
                }
                LitKey += NumOfCardsInPosition.ToString();
            }
            return LitKey;
        }

        public static Int64 AddCardToState(Int64 state, card card, int NumOfDecks)
        {
            int Position = card.CardLiteralKeyPosition();
            string NewState = "";
            string literal = Convert.ToString(state, (NumOfDecks + 1));
            
            for (int i = 0; i < literal.Length; i++)
            {
                if (Position == i)
                {
                    if (literal[i] == '0')  {NewState += '1'; continue;}
                    if (literal[i] == '1')  {NewState += '2'; continue;}
                    if (literal[i] == '2')  {NewState += '3'; continue;}
                    if (literal[i] == '3')  {NewState += '4'; continue;}
                }
                NewState += literal[i];
            }
            return Convert.ToInt64(NewState,(NumOfDecks + 1));
        }
        // Get the position of a card in CardsToLiteralKey
        public static int CardLiteralKeyPosition(this card card)
        {
            int position = 0;
            //Position is arbitary and is 1D , 1C , 1H, 1S, 2D, 2C, 2H, 2S ...
            position = ((card.value - 1) * 4) + (card.suit - 1);
            return position;
        }


        // Int64 state to a list of cards - Probably inefficient but useful for analysis
        public static List<card> StateNumberToCardList(int NumOfDecks, Int64 State)
        {
            List<card> cards = new List<card>();
            //Convert to key literal by base (number of decks plus one defines base)
            string literal = Convert.ToString(State, (NumOfDecks + 1));
            int i = -1;
            foreach (char c in literal)
            {
                i++;
                // if zero ignore
                if (c == '0') continue;
                // if one
                if (c == '1') cards.Add(card.PositionToCard(i));
                if (c == '2') { cards.Add(card.PositionToCard(i)); cards.Add(card.PositionToCard(i));}
                if (c == '3') { cards.Add(card.PositionToCard(i)); cards.Add(card.PositionToCard(i)); cards.Add(card.PositionToCard(i));}
                if (c == '4') { cards.Add(card.PositionToCard(i)); cards.Add(card.PositionToCard(i)); cards.Add(card.PositionToCard(i));cards.Add(card.PositionToCard(i));}
            }
            return cards;
        }

    }
            public static class ThreadSafeRandom
        {
            [ThreadStatic] private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }

}