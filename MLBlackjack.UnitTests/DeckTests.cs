using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardExploration.extensions;
using CardExploration.models;

namespace MLBlackjack.UnitTest
{
    [TestClass]
    public class DeckTests
    {
        /// <summary>
        /// Check that shuffling a deck doesn't change the total value of cards in the deck
        /// </summary>
        [TestMethod]
        public void CheckShuffleValue()
        {
            deck Deck = new deck(1);
            int InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Deck.Cards.Shuffle();
            int ShuffledCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, ShuffledCardTotal);
        }
    }
}