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
    class CancelBooksActions
    {
        SqlDataAdapter _sda;
        //SqlCommandBuilder _scb;
        DataTable _dt;


        private CancelBooksViewModel _cancelBooksViewModel;
        public CancelBooksActions(CancelBooksViewModel _cancelBooksViewModel)
        {
            this._cancelBooksViewModel = _cancelBooksViewModel;
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
                    _cancelBooksViewModel.RezervationsDataTable = _dt;
                }
            }
            con.Close();
        }


        public void CancelRezervation(object param)
        {
            if (_cancelBooksViewModel.ID == null)
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
                    using (SqlCommand cmd = new SqlCommand("CancelRezervation", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _cancelBooksViewModel.ID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot cancel rezervation, wrong ID");
                return;
            }
            _cancelBooksViewModel.ID = null;
            System.Windows.MessageBox.Show("Rezervation Canceled!", "Client", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
