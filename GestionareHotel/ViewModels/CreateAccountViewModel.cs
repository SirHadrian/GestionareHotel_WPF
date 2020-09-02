using GestionareHotel.Commands;
using GestionareHotel.Models;
using GestionareHotel.Models.Actions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class CreateAccountViewModel: BaseViewModel
    {
        private CreateAccountActions _operations;
        public CreateAccountViewModel()
        {
            _operations = new CreateAccountActions(this);
        }


        #region Properties
        //=================================

        private string _userName = null;
        public string UserName 
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _emailAdress = null;
        public string EmailAdress
        {
            get
            {
                return _emailAdress;
            }
            set
            {
                _emailAdress = value;
                OnPropertyChanged("EmailAdress");
            }
        }

        private string _password_1 = null;
        public string Password_1
        {
            get
            {
                return _password_1;
            }
            set
            {
                _password_1 = value;
                OnPropertyChanged("Password_1");
            }
        }

        private string _password_2 = null;
        public string Password_2
        {
            get
            {
                return _password_2;
            }
            set
            {
                _password_2 = value;
                OnPropertyChanged("Password_2");
            }
        }

        //=================================
        #endregion



        #region ICommands
        //=================================
        private ICommand _createAccountCommand;
        public ICommand CreateAccountCommand
        {
            get
            {
                if (_createAccountCommand == null)
                {
                    _createAccountCommand = new RelayCommand(_operations.CreateAccount);
                }
                return _createAccountCommand;
            }
        }


        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(_operations.BackBTN);
                }
                return _backCommand;
            }
        }
        //=================================
        #endregion
    }
}
