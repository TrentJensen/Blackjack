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
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for wndResults.xaml
    /// </summary>
    public partial class wndResults : Window
    {
        #region Methods

        /// <summary>
        /// Default Constructor for Properties Window
        /// </summary>
        public wndResults(string result)
        {
            InitializeComponent();
            lblResult.Content = result;
        }

        /// <summary>
        /// Resets game to play again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
