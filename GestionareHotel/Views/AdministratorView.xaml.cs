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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionareHotel.Views
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : UserControl
    {
        public AdministratorView()
        {
            InitializeComponent();
            DataContext = new PrivilegesViewModel();
        }

        private void RoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RoomsViewModel();
        }

        private void PrivilegesBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PrivilegesViewModel();
        }
    }
}
