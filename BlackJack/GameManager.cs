using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackJack
{
    class GameManager
    {
        #region Attributes

        /// <summary>
        /// The deck used in the game
        /// </summary>
        public Deck gameDeck;

        /// <summary>
        /// Hand object for the dealer
        /// </summary>
        public Hand dealerHand { get; set; }

        /// <summary>
        /// Hand object for player 1
        /// </summary>
        public Hand player1Hand { get; set; }



        /// <summary>
        /// Initialize scores
        /// </summary>
        public int player1Score { get; set; }
        public int dealerScore { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Default Constructor for new game
        /// </summary>
        public GameManager()
        {
            gameDeck = new Deck();
            dealerHand = new Hand(gameDeck);
            player1Hand = new Hand(gameDeck);
            player1Score = 0;
            dealerScore = 0;
        }

        /// <summary>
        /// Deals the first two cards for dealer and player when game begins
        /// </summary>
        public void dealFirstCards()
        {
            dealerHand.addCardToHand();
            dealerHand.addCardToHand();

            player1Hand.addCardToHand();
            player1Hand.addCardToHand();

            //If player and/or dealer get BlackJack
            if (player1Hand.getHandValue() == 21)
            {
                player1Score += 2;
                ClearBoard();
            }
            else if (dealerHand.getHandValue() == 21)
            {
                dealerScore += 2;
                ClearBoard();
            }
            else if (player1Hand.getHandValue() == 21 && dealerHand.getHandValue() == 21)
            {
                player1Score += 1;
                dealerScore += 1;
                ClearBoard();
            }
        }

        /// <summary>
        /// Adds a card when the player hits
        /// </summary>
        public void playerHit()
        {
            player1Hand.addCardToHand();
        }

        /// <summary>
        /// Adds cards when the player stands
        /// </summary>
        public void DealerHit()
        {
            dealerHand.addCardToHand();
        }

        public void ClearBoard()
        {
            dealerHand.hand.Clear();
            player1Hand.hand.Clear();

            dealFirstCards();
        }

        #endregion

    }
}
