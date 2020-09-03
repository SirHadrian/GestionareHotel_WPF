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
    class ModifyActions
    {
        SqlDataAdapter _sda;
        SqlCommandBuilder _scb;
        DataTable _dt;


        private ModifyViewModel _modifyViewModel;
        public ModifyActions(ModifyViewModel _modifyViewModel)
        {
            this._modifyViewModel = _modifyViewModel;
        }


        public void DeleteOffer(object param)
        {
            if (_modifyViewModel.DeleteOfferID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    using (SqlCommand cmd = new SqlCommand("DeleteOffer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _modifyViewModel.DeleteOfferID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot delete, required for Rezervations");
                return;
            }
            _modifyViewModel.DeleteOfferID = null;
            System.Windows.MessageBox.Show("Offer Deleted!", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void DeleteRoom(object param)
        {
            if (_modifyViewModel.DeleteRoomID == null)
            {
                System.Windows.MessageBox.Show("Insert the ID", "Modify", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    using (SqlCommand cmd = new SqlCommand("DeleteRoom", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", _modifyViewModel.DeleteRoomID);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot delete, required for Rezervations");
                return;
            }

            _modifyViewModel.DeleteRoomID = null;
            System.Windows.MessageBox.Show("Room Deleted!", "Modify", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void LoadRooms(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, Imagine AS Image, Denumire, Descriere, NumarCamere, NumarPersoane, Pret FROM Rooms;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _modifyViewModel.RoomsDataTable = _dt;

            Debug.WriteLine("Executat");
        }


        public void LoadOffers(object param)
        {
            string conectionStringEF = ConfigurationManager.ConnectionStrings["GestionareHotelEntities"].ConnectionString;
            var builder = new EntityConnectionStringBuilder(conectionStringEF);
            var regularConnectionString = builder.ProviderConnectionString;


            SqlConnection con = new SqlConnection(regularConnectionString);
            string querry = "SELECT ID, OfferImage AS Image, Offer_Description, Price, StartDate, EndDate FROM Offers;";

            _sda = new SqlDataAdapter(querry, con);
            _dt = new DataTable();
            _sda.Fill(_dt);

            _modifyViewModel.OffersDataTable = _dt;

            Debug.WriteLine("Executat");
        }


        public void UpdateOffers(object param)
        {
            _scb = new SqlCommandBuilder(_sda);
            _sda.Update(_modifyViewModel.OffersDataTable);
        }


        public void UpdateRooms(object param)
        {
            _scb = new SqlCommandBuilder(_sda);
            _sda.Update(_modifyViewModel.RoomsDataTable);
        }
    }
}
