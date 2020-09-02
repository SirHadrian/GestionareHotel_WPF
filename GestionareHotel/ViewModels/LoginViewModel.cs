using GestionareHotel.Commands;
using GestionareHotel.Models;
using GestionareHotel.Models.Actions;
using GestionareHotel.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        private LoginActions _operations;
        public LoginViewModel()
        {
            _operations = new LoginActions(this);
        }


        #region Visibility

        private Visibility loginLabel;
        public Visibility LoginLabel {
            get
            {
                return loginLabel;
            }
            set
            {
                loginLabel = value;
                OnPropertyChanged("LoginLabel");
            }
        }

        private Visibility _userNameLabel;
        public Visibility UserNameLabel
        {
            get
            {
                return _userNameLabel;
            }
            set
            {
                _userNameLabel = value;
                OnPropertyChanged("UserNameLabel");
            }
        }

        private Visibility _userNameBox;
        public Visibility UserNameBox
        {
            get
            {
                return _userNameBox;
            }
            set
            {
                _userNameBox = value;
                OnPropertyChanged("UserNameBox");
            }
        }

        private Visibility _passwordLabel;
        public Visibility PasswordLabel
        {
            get
            {
                return _passwordLabel;
            }
            set
            {
                _passwordLabel = value;
                OnPropertyChanged("PasswordLabel");
            }
        }

        private Visibility _passwordBox;
        public Visibility PasswordBox
        {
            get
            {
                return _passwordBox;
            }
            set
            {
                _passwordBox = value;
                OnPropertyChanged("PasswordBox");
            }
        }

        private Visibility _btnJoin;
        public Visibility BtnJoin
        {
            get
            {
                return _btnJoin;
            }
            set
            {
                _btnJoin = value;
                OnPropertyChanged("BtnJoin");
            }
        }

        private Visibility _btnLogin;
        public Visibility BtnLogin
        {
            get
            {
                return _btnLogin;
            }
            set
            {
                _btnLogin = value;
                OnPropertyChanged("BtnLogin");
            }
        }

        private Visibility _spacer = Visibility.Hidden;
        public Visibility Spacer
        {
            get
            {
                return _spacer;
            }
            set
            {
                _spacer = value;
                OnPropertyChanged("Spacer");
            }
        }

        private Visibility _joinLabel = Visibility.Hidden;
        public Visibility JoinLabel
        {
            get
            {
                return _joinLabel;
            }
            set
            {
                _joinLabel = value;
                OnPropertyChanged("JoinLabel");
            }
        }

        private Visibility _btnBack = Visibility.Hidden;
        public Visibility BtnBack
        {
            get
            {
                return _btnBack;
            }
            set
            {
                _btnBack = value;
                OnPropertyChanged("BtnBack");
            }
        }

        private Visibility _btnGuest = Visibility.Hidden;
        public Visibility BtnGuest
        {
            get
            {
                return _btnGuest;
            }
            set
            {
                _btnGuest = value;
                OnPropertyChanged("BtnGuest");
            }
        }

        private Visibility _btnCreateAccount = Visibility.Hidden;
        public Visibility BtnCreateAccount
        {
            get
            {
                return _btnCreateAccount;
            }
            set
            {
                _btnCreateAccount = value;
                OnPropertyChanged("BtnCreateAccount");
            }
        }
        #endregion


        #region Proprieties
        //============================

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

        private string _password = null;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


        //============================
        #endregion


        #region Commands
        //===================
        private ICommand _joinCommand;
        public ICommand JoinCommand
        {
            get
            {
                if (_joinCommand == null)
                {
                    _joinCommand = new RelayCommand(_operations.Join);
                }
                return _joinCommand;
            }
        }


        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(_operations.Back);
                }
                return _backCommand;
            }
        }


        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(_operations.Login);
                }
                return _loginCommand;
            }
        }


        private ICommand _guestCommand;
        public ICommand GuestCommand
        {
            get
            {
                if (_guestCommand == null)
                {
                    _guestCommand = new RelayCommand(_operations.Guest);
                }
                return _guestCommand;
            }
        }


        private ICommand _createAccountButton;
        public ICommand CreateAccountButton
        {
            get
            {
                if (_createAccountButton == null)
                {
                    _createAccountButton = new RelayCommand(_operations.CreateAccountB);
                }
                return _createAccountButton;
            }
        }
        //===================
        #endregion
    }
}
