using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class AdministratorViewModel: BaseViewModel
    {
        #region Visibility User Type
        //========================
        private string _curentUser = Props.curentuser;
        public string CurentUser
        {
            get
            {
                return _curentUser;
            }
            set
            {
                _curentUser = value;
                OnPropertyChanged("CurentUser");
            }
        }


        private Visibility _adminBtn = Props.adminbtn;
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


        public Visibility _angajatBtn = Props.angajatbtn;
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
