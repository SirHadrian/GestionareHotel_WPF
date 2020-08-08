using GestionareHotel.Models;
using GestionareHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
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
    /// Interaction logic for PrivilegesView.xaml
    /// </summary>
    public partial class PrivilegesView : UserControl
    {
        public PrivilegesView()
        {
            InitializeComponent();
            DataContext = new PrivilegesViewModel();
        }

        
    }
}
