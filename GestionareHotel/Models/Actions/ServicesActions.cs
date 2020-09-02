using GestionareHotel.ViewModels;
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
    class ServicesActions
    {
        SqlDataAdapter _sda;
        SqlCommandBuilder _scb;
        DataTable _dt;


        private ServicesViewModel _servicesViewModel;
        public ServicesActions(ServicesViewModel _servicesViewModel)
        {
            this._servicesViewModel = _servicesViewModel;
        }


        public void LoadService(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Descriere, Pret FROM Servicii;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _servicesViewModel.ServicesDataTable = _dt;
        }


        public void UpdateServices(object param)
        {
            _scb = new SqlCommandBuilder(_sda);
            _sda.Update(_servicesViewModel.ServicesDataTable);
        }


        public void DeleteService(object param)
        {
            if (_servicesViewModel.DeleteServiceID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Services", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteServ", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _servicesViewModel.DeleteServiceID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot delete, required for Rezervations");
            }
            _servicesViewModel.DeleteServiceID = null;
            System.Windows.MessageBox.Show("Service Deleted", "Services", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void AddService(object param)
        {
            if (_servicesViewModel.Service == null || _servicesViewModel.Price == null)
            {
                System.Windows.MessageBox.Show("Missing parameters!", "Services", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(regularConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddServ", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@descriere", _servicesViewModel.Service);
                        cmd.Parameters.AddWithValue("@pret", _servicesViewModel.Price);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Incorect values");
            }
            System.Windows.MessageBox.Show("Service added!", "Services", MessageBoxButton.OK, MessageBoxImage.Information);
            Debug.WriteLine("Executat");
        }
    }
}
