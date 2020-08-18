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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionareHotel.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = new HomePageViewModel();
        }

        private void AdminBtnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new AdministratorViewModel();
        }

        private void HomeBtnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new HomePageViewModel();
        }

        private void ClientBtnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new ClientViewModel();
        }

        private void AngajatBtnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new AngajatViewModel();
        }
    }
}
