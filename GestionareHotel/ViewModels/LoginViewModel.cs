using GestionareHotel.Commands;
using GestionareHotel.Models;
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
        GestionareHotelEntities context;

        public LoginViewModel()
        {
            context = new GestionareHotelEntities();
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


        #region Proprieties
        //============================

        private string _userName;
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

        private string _password;
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


        public void Login(object param)
        {
            if (UserName == null || Password == null) 
            {
                MessageBox.Show("UserName or Password missing!");
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;

            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    

                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (status == 1)
                    {
                        MainWindow main = new MainWindow();
                        main.Show();

                        var win = Application.Current.MainWindow;
                        win.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!");
                    }

                    
                }
                con.Close();
            }
        }


        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new RelayCommand(Login);
                return _loginCommand;
            }
        }


        public void Guest(object param)
        {
            MainWindow main = new MainWindow();
            main.Show();

            var win = Application.Current.MainWindow;
            win.Close();
        }

        private ICommand _guestCommand;
        public ICommand GuestCommand
        {
            get
            {
                if (_guestCommand == null)
                    _guestCommand = new RelayCommand(Guest);
                return _guestCommand;
            }
        }


        public void CreateAccountB(object param)
        {
            var win = Application.Current.MainWindow;
            win.DataContext = new CreateAccountViewModel();
        }

        private ICommand _createAccountButton;
        public ICommand CreateAccountButton
        {
            get
            {
                if (_createAccountButton == null)
                    _createAccountButton = new RelayCommand(CreateAccountB);
                return _createAccountButton;
            }
        }

        #endregion

    }
}
