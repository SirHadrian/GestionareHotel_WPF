using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        GestionareHotelEntities context;

        public CreateAccountViewModel()
        {
            context = new GestionareHotelEntities();
        }


        #region Properties
        //=================================

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

        private string _emailAdress;
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

        private string _password_1;
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

        private string _password_2;
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

        public void CreateAccount(object param)
        {
            //Debug.WriteLine("TESSSSSSSSSSSSTTTTTTTTTTT");

            Debug.WriteLine(_userName);
            Debug.WriteLine(_emailAdress);
            Debug.WriteLine(_password_1);
            Debug.WriteLine(_password_2);

            if (this.UserName == "" || this.EmailAdress == "" || this.Password_1 == "" || this.Password_2 == "") 
            {
                MessageBox.Show("All fields must be completed!");
                return;
            }

            if(this.Password_1 != this.Password_2)
            {
                MessageBox.Show("Passwords don't match");
                return;
            }

            User newUser = new User()
            {
                UserName = this.UserName,
                EmailAdress = this.EmailAdress,
                Password = this.Password_1,
                Client = true
            };

            context.Users.Add(newUser);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                this.UserName = "";
                context.Users.Remove(newUser);
                Debug.WriteLine(ex);
                MessageBox.Show("Username already registered"); 
            }
        }

        private ICommand _createAccountCommand;
        public ICommand CreateAccountCommand
        {
            get
            {
                if (_createAccountCommand == null)
                    _createAccountCommand = new RelayCommand(CreateAccount);
                return _createAccountCommand;
            }
        }

        //=================================
        #endregion
    }
}
