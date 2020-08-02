using GestionareHotel.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        public LoginViewModel()
        {
            
        }

        private bool canExecuteCommand = false;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }

            set
            {
                if (canExecuteCommand == value)
                {
                    return;
                }
                canExecuteCommand = value;
            }
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


        #region Commands

        public void Join(object param)
        {
            //Debug.WriteLine("TESSSSSSSSSSSSTTTTTTTTTTT");
            

            LoginLabel = Visibility.Collapsed;
            UserNameLabel = Visibility.Collapsed;
            UserNameBox = Visibility.Collapsed;
            PasswordLabel = Visibility.Collapsed;
            PasswordBox = Visibility.Collapsed;
            BtnJoin = Visibility.Collapsed;
            BtnLogin = Visibility.Collapsed;
            Spacer = Visibility.Collapsed;


            JoinLabel = Visibility.Visible;
            BtnBack = Visibility.Visible;
            BtnCreateAccount = Visibility.Visible;
            BtnGuest = Visibility.Visible;
        }

        private ICommand _joinCommand;
        public ICommand JoinCommand
        {
            get
            {
                if (_joinCommand == null)
                    _joinCommand = new RelayCommand(Join);
                return _joinCommand;
            }
        }


        public void Back(object param)
        {
            LoginLabel = Visibility.Visible;
            UserNameLabel = Visibility.Visible;
            UserNameBox = Visibility.Visible;
            PasswordLabel = Visibility.Visible;
            PasswordBox = Visibility.Visible;
            BtnJoin = Visibility.Visible;
            BtnLogin = Visibility.Visible;
            Spacer = Visibility.Visible;


            JoinLabel = Visibility.Hidden;
            BtnBack = Visibility.Hidden;
            BtnCreateAccount = Visibility.Hidden;
            BtnGuest = Visibility.Hidden;
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new RelayCommand(Back);
                return _backCommand;
            }
        }

        #endregion

    }
}
