using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionareHotel.ViewModels
{
    class HomePageViewModel: BaseViewModel
    {
        public static Visibility adminbtn;



        #region Visibility
        //========================
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


        public Visibility _angajatBtn;
        public Visibility AngajatBtn
        {
            get
            {
                return _angajatBtn;
            }
            set
            {
                _angajatBtn = value;
                OnPropertyChanged("AngajatBtn");
            }
        }
        //============================
        #endregion
    }
}
