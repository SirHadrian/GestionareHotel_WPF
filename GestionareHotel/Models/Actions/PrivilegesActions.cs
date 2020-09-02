using GestionareHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionareHotel.Models.Actions
{
    class PrivilegesActions
    {
        SqlDataAdapter _sda;
        SqlCommandBuilder _scb;
        DataTable _dt;


        private PrivilegesViewModel _privilegesViewModel;
        public PrivilegesActions(PrivilegesViewModel _privilegesViewModel)
        {
            this._privilegesViewModel = _privilegesViewModel;
        }


        public void LoadUsers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, UserName, EmailAdress, Angajat, Admin FROM Users;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _privilegesViewModel.UsersDataTable = _dt;

            //Debug.WriteLine("ExECUtaT");
        }


        public void UpdateUsers(object param)
        {
            _scb = new SqlCommandBuilder(_sda);
            _sda.Update(_privilegesViewModel.UsersDataTable);
        }


        public void DeleteUser(object param)
        {
            if (_privilegesViewModel.DeleteThis == null)
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

                    cmd.Parameters.AddWithValue("@userDel", _privilegesViewModel.DeleteThis);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            _privilegesViewModel.DeleteThis = null;
            System.Windows.MessageBox.Show("User Deleted!", "Privileges", MessageBoxButton.OK, MessageBoxImage.Information);
            //Debug.WriteLine("Executed");
        }
    }
}
