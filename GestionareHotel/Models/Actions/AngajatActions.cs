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
    class AngajatActions
    {
        SqlDataAdapter _sda;
        //SqlCommandBuilder _scb;
        DataTable _dt;


        private AngajatViewModel _angajatViewModel;
        public AngajatActions(AngajatViewModel _angajatViewModel)
        {
            this._angajatViewModel = _angajatViewModel;
        }


        public void LoadRezervations(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            con.Open();
            using (var cmd = new SqlCommand("getRezervationsFull", con))
            {
                _dt = new DataTable();
                using (_sda = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _sda.Fill(_dt);
                    _angajatViewModel.RezervationsDataTable = _dt;
                }
            }
            con.Close();           
        }


        public void DezactivateRezervations(object param)
        {
            if (_angajatViewModel.ID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Angajat", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    using (SqlCommand cmd = new SqlCommand("DezactivateRezervation", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _angajatViewModel.ID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot update, wrong ID");
                return;
            }
            _angajatViewModel.ID = null;
            System.Windows.MessageBox.Show("Rezervation Updated!", "Angajat", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void ActivateRezervations(object param)
        {
            if (_angajatViewModel.ID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Angajat", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    using (SqlCommand cmd = new SqlCommand("ActivateRezervation", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _angajatViewModel.ID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot update, wrong ID");
                return;
            }
            _angajatViewModel.ID = null;
            System.Windows.MessageBox.Show("Rezervation Updated!", "Angajat", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
