using GestionareHotel.Commands;
using GestionareHotel.Models;
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
 
        public CreateAccountViewModel()
        {
           
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

        public void CreateAccount(object param)
        {
            //Debug.WriteLine("TESSSSSSSSSSSSTTTTTTTTTTT");

            //Debug.WriteLine(_userName);
            //Debug.WriteLine(_emailAdress);
            //Debug.WriteLine(_password_1);
            //Debug.WriteLine(_password_2);

            if (this.UserName == null || this.EmailAdress == null || this.Password_1 == null || this.Password_2 == null) 
            {
                MessageBox.Show("All fields must be completed!");
                return;
            }


            if (!Tools.IsValidEmail(EmailAdress))
            {
                MessageBox.Show("Email invalid");
                return;
            }
            

            if(this.Password_1 != this.Password_2)
            {
                MessageBox.Show("Passwords don't match");
                return;
            }


            //Validate User name form data base!

            //========

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("isUserNameAndEmail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", UserName);
                    cmd.Parameters.AddWithValue("@email", EmailAdress);

                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (status == 1)
                    {
                        MessageBox.Show("UserName or Email already registered");
                        return;
                    }
                    else
                    {
                        using (SqlCommand cmd2 = new SqlCommand("NewUser", con))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@username", UserName);
                            cmd2.Parameters.AddWithValue("@email", EmailAdress);
                            cmd2.Parameters.AddWithValue("@password", Password_1);
                            cmd2.Parameters.AddWithValue("@client", 1);
                            cmd2.Parameters.AddWithValue("@angajat", 0);
                            cmd2.Parameters.AddWithValue("@admin", 0);

                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
                con.Close();
            }

            //================

            MessageBox.Show("Account created successfully");
            var win = Application.Current.MainWindow;
            win.DataContext = new LoginViewModel();
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



        public void BackBTN(object param)
        {
            //Debug.WriteLine("TESSSSSSSSSSSSTTTTTTTTTTT");

            var win = Application.Current.MainWindow;
            win.DataContext = new LoginViewModel();
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new RelayCommand(BackBTN);
                return _backCommand;
            }
        }

        //=================================
        #endregion
    }
}
