using GestionareHotel.Commands;
using GestionareHotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;
using System.Windows.Input;

namespace GestionareHotel.ViewModels
{
    class PrivilegesViewModel: BaseViewModel
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;

        public PrivilegesViewModel()
        {
            
        }

        #region Properties

        private DataTable _usersDataTable;
        public DataTable UsersDataTable
        {
            get { return _usersDataTable; }
            set
            {
                _usersDataTable = value;
                OnPropertyChanged("UsersDataTable");
            }
        }

        private string _userToBeDeleted = null;
        public string DeleteThis
        {
            get
            {
                return _userToBeDeleted;
            }
            set
            {
                _userToBeDeleted = value;
                OnPropertyChanged("DeleteThis");
            }
        }

        #endregion


        #region Commands

        public void LoadUsers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, UserName, EmailAdress, Angajat, Admin FROM Users;";

            sda = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sda.Fill(dt);

            UsersDataTable = dt;

            //Debug.WriteLine("ExECUtaT");

        }

        private ICommand _loadUsers;
        public ICommand LoadUsersCommand
        {
            get
            {
                if (_loadUsers == null)
                    _loadUsers = new RelayCommand(LoadUsers);
                return _loadUsers;
            }
        }



        public void UpdateUsers(object param)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(UsersDataTable);
        }

        private ICommand _updateUsers;
        public ICommand UpdateUsersCommand
        {
            get
            {
                if (_updateUsers == null)
                    _updateUsers = new RelayCommand(UpdateUsers);
                return _updateUsers;
            }
        }



        public void DeleteUser(object param)
        {
            if (DeleteThis == null)
            {
                System.Windows.MessageBox.Show("Insert ID", "Privileges", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;

            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(regularConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UserDel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@userDel", DeleteThis);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            DeleteThis = null;
            System.Windows.MessageBox.Show("User Deleted!", "Privileges", MessageBoxButton.OK, MessageBoxImage.Information);
            //Debug.WriteLine("Executed");
        }

        private ICommand _deleteUsers;
        public ICommand DeleteUsersCommand
        {
            get
            {
                if (_deleteUsers == null)
                    _deleteUsers = new RelayCommand(DeleteUser);
                return _deleteUsers;
            }
        }
        #endregion
    }
}
