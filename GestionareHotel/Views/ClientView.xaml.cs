using GestionareHotel.Models;
using GestionareHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionareHotel.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : System.Windows.Controls.UserControl
    {
        public ClientView()
        {
            InitializeComponent();
            DataContext = new BookRoomViewModel();
        }


        private void BookRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BookRoomViewModel();
        }

        private void MyBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Props.curentuser == "Guest")
            {
                System.Windows.MessageBox.Show("Create an account to acces all functionality");
            }
            else
            {
                DataContext = new MyBooksViewModel();
            }
        }

        private void BookOfferBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BookOfferViewModel();
        }

        private void BookServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BookServiceViewModel();
        }
    }
}
