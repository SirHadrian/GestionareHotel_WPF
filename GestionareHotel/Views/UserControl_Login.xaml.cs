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

namespace GestionareHotel.Views
{
    /// <summary>
    /// Interaction logic for UserControl_Login.xaml
    /// </summary>
    public partial class UserControl_Login : UserControl
    {
        public UserControl_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            loginLabel.Visibility = Visibility.Collapsed;
            UserNameLabel.Visibility = Visibility.Collapsed;
            UserName_Box.Visibility = Visibility.Collapsed;
            PasswardLabel.Visibility = Visibility.Collapsed;
            Passward_Box.Visibility = Visibility.Collapsed;
            btnJoin.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Collapsed;
            Spacer.Visibility = Visibility.Collapsed;


            JoinLabel.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;
            btnCreateAccount.Visibility = Visibility.Visible;
            btnGuest.Visibility = Visibility.Visible;
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Window test = Window.GetWindow(this);
            test.Close();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            loginLabel.Visibility = Visibility.Visible;
            UserNameLabel.Visibility = Visibility.Visible;
            UserName_Box.Visibility = Visibility.Visible;
            PasswardLabel.Visibility = Visibility.Visible;
            Passward_Box.Visibility = Visibility.Visible;
            btnJoin.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            Spacer.Visibility = Visibility.Visible;

            JoinLabel.Visibility = Visibility.Hidden;
            Back.Visibility = Visibility.Hidden;
            btnCreateAccount.Visibility = Visibility.Hidden;
            btnGuest.Visibility = Visibility.Hidden;
        }
    }
}
