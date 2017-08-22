using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        #region Attributes

        static int DECK_SIZE = 52;

        public List<Card> cards { set; get; }

        int randNum;

        #endregion


        #region Methods

        /// <summary>
        /// Default Constructor for a new Deck of 52 cards
        /// </summary>
        public Deck()
        {
            cards = new List<Card>(DECK_SIZE);

            foreach (var suit in new [] { "Spades", "Hearts", "Clubs", "Diamonds", })
            {
                for (var rank = 1; rank <= (DECK_SIZE / 4); rank++)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        public Card pullCard()
        {
            Card tempCard;
            Random random = new Random();

            randNum = random.Next(0, cards.Count);

            tempCard = cards[randNum];
            cards.Remove(cards[randNum]);

            return tempCard;
        }

        #endregion
    }
}
