using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionareHotel.ViewModels
{
    class ClientViewModel: BaseViewModel
    {
        private Visibility _adminBtn = HomePageViewModel.adminbtn;
        public Visibility AdminBtn
        {
            get
            {
                return _adminBtn;
            }
            set
            {
                _adminBtn = value;
                OnPropertyChanged("AdminBtn");
            }
        }
    }
}
