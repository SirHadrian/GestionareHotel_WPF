using GestionareHotel.ViewModels;
using GestionareHotel.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionareHotel.Models.Actions
{
    class LoginActions
    {
        private LoginViewModel _loginViewModel;
        public LoginActions(LoginViewModel _loginViewModel)
        {
            this._loginViewModel = _loginViewModel;
        }


        public void Join(object param)
        {
            _loginViewModel.LoginLabel = Visibility.Collapsed;
            _loginViewModel.UserNameLabel = Visibility.Collapsed;
            _loginViewModel.UserNameBox = Visibility.Collapsed;
            _loginViewModel.PasswordLabel = Visibility.Collapsed;
            _loginViewModel.PasswordBox = Visibility.Collapsed;
            _loginViewModel.BtnJoin = Visibility.Collapsed;
            _loginViewModel.BtnLogin = Visibility.Collapsed;
            _loginViewModel.Spacer = Visibility.Collapsed;


            _loginViewModel.JoinLabel = Visibility.Visible;
            _loginViewModel.BtnBack = Visibility.Visible;
            _loginViewModel.BtnCreateAccount = Visibility.Visible;
            _loginViewModel.BtnGuest = Visibility.Visible;
        }


        public void Back(object param)
        {
            _loginViewModel.LoginLabel = Visibility.Visible;
            _loginViewModel.UserNameLabel = Visibility.Visible;
            _loginViewModel.UserNameBox = Visibility.Visible;
            _loginViewModel.PasswordLabel = Visibility.Visible;
            _loginViewModel.PasswordBox = Visibility.Visible;
            _loginViewModel.BtnJoin = Visibility.Visible;
            _loginViewModel.BtnLogin = Visibility.Visible;
            _loginViewModel.Spacer = Visibility.Visible;


            _loginViewModel.JoinLabel = Visibility.Hidden;
            _loginViewModel.BtnBack = Visibility.Hidden;
            _loginViewModel.BtnCreateAccount = Visibility.Hidden;
            _loginViewModel.BtnGuest = Visibility.Hidden;
        }


        public void Login(object param)
        {
            if (_loginViewModel.UserName == null || _loginViewModel.Password == null)
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


                using (SqlCommand cmd = new SqlCommand("isAdmin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", _loginViewModel.UserName);


                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (status == 1)
                    {
                        Props.adminbtn = Visibility.Visible;
                        Props.angajatbtn = Visibility.Visible;
                    }
                    else
                    {
                        Props.adminbtn = Visibility.Collapsed;
                        Debug.WriteLine("Not Admin");
                    }
                }


                using (SqlCommand cmd = new SqlCommand("isAngajat", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", _loginViewModel.UserName);


                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (status == 1)
                    {
                        Props.angajatbtn = Visibility.Visible;
                    }
                    else
                    {
                        Props.angajatbtn = Visibility.Collapsed;
                        Debug.WriteLine("Not Angajat");
                    }
                }



                using (SqlCommand cmd = new SqlCommand("Login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", _loginViewModel.UserName);
                    cmd.Parameters.AddWithValue("@Password", _loginViewModel.Password);

                    int status = Convert.ToInt32(cmd.ExecuteScalar());

                    if (status == 1)
                    {
                        Props.curentuser = _loginViewModel.UserName;

                        Main main = new Main();
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


        public void Guest(object param)
        {
            Props.curentuser = "Guest";
            Props.adminbtn = Visibility.Collapsed;
            Props.angajatbtn = Visibility.Collapsed;

            Main main = new Main();
            main.Show();

            var win = Application.Current.MainWindow;
            win.Close();
        }


        public void CreateAccountB(object param)
        {
            var win = Application.Current.MainWindow;
            win.DataContext = new CreateAccountViewModel();
        }
    }
}
