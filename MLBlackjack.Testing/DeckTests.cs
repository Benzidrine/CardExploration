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
        public void ShuffleValue()
        {
            deck Deck = new deck(1);
            int InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Deck.Cards.Shuffle();
            int ShuffledCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, ShuffledCardTotal);
        }

        /// <summary>
        /// Check deck instantiation value for single deck
        /// </summary>
        [TestMethod]
        public void SingleDeckTotalValue()
        {
            deck Deck = new deck(1);
            int InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, 364);
        }

        /// <summary>
        /// Check deck instantiation value for multiple decks
        /// </summary>
        [TestMethod]
        public void MultiDeckTotalValue()
        {
            deck Deck = new deck(2);
            int InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, (364 * 2));

            Deck = new deck(3);
            InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, (364 * 3));

            Deck = new deck(4);
            InitialCardTotal = CardExploration.extensions.extensions.CardTotal(Deck.Cards);
            Assert.AreEqual(InitialCardTotal, (364 * 4));
        }

        /// <summary>
        /// Count number of cards in deck instantiation 
        /// </summary>
        [TestMethod]
        public void DeckCardCount()
        {
            Assert.AreEqual(new deck(1).Cards.Count, 52);
        }
    }
}