using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Attributes

        /// <summary>
        /// The game Manager
        /// </summary>
        GameManager game;

        /// <summary>
        /// List of the Image locations for the player cards
        /// </summary>
        List<Image> playerCards;
        List<Image> dealerCards;

        /// <summary>
        /// List of Canvas Objects
        /// </summary>
        List<Canvas> lstCanvas;

        /// <summary>
        /// Results window
        /// </summary>
        wndResults results;

        #endregion

        #region Methods

        public MainWindow()
        {

            InitializeComponent();

            //initialize GameManager
            game = new GameManager();

            //Set up list of card images
            playerCards = new List<Image>();
            dealerCards = new List<Image>();

            playerCards.Add(playerCard0);
            playerCards.Add(playerCard1);
            playerCards.Add(playerCard2);
            playerCards.Add(playerCard3);
            playerCards.Add(playerCard4);
            playerCards.Add(playerCard5);

            dealerCards.Add(dealerCard0);
            dealerCards.Add(dealerCard1);
            dealerCards.Add(dealerCard2);
            dealerCards.Add(dealerCard3);
            dealerCards.Add(dealerCard4);
            dealerCards.Add(dealerCard5);

            //Set up list of canvases
            lstCanvas = new List<Canvas>();
            lstCanvas.Add(dealerCanvas);
            lstCanvas.Add(player1Canvas);

            lblDealerScore.Visibility = Visibility.Hidden;
            lblPlayerScore.Visibility = Visibility.Hidden;
            lblHand.Visibility = Visibility.Hidden;
            btnHit.Visibility = Visibility.Hidden;
            btnStand.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// This method looks at each hand and places the correct images on the table for each card
        /// </summary>
        private void PopulateBoard()
        {
            //Clear images each time before populating
            foreach (Canvas myCanvas in lstCanvas)
            {
                foreach (Image img in myCanvas.Children)
                {
                    img.Source = null;
                }
            }

            foreach (Image img in dealerCanvas.Children)
            {
                img.Source = null;
            }

            //Place image for cards in hand
            for (int cardNum = 0; cardNum < game.player1Hand.hand.Count; cardNum++)
            {
                playerCards[cardNum].Source = new BitmapImage(new Uri("cards/" + game.player1Hand.hand[cardNum].rank + "_of_" + game.player1Hand.hand[cardNum].suit + ".jpg", UriKind.Relative));
            }

            dealerCard0.Source = new BitmapImage(new Uri("cards/back.jpg", UriKind.Relative));

            for (int cardNum = 1; cardNum < game.dealerHand.hand.Count; cardNum++)
            {
                dealerCards[cardNum].Source = new BitmapImage(new Uri("cards/" + game.dealerHand.hand[cardNum].rank + "_of_" + game.dealerHand.hand[cardNum].suit + ".jpg", UriKind.Relative));
            }

            lblDealerScore.Content = "Dealer: " + game.dealerScore;
            lblPlayerScore.Content = "Player: " + game.player1Score;
            lblHand.Content = "Hand: " + game.player1Hand.getHandValue();
        }

        /// <summary>
        /// This method begins the game when the Start Game button is clicked.  The first two cards are dealt and the board is populated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            //Make game objects visible when game starts
            btnStartGame.Visibility = Visibility.Hidden;
            lblTitle.Visibility = Visibility.Hidden;
            lblDealerScore.Visibility = Visibility.Visible;
            lblPlayerScore.Visibility = Visibility.Visible;
            lblHand.Visibility = Visibility.Visible;
            btnHit.Visibility = Visibility.Visible;
            btnStand.Visibility = Visibility.Visible;
            lblHand.Visibility = Visibility.Visible;

            game.dealFirstCards();

            PopulateBoard();
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            game.playerHit();

            PopulateBoard();

            if (game.player1Hand.getHandValue() > 21)
            {
                results = new wndResults("Bust!");
                game.dealerScore += 1;
                results.ShowDialog();

                //Reset game
                game.ClearBoard();
                PopulateBoard();
            }
            else if (game.player1Hand.getHandValue() == 21)
            {
                btnHit.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// When the player stands, the dealer then hits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStand_Click(object sender, RoutedEventArgs e)
        {
            while (game.dealerHand.getHandValue() < 17 || game.dealerHand.getHandValue() < game.player1Hand.getHandValue())
            {
                await Task.Delay(2000);
                game.DealerHit();
                 
                PopulateBoard();
            }

            // Show dealer's first card
            dealerCards[0].Source = new BitmapImage(new Uri("cards/" + game.dealerHand.hand[0].rank + "_of_" + game.dealerHand.hand[0].suit + ".jpg", UriKind.Relative));

            // If dealer busts
            if (game.dealerHand.getHandValue() > 21)
            {
                results = new wndResults("Dealer Busts!");
                game.player1Score += 1;
                results.ShowDialog();

                // Reset game
                game.ClearBoard();
                PopulateBoard();
            }
            // If dealer has higher total
            else if (game.dealerHand.getHandValue() > game.player1Hand.getHandValue())
            {
                results = new wndResults("Dealer Wins!");
                game.dealerScore += 1;
                results.ShowDialog();

                // Reset game
                game.ClearBoard();
                PopulateBoard();
            }
            // If player has higher total
            else if (game.player1Hand.getHandValue() > game.dealerHand.getHandValue())
            {
                results = new wndResults("You Win!");
                game.player1Score += 1;
                results.ShowDialog();

                // Reset game
                game.ClearBoard();
                PopulateBoard();
                btnHit.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// Clear the hands and table
        /// </summary>
        private void ClearTable()
        {
            playerCards.Clear();
            dealerCards.Clear();
        }
    }

    #endregion
}
